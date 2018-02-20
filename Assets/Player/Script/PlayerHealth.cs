using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // faut utiliser l'UI

public class PlayerHealth : MonoBehaviour {

    public int initialHealth = 100;
    public int health; //ta variable de vie
    public Slider healthbar;

    // Use this for initialization
    void Start() {
        health = initialHealth; // c'est à toi de le faire ça :P
        healthbar.value = health;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
