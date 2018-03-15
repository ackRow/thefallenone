using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public void Startbtn(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
