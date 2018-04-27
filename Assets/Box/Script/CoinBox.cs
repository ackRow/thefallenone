using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : Box {

    // Donne un gain de vie au joueur

    public int reward = 10;

   public override void Action(Player p)
    {
        if (StaticInfo.Token != "")
            p.getReward(StaticInfo.Token, reward);
        Destroy(gameObject);
    }

}
