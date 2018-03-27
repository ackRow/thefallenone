using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerController : MonoBehaviour {

    private Camera fpsCam; // Get direction using camera
    Player player;

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
    }
	
	// Update is called once per frame
	void Update () { // Input

        /* Mouvement */
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
            player.Jump();


        player.Forward(Input.GetKey(KeyCode.LeftShift), //CrossPlatformInputManager.GetButtonDown("Run"), 
            new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")));

        /* Attack */

        if (Input.GetMouseButton(0)) // clic gauche
            player.Attack(fpsCam.transform.position, fpsCam.transform.forward);

        if (Input.GetMouseButtonDown(1)) // clic droit
            player.Scope();


    }
}
