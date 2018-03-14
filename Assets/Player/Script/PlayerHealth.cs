using UnityEngine.UI; // faut utiliser l'UI
using UnityEngine.Networking;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{

    public Slider healthbar;
    //Player player;

    // Useless for now
    void Start() {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
        healthbar = GetComponent<Slider>();
        //player = GetComponent<Player>();
       // healthbar.value = 10.0f;
        
    }
	
	// Update is called once per frame
	void Update () {
        //healthbar.value = player.target.health;

    }

}
