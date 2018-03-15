using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PauseMenuScript : MonoBehaviour {

    public NetworkManager netm;
    public GameObject menuObject;
    public GameObject hud;

    public bool isActive = true;
    public bool state = true;

	// Update is called once per frame
	void Update () {
		
        if (isActive)
        {
            menuObject.SetActive(true);
            hud.SetActive(false);
            state = true;
        }
        else
        {
            menuObject.SetActive(false);
            hud.SetActive(true);
        }

        if (!netm.isNetworkActive)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }

        if (state)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            state = !state;
        }
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
