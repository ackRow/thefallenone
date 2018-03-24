using UnityEngine.Networking;
using UnityEngine;

public class moveLook_Net : NetworkBehaviour
{

    Vector2 mouseLook;
    Vector2 smoothV;

    public float sensivity = 2.0f;
    public float smoothing = 2.0f;

    public float minimumY = -60F;
    public float maximumY = 60F;


    GameObject character;

    private int idleStateHash = Animator.StringToHash("Base Layer.Idle");
    private int bendStateHash = Animator.StringToHash("Base Layer.Bending");

    public bool local = true;

    // Use this for initialization
    void Start()
    {

        character = this.transform.parent.gameObject;     

        if (!local) // On desactive la camera pour les autres joueurs multi
        {
            Camera cam = gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
            cam.enabled = false;
            cam.GetComponent<AudioListener>().enabled = false;
            Destroy(this);
            return;
        }
        
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

        mouseLook.y = Mathf.Clamp(mouseLook.y, minimumY, maximumY);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);


        // Pour le moment on appuie sur echap pour avoir le curseur
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetKeyDown(KeyCode.Return))
            Cursor.lockState = CursorLockMode.Locked;
    }
}
