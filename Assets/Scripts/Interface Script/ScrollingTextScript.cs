using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollingTextScript : MonoBehaviour {

    public float scrollspeed = 0.05f;
    ScrollRect textzone;
    Text text;
    GameObject thx;

    bool ispassed;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        thx = GameObject.Find("Thx");
        thx.SetActive(false);
        textzone = GameObject.Find("Container").GetComponent<ScrollRect>();
        text = GameObject.Find("Text").GetComponent<Text>();
    }

    private void Update()
    {
        if (text.transform.position.y < 920f)
        {
            float pos = textzone.verticalNormalizedPosition;

            pos -= scrollspeed * Time.deltaTime;

            textzone.verticalNormalizedPosition = pos;
        }
        else
        {
            thx.SetActive(true);
        }

        if (Input.GetKeyDown("escape") || Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("Menu");
    }
}
