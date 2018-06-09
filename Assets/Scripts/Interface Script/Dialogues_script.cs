using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogues_script : MonoBehaviour {

    string dial_lvl1;
    string dial_lvl2;
    string dial_lvl3;
    string dial_lvl4;
    string dial_lvl5;

    Text dialogue;

    // Use this for initialization
    void Start () {
        dialogue = GameObject.Find("Dialogue").GetComponent<Text>();
        dial_lvl2 = "On dirait les bureaux de la prison, la sortie n'est peut-être pas très loin...";

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            dialogue.text = dial_lvl2;
            StartCoroutine(FadeText(false, 3f));
            StartCoroutine(Delay(3));
            StartCoroutine(FadeText(true, 3f));
        }
    }

    IEnumerator FadeText(bool fadeAway, float duration)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * 1/duration)
            {
                dialogue.color = new Color(1, 1, 1, i);
                yield return null;
            }
            dialogue.color = new Color(1, 1, 1, 0);
        }

        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime * 1/duration)
            {
                dialogue.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    IEnumerator Delay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
