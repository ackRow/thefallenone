using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : Box {

    // Donne un gain de coin

    public int reward = 10;

   public override void Action(Player p)
    {
        p.getReward(reward);
        Destroy(gameObject);
    }

}
