using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

	public GameObject MainCanvas;
	public GameObject singleplayerCanvas;
	public GameObject multiplayerCanvas;

    public void Launchbtn(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

	public void Singlecv()
    {
        MainCanvas.SetActive(false);
        singleplayerCanvas.SetActive(true);
    }

    public void Multicv()
    {
        MainCanvas.SetActive(false);
        multiplayerCanvas.SetActive(true);
    }

    public void Return()
    {
        if (singleplayerCanvas.activeSelf == true)
        {
            singleplayerCanvas.SetActive(false);
        }
        else
        {
            multiplayerCanvas.SetActive(false);
        }
        MainCanvas.SetActive(true);
    }
}
