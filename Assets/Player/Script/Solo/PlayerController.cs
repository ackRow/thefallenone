using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerController : MonoBehaviour {

    public GameObject FPSContainer;

    private Camera fpsCam; // Get direction using camera
    Player player;

    private Vector3 camPos;
    private Vector3 FPSpos;

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

        FPSpos = FPSContainer.transform.position;
        camPos = fpsCam.transform.position;
    }
	
	// Update is called once per frame
	void Update () { // Input

        /* Mouvement */
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
            player.Jump();

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            adjustingCamera(player.Crouch());
        }


        if (Input.GetKeyDown(KeyCode.I))
        {
            player.TakeDamage(100);
        }


        player.Forward(Input.GetKey(KeyCode.LeftShift), //CrossPlatformInputManager.GetButtonDown("Run"), 
            new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")));

        /* Attack */

        if (Input.GetMouseButton(0)) // clic gauche
            player.Attack(fpsCam.transform.position, fpsCam.transform.forward);

        if (Input.GetMouseButtonDown(1)) // clic droit
            player.Scope();


    }

    public void adjustingCamera(bool crouching)
    {
        if (crouching)
        {
            FPSContainer.transform.Translate(new Vector3(0, 0.0f, 0.2f));
            fpsCam.transform.Translate(new Vector3(0, -0.25f, 0.0f));
        }

        else
        {
            FPSContainer.transform.Translate(new Vector3(0, 0.0f, -0.2f));
            fpsCam.transform.Translate(new Vector3(0, +0.25f, 0.0f));
        }
    }
}
