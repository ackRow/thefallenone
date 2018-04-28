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

    bool time_stop;
    bool solo;
    bool button_set = false;
    Scene actual_scene;

    private void Awake()
    {
        isActive = false;
        solo = false;
    }

    void Set_buttons()
    {
        if (solo)
        {
            GameObject.Find("MainMenuButton").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(MainMenuButton);

            GameObject.Find("QuitButton").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("QuitButton").GetComponent<Button>().onClick.AddListener(Application.Quit);
        }
        else
        {
            GameObject.Find("MainMenuButton").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);

            GameObject.Find("QuitButton").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("QuitButton").GetComponent<Button>().onClick.AddListener(Application.Quit);
        }
    }

    void Update () {

        actual_scene = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;
            if (actual_scene.name == "Level1" || actual_scene.name == "Level2")
            {
                solo = true;
            }
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
            if (!time_stop && solo)
            {
                time_stop = true;
                Time.timeScale = 0;
            }
        }
        else
        {
            PauseMenuObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            button_set = false;
            if (time_stop && solo)
            {
                time_stop = false;
                Time.timeScale = 1;
            }
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
