using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnim : MonoBehaviour
{

    public Slider healthbar;

    public Text deathtext;
    public RawImage hit_indicator;
    private float player_health;
    public Text heal_box;
    public Text speed_box;
    public Text Coin_box;
    public Text Wall_box;
    public Text Damage_box;

    private void Awake()
    {
        player_health = healthbar.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_health != healthbar.value)
        {
            hit_indicator.color = new Color(1, 1, 1, 1);
            StartCoroutine(FadeImage(true));
            player_health = healthbar.value;
            hit_indicator.color = new Color(1, 1, 1, 0);
        }

        if (healthbar.value <= 0)
        {
            StartCoroutine(FadeText(false, 5f, deathtext, 1, 0, 0));
            StartCoroutine(FadeText(true, 5f, deathtext, 1, 0, 0));
        }
    }

    public void GetBoxFade(string box_name)
    {
        switch (box_name)
        {
            case "heal":
                StartCoroutine(FadeText(false, 1f, heal_box, 0, 1, 0));
                StartCoroutine(FadeText(true, 1f, heal_box, 0, 1, 0));
                break;
            case "speed":
                StartCoroutine(FadeText(false, 1f, speed_box, 0, 0, 1));
                StartCoroutine(FadeText(true, 1f, speed_box, 0, 0, 1));
                break;
            case "wall":
                StartCoroutine(FadeText(false, 1f, Wall_box, 0, 0, 1));
                StartCoroutine(FadeText(true, 1f, Wall_box, 0, 0, 1));
                break;
            case "coin":
                StartCoroutine(FadeText(false, 1f, Coin_box, 255, 213, 0));
                StartCoroutine(FadeText(true, 1f, Coin_box, 255, 213, 0));
                break;
            case "damage":
                StartCoroutine(FadeText(false, 1f, Damage_box, 1, 0, 0));
                StartCoroutine(FadeText(true, 1f, Damage_box, 1, 0, 0));
                break;
            default:
                Debug.Log("Wrong box name entered in parameter");
                break;
        }
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i=0.5f; i >= 0; i -= Time.deltaTime)
            {
                hit_indicator.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }

        else
        {
            for (float i = 0.5f; i <= 1; i += Time.deltaTime)
            {
                deathtext.color = new Color(1, 0, 0, i);
                yield return null;
            }
        }
    }

    IEnumerator FadeText(bool fadeAway, float duration, Text text, int red, int green, int blue)
    {
        if (fadeAway)
        {
            for (float i = duration; i >= 0; i -= Time.deltaTime)
            {
                text.color = new Color(red, green, blue, i);
                yield return null;
            }
        }

        else
        {
            for (float i = duration; i <= 1; i += Time.deltaTime)
            {
                text.color = new Color(red, green, blue, i);
                yield return null;
            }
        }
    }
}
