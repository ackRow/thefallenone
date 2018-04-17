using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour {

    Bot bot;

    bool triggerTarget = true;
    float angle;

   public Human target;

    Vector3 forward = new Vector3(1.0f, 0, 0);

    int round = 50;

    // Use this for initialization
    void Start () {
        bot = GetComponent<Bot>();

        bot.Scope();
    }
	
	// Update is called once per frame
	void Update () {
        
        bot.Forward(false, forward); // go forward  
        
    }

    private void FixedUpdate()
    {

        if (triggerTarget)
        {
            Vector3 targetDir = target.transform.position - transform.position;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1.0f, 0.0f);
            //Debug.DrawRay(bot.transform.position + new Vector3(0, 1f, 0), newDir, Color.red, 0.1f, true);
            //Debug.DrawRay(transform.position, newDir, Color.red);


            if (!bot.dead)
            {
                transform.rotation = Quaternion.LookRotation(newDir);
                bot.Attack(bot.transform.position + new Vector3(0, 1f, 0), newDir);
            }
        }
        else
        {   // Routine
            if (round == 100)
            {
                angle += 180.0f;

                bot.transform.rotation = Quaternion.AngleAxis(angle, bot.transform.up); // rotate

                round = 0;
            }

            round++;
        }
        
        
    }
}
