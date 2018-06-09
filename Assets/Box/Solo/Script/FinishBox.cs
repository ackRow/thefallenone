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
        else if (level == 3)
        {
            SceneManager.LoadScene("Level4");
        }
        else if (level == 4)
        {
            SceneManager.LoadScene("Level5");
        }
        else if (level == 5)
        {
            SceneManager.LoadScene("générique");
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