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
        }

        if (healthbar.value <= 0)
        {
            StartCoroutine(FadeText(false));
            StartCoroutine(FadeText(true));
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

    IEnumerator FadeText(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 5f; i >= 0; i -= Time.deltaTime)
            {
                deathtext.color = new Color(1, 0, 0, i);
                yield return null;
            }
        }

        else
        {
            for (float i = 5f; i <= 1; i += Time.deltaTime)
            {
                deathtext.color = new Color(1, 0, 0, i);
                yield return null;
            }
        }
    }
}
