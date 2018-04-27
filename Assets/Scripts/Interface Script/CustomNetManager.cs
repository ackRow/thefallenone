﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Sockets;


public class CustomNetManager : NetworkManager {

    public Dropdown mapselect;
    public MainMenuScript main;

    private void Update()
    {
        NetworkManager.singleton.onlineScene = mapselect.options[mapselect.value].text;
    }

    public void StartupHost()
    {
        try
        {
            UPnP.NAT.Discover();
            UPnP.NAT.ForwardPort(4761, ProtocolType.Tcp, "TFO");
        }
        catch
        {
            Debug.Log("UPnP failed");
        }
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("IPAdress").transform.Find("IP").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 4761;
    }

    /*void SceneManager.sceneLoaded()
    {

    }*/

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            SetupMenuButtons();
        }
        else
        {
            SetupOtherButtons();
        }
    }

    void LaunchSingleMap1()
    {
        main.Launchbtn("Level1");
    }

    void LaunchSingleMap2()
    {
        main.Launchbtn("Level2");
    }

    void SetupMenuButtons()
    {
        GameObject.Find("Single").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Single").GetComponent<Button>().onClick.AddListener(main.Singlecv);

        GameObject.Find("Multi").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Multi").GetComponent<Button>().onClick.AddListener(main.Multicv);

        GameObject.Find("ExitButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ExitButton").GetComponent<Button>().onClick.AddListener(main.ExitBtn);

        GameObject.Find("Map1Button").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Map1Button").GetComponent<Button>().onClick.AddListener(LaunchSingleMap1);

        GameObject.Find("Map2Button").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Map2Button").GetComponent<Button>().onClick.AddListener(LaunchSingleMap2);

        GameObject.Find("SingleBack").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("SingleBack").GetComponent<Button>().onClick.AddListener(main.Return);

        GameObject.Find("StartHostButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("StartHostButton").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("JoinButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("JoinButton").GetComponent<Button>().onClick.AddListener(JoinGame);

        GameObject.Find("BackMulti").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("BackMulti").GetComponent<Button>().onClick.AddListener(main.Return);
    }

    void SetupOtherButtons()
    {
        GameObject.Find("MainMenuButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);

        GameObject.Find("QuitButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("QuitButton").GetComponent<Button>().onClick.AddListener(Application.Quit);
    }
}
