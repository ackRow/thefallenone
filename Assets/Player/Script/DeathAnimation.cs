using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathAnimation : MonoBehaviour {

    public Slider healthbar;
    public static DeathAnimation Instance { get; set; }

    public Text fadetext;
    private bool isInTransition;
    private float transition;
    private bool isShowing;
    private float duration;

    private void Awake()
    {
        Instance = this;
        healthbar = FindObjectsOfType<Slider>()[0]; // On recupère le slider
    }

    public void Fade(bool showing, float duration)
    {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }

    // Update is called once per frame
    void Update () {
		if (healthbar.value <= 0)
        {
            Fade(true, 0.8f);
            Fade(false, 0.8f);
        }

        if (!isInTransition)
        {
            return;
        }

        transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        fadetext.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, transition);

        if(transition > 1 || transition < 0)
        {
            isInTransition = false;
        }
	}
}
