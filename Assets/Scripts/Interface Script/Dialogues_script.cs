using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogues_script : MonoBehaviour {

    string[] dial_lvl1 = new string[3];
    string dial_lvl2;
    string dial_lvl3;
    string dial_lvl4;
    string[] dial_lvl5 = new string[2];
    string time;
    
    Text dialogue;

    // Use this for initialization
    void Start () {
        dialogue = GameObject.Find("Dialogue").GetComponent<Text>();
        dial_lvl2 = "On dirait les bureaux de la prison, la sortie n'est peut-être pas très loin...";

        dial_lvl1[0] = "Cela fait plus de 3 mois que je suis enfermé ici,";
        dial_lvl1[1] = "mes pouvoirs étants considérés comme dangereux pour le maintiens de la dictature;";
        dial_lvl1[2] = "Mais aujourd'hui la porte de ma cellule à bugué...";

        dial_lvl3 = "On dirait une manifestation, les citoyens ont-ils enfin compris qu'ils se trouvent dans une dictature ?";

        dial_lvl4 = "Il y a donc un autre monde en dehors de la dictature, je me sens bien plus à ma place ici...";

        dial_lvl5[0] = "Le chef des glitcher est enfait un psychopathe, il veut éradiquer tous les non-mutants;";
        dial_lvl5[1] = "Il faut à tout prix que je l'en empêche";

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            dialogue.text = dial_lvl2;
            StartCoroutine(Delay(3));
        }

        if (SceneManager.GetActiveScene().name == "Level3")
        {
            dialogue.text = dial_lvl3;
            StartCoroutine(Delay(3));
        }

        if (SceneManager.GetActiveScene().name == "Level4")
        {
            dialogue.text = dial_lvl4;
            StartCoroutine(Delay(3));
        }

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            dialogue.text = dial_lvl1[0];
            StartCoroutine(Delay_lvl1());
        }

        if (SceneManager.GetActiveScene().name == "Level5")
        {
            dialogue.text = dial_lvl5[0];
            StartCoroutine(Delay_lvl5());
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

    IEnumerator Delay(float seconds)
    {
        StartCoroutine(FadeText(false, seconds));
        yield return new WaitForSeconds(seconds);
        StartCoroutine(FadeText(true, seconds));
    }

    IEnumerator Delay_lvl5()
    {
        StartCoroutine(Delay(1.5f));
        yield return new WaitForSeconds(3);
        dialogue.text = dial_lvl5[1];
        StartCoroutine(Delay(1));
    }

    IEnumerator Delay_lvl1()
    {
        StartCoroutine(Delay(1.5f));
        yield return new WaitForSeconds(3);
        dialogue.text = dial_lvl1[1];
        StartCoroutine(Delay(1.5f));
        yield return new WaitForSeconds(3);
        dialogue.text = dial_lvl1[2];
        StartCoroutine(Delay(1.5f));
        yield return new WaitForSeconds(3);
    }

}
