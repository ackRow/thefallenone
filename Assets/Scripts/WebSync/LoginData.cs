using System;
using UnityEngine;

[Serializable]
public class LoginData : IJsonClass
{
    public string type;
    public string result;

    public void ProcessData(UnityEngine.Object caller)
    {
        if (type != "success")
            Debug.Log(result);
        else
        {
            ((Player)caller).Token = result;
            ((Player)caller).getUserInfo();
        }

    }
}
