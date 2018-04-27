using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerController : MonoBehaviour {

    public GameObject FPSContainer;

    private Camera fpsCam; // Get direction using camera
    Player player;

    private Quaternion camRot;
    //private Vector3 FPSpos;

    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
        if (Camera.main != null)
        {
            fpsCam = GetComponentInChildren<Camera>();
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        camRot = fpsCam.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () { // Input

        /* Mouvement */
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
            player.Jump();

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(player.canStand || !player.crouching)
                adjustingCamera(player.Crouch());
        }


        if (Input.GetKeyDown(KeyCode.I))
        {
            player.TakeDamage(100, player);
        }


        player.Forward(Input.GetKey(KeyCode.LeftShift), //CrossPlatformInputManager.GetButtonDown("Run"), 
            new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")));

        /* Attack */

        if (Input.GetMouseButton(0))
        { // clic gauche
            player.Attack(fpsCam.transform.position, fpsCam.transform.forward);
        }

        if (Input.GetMouseButtonDown(1)) // clic droit
            player.Scope();


    }

    public void resetCamera(bool dead)
    {
        if (dead)
        {
            camRot = fpsCam.transform.rotation;
            fpsCam.transform.Rotate(new Vector3(-40, 0, 0));
        }
        else
            fpsCam.transform.rotation = camRot;
    }

    public void adjustingCamera(bool crouching)
    {
        if (crouching)
        {
            //FPSContainer.transform.Translate(new Vector3(0, 0.0f, 0.2f));
            FPSContainer.transform.localPosition += new Vector3(0, 0.0f, 0.2f);

            fpsCam.transform.localPosition += new Vector3(0.02f, -0.30f, 0.07f);
        }

        else
        {
            //FPSContainer.transform.Translate(new Vector3(0, 0.0f, -0.2f));
            FPSContainer.transform.localPosition += new Vector3(0, 0.0f, -0.2f);
            fpsCam.transform.localPosition += new Vector3(-0.02f, +0.30f, -0.07f);
        }
    }
}
