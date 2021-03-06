﻿using UnityEngine;

public class moveLook : MonoBehaviour
{

    Vector2 mouseLook;
    Vector2 smoothV;

    public float sensivity = 2.0f;
    public float smoothing = 2.0f;

    public float minimumY = -60F;
    public float minimumY_crunch = -30F;
    public float maximumY = 60F;
    public float maximumY_crunch = 30F;


    GameObject character;
    Player player;

    //private int idleStateHash = Animator.StringToHash("Base Layer.Idle");
    //private int bendStateHash = Animator.StringToHash("Base Layer.Bending");

    // Use this for initialization
    void Start()
    {

        character = transform.parent.gameObject;
        player = character.GetComponent<Player>();

        // On affiche pas le curseur en jeu
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        
        // On tourne la camera et le joueur en fonction des mouvements de souris

        md = Vector2.Scale(md, new Vector2(sensivity * smoothing, sensivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, player.crouching ? minimumY_crunch : minimumY, player.crouching ? maximumY_crunch : maximumY);

        if (!player.dead && !(GameObject.Find("PauseScript").GetComponent<PauseMenuScript>()).isActive)
        {
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }


        // Pour le moment on appuie sur echap pour avoir le curseur
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetKeyDown(KeyCode.Return))
            Cursor.lockState = CursorLockMode.Locked;
    }
}
