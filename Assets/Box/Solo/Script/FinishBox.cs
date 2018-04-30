using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishBox : Box
{

    // Donne un gain de vie au joueur

    public int level = 1;

    public new void Update()
    {
       // transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

    }

    public override void Action(Player p)
    {
        p.finishLevel(level);

        Cursor.lockState = CursorLockMode.None;
        (GameObject.Find("PauseScript").GetComponent<PauseMenuScript>()).isActive = true;

        SceneManager.LoadScene("Menu");

        //Destroy(gameObject);
    }

}