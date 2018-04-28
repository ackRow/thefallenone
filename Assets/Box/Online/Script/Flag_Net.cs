using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag_Net : Box_Net
{

    public override void Action(Player_Net p)
    {
        p.Win();

        /*Cursor.lockState = CursorLockMode.None;
        (GameObject.Find("PauseScript").GetComponent<PauseMenuScript>()).isActive = true;

        SceneManager.LoadScene("Menu");*/
    }

}