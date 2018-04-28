using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishBox : Box
{

    // Donne un gain de vie au joueur

    public int level = 1;
    public override void Action(Player p)
    {
        p.finishLevel(level);

        Cursor.lockState = CursorLockMode.None;
        (GameObject.Find("PauseScript").GetComponent<PauseMenuScript>()).isActive = true;

        SceneManager.LoadScene("Menu");

        //Destroy(gameObject);
    }

}