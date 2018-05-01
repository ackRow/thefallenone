using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour {

    public InputField login;
    public InputField password;
    public Button connect;
    public Button register;
    public GameObject loadingGun;

    public GameObject loginCanvas;
    public GameObject MenuCanvas;

    public Text message;

	// Update is called once per frame
	void Awake () {
        loadingGun.SetActive(false);
        password.contentType = InputField.ContentType.Password;
        connect.onClick.RemoveAllListeners();
        connect.onClick.AddListener(Connection);
	}

    void Update()
    {
        if (StaticInfo.Token != "")
        {
            Debug.Log("Sending request");
            getUserInfo(StaticInfo.Token);
        }
    }

    void Connection()
    { 
        Login(login.text, password.text);
        
    }

    public void getUserInfo(string token)
    {
        WWWForm form = new WWWForm();
        form.AddField("token", token);

        WWW www = new WWW("https://thefallen.one/sync/userInfo.php", form);

        StartCoroutine(WaitForRequest<UserData>(www));
    }

    public void Login(string name, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", name);
        form.AddField("password", pass);

        WWW www = new WWW("https://thefallen.one/sync/login.php", form);

        StartCoroutine(WaitForRequest<LoginData>(www));
    }

    IEnumerator WaitForRequest<T>(WWW data)
    {
        connect.interactable = false;
        loadingGun.SetActive(true);
        yield return data; // Wait until the download is done
        if (data.error != null)
        {
            Debug.Log("There was an error sending request: " + data.error);
        }
        else
        {
            T jsonClass = JsonUtility.FromJson<T>(data.text);
            if (((IJsonClass)jsonClass).ProcessData(this) && loginCanvas.activeSelf == true)
            {
                loginCanvas.SetActive(false);
                MenuCanvas.SetActive(true);
            }
        }
        connect.interactable = true;
        loadingGun.SetActive(false);
    }

    public void Offline()
    {
        loginCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public void Register()
    {
        Application.OpenURL("http://thefallen.one/register.php");
    }
}
