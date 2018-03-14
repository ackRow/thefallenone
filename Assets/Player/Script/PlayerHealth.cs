using UnityEngine;
using UnityEngine.UI; // faut utiliser l'UI

public class PlayerHealth : MonoBehaviour {

    public Slider healthbar;
    Player player;

    // Use this for initialization
    void Start() {
        player = GetComponent<Player>();
        healthbar.value = player.target.health;
    }
	
	// Update is called once per frame
	void Update () {
        healthbar.value = player.target.health;

    }
}
