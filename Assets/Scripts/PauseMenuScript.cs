using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PauseMenuScript : MonoBehaviour {
    
    public GameObject PauseMenuObject;
    public GameObject hud;

    public bool isActive;

    private void Awake()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;
        }

        if (isActive)
        {
            PauseMenuObject.SetActive(true);
            hud.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            PauseMenuObject.SetActive(false);
            hud.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

	}

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void EXITButton()
    {
        Application.Quit();
    }
}
