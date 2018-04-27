using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour {
    
    public GameObject PauseMenuObject;
    public GameObject hud;

    public bool isActive;
    bool button_set = false;

    private void Awake()
    {
        isActive = false;
    }

    void Set_buttons()
    {
        GameObject.Find("MainMenuButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);

        GameObject.Find("QuitButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("QuitButton").GetComponent<Button>().onClick.AddListener(Application.Quit);
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;
        }

        if (isActive)
        {
            PauseMenuObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            if (!button_set)
            {
                Set_buttons();
                button_set = true;
            }
        }
        else
        {
            PauseMenuObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            button_set = false;
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
