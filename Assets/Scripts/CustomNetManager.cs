using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomNetManager : NetworkManager {

    public Dropdown mapselect;

	public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.onlineScene = mapselect.options[mapselect.value].text;
        NetworkManager.singleton.StartClient();
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
}
