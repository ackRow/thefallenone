using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverScript : MonoBehaviour {

    public RawImage hoverimage;

    public void MouseOver()
    {
        StartCoroutine(FadeImage(false));
        Debug.Log("elle est dessus ta daronne !");
    }

    public void MouseExit()
    {
        StartCoroutine(FadeImage(true));
        Debug.Log("elle n'y est plus :P");
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 0.5f; i >= 0; i -= Time.deltaTime)
            {
                hoverimage.color = new Color(1, 1, 1, i);
                yield return null;
            }
            hoverimage.color = new Color(1, 1, 1, 0); // hide picture
        }

        else
        {
            for (float i = 0.5f; i <= 1.1; i += Time.deltaTime)
            {
                hoverimage.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

}
