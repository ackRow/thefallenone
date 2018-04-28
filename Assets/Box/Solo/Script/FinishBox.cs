using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag_Net : Box_Net
{

    // Donne un gain de vie au joueur

    public int level = 1;
    public override void Action(Player_Net p)
    {
        //p.finishLevel(level);

        Cursor.lockState = CursorLockMode.None;
        (GameObject.Find("PauseScript").GetComponent<PauseMenuScript>()).isActive = true;

        SceneManager.LoadScene("Menu");
        
        //Destroy(gameObject);
    }

}