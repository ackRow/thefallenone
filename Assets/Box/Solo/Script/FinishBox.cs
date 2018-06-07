using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishBox : Box
{

    // Donne un gain de vie au joueur

    public int level = 1;

    public new void Update()
    {
    }

    public override void Action(Player p)
    {
        p.finishLevel(level);

        if (level == 1)
        {
            SceneManager.LoadScene("Level2");
        }
        else if (level == 2)
        {
            SceneManager.LoadScene("Level3");
        }
        else
        {

            Cursor.lockState = CursorLockMode.None;
            (GameObject.Find("PauseScript").GetComponent<PauseMenuScript>()).isActive = true;

            SceneManager.LoadScene("Menu");
        }

        //Destroy(gameObject);
    }

}