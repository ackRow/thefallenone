using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverScript : MonoBehaviour {

    public Button button;
    public Text green_text;
    public Text red_text;

    void Start()
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });

        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });

        trigger.triggers.Add(entry);
        trigger.triggers.Add(exit);

    }

    public void OnPointerEnter(PointerEventData data)
    {
        StartCoroutine(FadeText(false, 0.25f, 1, 0, red_text));
        StartCoroutine(FadeText(false, 0.25f, 0, 1, green_text));
    }

    public void OnPointerExit(PointerEventData data)
    {
        StartCoroutine(FadeText(true, 0.25f, 1, 0, red_text));
        StartCoroutine(FadeText(true, 0.25f, 0, 1, green_text));
    }

    IEnumerator FadeText(bool fadeAway, float duration, int red, int green, Text text)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * 1 / duration)
            {
                text.color = new Color(red, green, 0, i);
                yield return null;
            }
            text.color = new Color(red, green, 0, 0);
        }

        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime * 1 / duration)
            {
                text.color = new Color(red, green, 0, i);
                yield return null;
            }
        }
    }
}
