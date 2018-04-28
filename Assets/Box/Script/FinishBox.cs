using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBox : Box
{

    // Donne un gain de vie au joueur

    public int level = 1;

    public override void Action(Player p)
    {
        p.finishLevel(level);
    }

}