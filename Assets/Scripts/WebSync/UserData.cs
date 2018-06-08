using System;
using UnityEngine;

[Serializable]
public class UserData : IJsonClass
{
    public string type;
    public string username;
    public string coin;
    public string level;
    public string perso;

    public bool ProcessData(UnityEngine.Object caller)
    {
        if (type != "success")
        {
            StaticInfo.Token = "";
            if(caller is LoginScript)
                ((LoginScript)caller).message.text = "Session expired";
            Debug.Log("couldn't retrieve user information");
        }
        else
        {
            //((Player)caller).username = username;
            StaticInfo.Username = username;
            StaticInfo.Coin = Int32.Parse(coin);
            StaticInfo.Level = Int32.Parse(level);
            StaticInfo.Perso = Int32.Parse(perso);
            return true;
        }
        return false;
    }
}
