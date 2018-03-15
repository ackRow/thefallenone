using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

    public GameObject menuObject;
    public GameObject hud;
    public bool isActive = false;

	// Update is called once per frame
	void Update () {
		
        if (isActive)
        {
            menuObject.SetActive(true);
            hud.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
        else
        {
            menuObject.SetActive(false);
            hud.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RESUMEButton();
        }
	}

    public void RESUMEButton()
    {
        isActive = !isActive;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("menutest");
    }

    public void EXITButton()
    {
        Application.Quit();
    }
}
