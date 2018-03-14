using UnityEngine.Networking;
using UnityEngine;

public class moveLook : NetworkBehaviour
{

    Vector2 mouseLook;
    Vector2 smoothV;

    public float sensivity = 2.0f;
    public float smoothing = 2.0f;

    public float minimumY = -60F;
    public float maximumY = 60F;


    GameObject character;
    //[SerializeField]
    //private Animator animator;

    private int idleStateHash = Animator.StringToHash("Base Layer.Idle");
    private int bendStateHash = Animator.StringToHash("Base Layer.Bending");

    public bool local = true;



    // Use this for initialization
    void Start()
    {

        character = this.transform.parent.gameObject;
        // On affiche pas le curseur en jeu
        Cursor.lockState = CursorLockMode.Locked;

        if (!local)
        {
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
            Destroy(this);
            return;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensivity * smoothing, sensivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, minimumY, maximumY);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

        

        //transform.position = Vector3.MoveTowards(transform.position, parent.transform.position, 0.03f); Vu TPS

       /* if (animator)
        {
            

            if(animator.GetCurrentAnimatorStateInfo(0).nameHash == idleStateHash)
            {
                animator.Play(bendStateHash);
                //animator.speed = 0;
            }
            else
            {
                //animator.enabled = true;
            }



        } */

        // Pour le moment on appuie sur echap pour avoir le curseur
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetKeyDown(KeyCode.Return))
            Cursor.lockState = CursorLockMode.Locked;
    }
}
