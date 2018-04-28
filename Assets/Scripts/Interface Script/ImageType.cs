using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageType : MonoBehaviour {

    public Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        image.type = Image.Type.Filled;
        image.fillAmount = 0.1f;
        image.fillMethod = Image.FillMethod.Radial360;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
