﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mooveleft : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(Time.deltaTime*-4, 0, 0));
	}
}
