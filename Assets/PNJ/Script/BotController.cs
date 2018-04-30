using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour {

    Bot bot;

    public List<Renderer> listToRender;
    public Material _forceField, _wallhack;

    bool triggerTarget = false;
    float angle;

    float distance;
    public float minDistance = 5;
    public float maxDistance = 20;

    public Human target;
    Vector3 forward = new Vector3(1.0f, 0, 0);

    int round = 50;

    bool isWallhack = false;

    // Use this for initialization
    void Start () {
        bot = GetComponent<Bot>();
        bot.Scope();
    }
	
	// Update is called once per frame
	void Update () {

        distance = Vector3.Distance(target.transform.position, transform.position);

        // Si la distance entre le bot et le personnage est superieur a minDistance alors il avant vers le player
        // Si il est trop loin il ne va pas vers le joueur non plus
        // Sinon il ne bouge pas

        triggerTarget = distance < maxDistance;

        if (distance < minDistance || !triggerTarget)
            bot.Forward(false, Vector3.zero);
        else
            bot.Forward(false, forward);
            
        if (target is Player)
        {
            Player p = (Player)target;

            if (isWallhack != p.hasWallhack)
            {
                isWallhack = p.hasWallhack;
                activeWallhack(p.hasWallhack);

            }
        }
    }

    void activeWallhack(bool active)
    {
        if (active)
        {
            foreach (Renderer render in listToRender)
            {
                render.material = _wallhack;
            }
        }
        else
        {
            foreach (Renderer render in listToRender)
            {
                render.material = _forceField;
            }
        }
    }

    private void FixedUpdate()
    {
        // Si le joueur est trigger par le bot
        if (triggerTarget)
        {
            Vector3 targetDir = target.transform.position - transform.position;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1.0f, 0.0f);
            //Debug.DrawRay(bot.transform.position + new Vector3(0, 1f, 0), newDir, Color.red, 0.1f, true);
            //Debug.DrawRay(transform.position + new Vector3(0, 1.1f, 0), newDir, Color.red, 0.1f, true);


            if (!bot.dead)
            {
                Quaternion rotateY = Quaternion.LookRotation(newDir);

                if (distance < maxDistance)
                {
                    transform.rotation = new Quaternion(transform.rotation.x, rotateY.y, transform.rotation.z, rotateY.w);
                    bot.Attack(bot.transform.position + new Vector3(0, 1.1f, 0), newDir);
                }

                //transform.localRotation = Quaternion.AngleAxis(Quaternion.LookRotation(newDir).y, transform.up);
            }
        }
        /*else
        {   // Routine
            if (round == 100)
            {
                angle += 180.0f;

                bot.transform.rotation = Quaternion.AngleAxis(angle, bot.transform.up); // rotate

                round = 0;
            }

            round++;
        }*/
        
        
    }
}
