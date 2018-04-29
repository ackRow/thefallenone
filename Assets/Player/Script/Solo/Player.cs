using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // faut utiliser l'UI

public class Player : Human { // Hérite de la classe human

    private Slider playerHealth;

    private Animator ArmAnimator; // les mains en vue fps

    private PlayerController controller;

    public bool FPSView = true;

    private GameObject gun;
    public GameObject head;

    public GameObject[] ArmExt; // last object is the gun

    public GameObject[] ArmFPS;

    private Vector3 spawnPoint;

    public bool hasWallhack = false;

    public AudioSource myAudio;

    // Sync server

    public LoginData _login;
    public UserData _user;

    // Use this for initialization
    new void Start () {
        base.Start();

        myAudio = GetComponent<AudioSource>();

        controller = GetComponent<PlayerController>();
        ArmAnimator = GetComponentsInChildren<Animator>()[1];

        if (FPSView)
        {
            head.GetComponent<Renderer>().enabled = false; // On cache la tête du joueur (car vue FPS)
            foreach (var obj in ArmExt)
            {
                obj.GetComponent<Renderer>().enabled = false;
            }

            gun = ArmFPS[ArmFPS.Length - 1];
        }
        else
        {
            head.GetComponent<Renderer>().enabled = true; // On cache la tête du joueur (car vue FPS)
            foreach (var obj in ArmFPS)
            {
                obj.GetComponent<Renderer>().enabled = false;
            }

            gun = ArmExt[ArmExt.Length - 1];
        }

        playerHealth = FindObjectsOfType<Slider>()[0]; // On recupère le slider

        spawnPoint = transform.position;

        if (StaticInfo.Username != "")
            username = StaticInfo.Username;
        else
            username = "Offline Player";
    }
	
	// Update is called once per frame
	new void Update () {

        //Debug.Log(walking_speed);

        /* Animation FPS Arm */
        Animate(ArmAnimator);

        base.Update();

        /* UI */
        playerHealth.value = health;

        /* Hide and Show gun */
        gun.GetComponent<Renderer>().enabled = hasGun && isScoping;

    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public override void Stand()
    {
        base.Stand();
        // Call camera change in PlayerController
        if(!crouching)
            controller.adjustingCamera(false);
    }

    public void Heal(float value)
    {
        if (dead)
            return;

        health += value;
        if (health > 100.0f)
            health = 100.0f;
    }
    
    public override void Die()
    {
        base.Die();
        controller.resetCamera(true);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5); // delay

        dead = false;
        controller.resetCamera(false);
        // On relance l'animation Idle et on remet la vie à 100
        _animator.Play("Idle", -1, 0f);
        _body.MovePosition(spawnPoint);
        health = 100f;
    }

    public void PlaySound(AudioClip sound, bool loop, float volume)
    {
        myAudio.clip = sound;
        myAudio.volume = volume;
        myAudio.loop = loop;
        myAudio.Play();
    }

    public void getReward(int coin)
    {
        if (StaticInfo.Token == "")
            return;

        WWWForm form = new WWWForm();
        form.AddField("token", StaticInfo.Token);
        form.AddField("reward", coin.ToString());

        WWW www = new WWW("https://thefallen.one/sync/userInfo.php", form);

        StartCoroutine(WaitForRequest<UserData>(www));
    }

    public void finishLevel(int level)
    {
        if (StaticInfo.Token == "")
            return;

        WWWForm form = new WWWForm();
        form.AddField("token", StaticInfo.Token);
        form.AddField("level", level.ToString());

        WWW www = new WWW("https://thefallen.one/sync/userInfo.php", form);

        StartCoroutine(WaitForRequest<UserData>(www));
    }

    public void updateStat(StaticInfo.Stat stat)
    {
        if (StaticInfo.Token == "")
            return;

        WWWForm form = new WWWForm();
        form.AddField("token", StaticInfo.Token);
        form.AddField(stat.ToString(), 1);

        WWW www = new WWW("https://thefallen.one/sync/userInfo.php", form);

        StartCoroutine(WaitForRequest<UserData>(www));
    }


    public void getUserInfo(string token)
    {
        if (StaticInfo.Token == "")
            return;

        WWWForm form = new WWWForm();
        form.AddField("token", token);

        WWW www = new WWW("https://thefallen.one/sync/userInfo.php", form);

        StartCoroutine(WaitForRequest<UserData>(www));
    }
    IEnumerator WaitForRequest<T>(WWW data)
    {
        yield return data; // Wait until the download is done
        if (data.error != null)
        {
            Debug.Log("There was an error sending request: " + data.error);
        }
        else
        {
            T jsonClass = JsonUtility.FromJson<T>(data.text);
            ((IJsonClass)jsonClass).ProcessData(this);
        }
    }


}
