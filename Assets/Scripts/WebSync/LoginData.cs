using System;
using UnityEngine;

[Serializable]
public class LoginData : IJsonClass
{
    public string type;
    public string result;

    public void ProcessData(UnityEngine.Object caller)
    {
        Debug.Log(type);
        Debug.Log(result);
        if (type != "success")
            ((LoginScript)caller).message.text = result;
        else
        {
            StaticInfo.Token = result;
            ((LoginScript)caller).message.text = "Welcome back";
            //((LoginScript)caller).getUserInfo();
        }

    }
}
