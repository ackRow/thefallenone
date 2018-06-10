using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public GameObject MainCanvas;
    public GameObject singleplayerCanvas;
    public GameObject multiplayerCanvas;
    public GameObject loadingCanvas;
    public GameObject Options;

    public CustomNetManager netmanager;
    private bool button_set;

    bool options_shown;

    private void Awake()
    {
        button_set = false;
        options_shown = false;
    }
    /*private void Awake()
    {
        netmanager = GameObject.Find("NetworkManager").GetComponent<CustomNetManager>();
        if (netmangager.launched)
        {
            loginCanvas.SetActive(false);
            MainCanvas.SetActive(true);
            singleplayerCanvas.SetActive(true);
            multiplayerCanvas.SetActive(true);
            netmangager.SetupMenuButtons();
            singleplayerCanvas.SetActive(false);
            Multicv();
        }
    }*/
    private void Update()
    {
        if (MainCanvas.activeSelf && !button_set)
        {
            button_set = true;
            netmanager = GameObject.Find("NetworkManager").GetComponent<CustomNetManager>();
            netmanager.SetupMenuButtons();
        }

        if (MainCanvas.activeSelf)
        {
            if (options_shown)
                Options.SetActive(true);
            else
                Options.SetActive(false);
        }
        else
        {
            options_shown = false;
        }
    }
    public void Launchbtn(string Scene)
    {
        LoadCv();
        SceneManager.LoadSceneAsync(Scene);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void Singlecv()
    {
        MainCanvas.SetActive(false);
        singleplayerCanvas.SetActive(true);
        netmanager.SetupSinglePlayerButtons();
    }

    public void Multicv()
    {
        MainCanvas.SetActive(false);
        multiplayerCanvas.SetActive(true);
        netmanager.SetupMultiplayerButtons();
    }

    public void LoadCv()
    {
        singleplayerCanvas.SetActive(false);
        loadingCanvas.SetActive(true);
    }

    public void Return()
    {
        if (singleplayerCanvas.activeSelf == true)
        {
            singleplayerCanvas.SetActive(false);
        }
        else if(loadingCanvas.activeSelf == true)
        {
            loadingCanvas.SetActive(false);
        }
        else
        {
            multiplayerCanvas.SetActive(false);
        }

        MainCanvas.SetActive(true);
    }

    public void Show_options()
    {
        options_shown = true;
    }
}
