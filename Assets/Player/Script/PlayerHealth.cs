using UnityEngine.UI; // faut utiliser l'UI
using UnityEngine.Networking;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{

    public Slider healthbar;
    Player player;

    // Use this for initialization
    void Start() {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
        player = GetComponent<Player>();
        healthbar.value = player.target.health;
        
    }
	
	// Update is called once per frame
	void Update () {
        healthbar.value = player.target.health;

    }
}
