using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour {

    public InputField login;
    public InputField password;
    public Button connect;
    public GameObject loginCanvas;
    public GameObject MenuCanvas;

    public GameObject revengegaytext;

    public Text message;

	// Update is called once per frame
	void Awake () {
        password.contentType = InputField.ContentType.Password;
        connect.onClick.RemoveAllListeners();
        connect.onClick.AddListener(Connection);
	}

    private void Update()
    {
        if (login.text == "Nexus" || login.text == "r0mi")
        {
            if (!revengegaytext.activeSelf)
            {
                revengegaytext.SetActive(true);
            }
        }
        else
        {
            if (revengegaytext.activeSelf)
            {
                revengegaytext.SetActive(false);
            }
        }
    }

    void Connection()
    {
        Login(login.text, password.text);
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
    }
}
