using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour {

    Bot bot;

    bool hasRotate = true;
    float angle;

    int round = 50;

    // Use this for initialization
    void Start () {
        bot = GetComponent<Bot>();

        bot.Scope();
    }
	
	// Update is called once per frame
	void Update () {
        
        bot.Forward(false, new Vector3(1.0f,0 ,0)); // go forward  

    }

    private void FixedUpdate()
    {
        // Routine
        if (round == 100)
        {
            angle += 180.0f;

            hasRotate = true;
            bot.transform.rotation = Quaternion.AngleAxis(angle, bot.transform.up); // rotate

            round = 0;
        }

        round++;
    }
}
