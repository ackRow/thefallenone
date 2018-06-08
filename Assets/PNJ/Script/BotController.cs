using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{

    Bot bot;

    public List<Renderer> listToRender;
    public Material _forceField, _wallhack;
    public NavMeshAgent agent;

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
    void Start()
    {
        bot = GetComponent<Bot>();
        agent = GetComponent<NavMeshAgent>();
        bot.Scope();

        // Attribut du NavMeshAgent
        agent.speed = bot.walking_speed;
        agent.stoppingDistance = minDistance;
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(target.transform.position, transform.position);

        // Si la distance entre le bot et le personnage est superieur a minDistance alors il avant vers le player
        // Si il est trop loin il ne va pas vers le joueur non plus
        // Sinon il ne bouge pas

        if (distance < maxDistance)  // Une fois que le bot à agro le joueur, il le suivra toujours
            triggerTarget = true;

        /*if (distance < minDistance || !triggerTarget)
            bot.Forward(false, Vector3.zero);
        else
            bot.Forward(false, forward);*/

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
                if (render.name != "Glock_Eagle")
                    render.material = _wallhack;
            }
        }
        else
        {
            foreach (Renderer render in listToRender)
            {
                if(render.name != "Glock_Eagle")
                    render.material = _forceField;
            }
        }
    }

    private bool rayPlayer(Vector3 _position, Vector3 _direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(_position, _direction, out hit, maxDistance))
        {
            ITarget target = hit.transform.GetComponent<ITarget>();
            if (target != null) // Si un joueur est touché
            {
                return target is Player;
            }
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (bot.dead)
        {
            agent.destination = transform.position;
            return;
        }
        Vector3 targetDir = target.transform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 2.0f, 0.0f);  // 120degrés de champ de vision

        Debug.DrawRay(bot.transform.position + new Vector3(0, 1f, 0), newDir, Color.red, 0.1f, true);
        //Debug.DrawRay(transform.position + new Vector3(0, 1.1f, 0), newDir, Color.red, 0.1f, true);   


        if (rayPlayer(bot.transform.position + new Vector3(0, 0.8f, 0), newDir)) // ~ Le joueur est visible par le bot
        {
            //triggerTarget = true;
            agent.destination = target.Position;
            bot.walking = true;

            Quaternion rotateY = Quaternion.LookRotation(newDir);

            if (distance < maxDistance)
            {
                if (distance <= minDistance)
                {
                    transform.rotation = new Quaternion(transform.rotation.x, rotateY.y, transform.rotation.z, rotateY.w);
                    bot.walking = false;
                }


                if (rayPlayer(bot.transform.position + new Vector3(0, 0.8f, 0), bot.transform.forward)) // Le bot vise vers le joueur = il tir
                {
                    bot.Attack(bot.transform.position + new Vector3(0, 0.8f, 0), bot.transform.forward);
                }

                //transform.localRotation = Quaternion.AngleAxis(Quaternion.LookRotation(newDir).y, transform.up);
            }
        }
        else
        {
            bot.walking = false;
        }

    }
}
