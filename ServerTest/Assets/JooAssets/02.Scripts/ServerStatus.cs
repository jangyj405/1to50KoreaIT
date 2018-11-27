using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;



public static class ServerStatus
{
    // Use this for initialization
    public static bool CheckServerStatus()
    {

        if (Backend.IsInitialized == false)
        {
            Backend.Initialize(() => { });
        }

        BackendReturnObject bro = Backend.Utils.GetServerStatus();
        Debug.Log(bro.ToString());
        string rv = bro.GetReturnValue();
        Debug.Log(rv);

        ServerStat stat = JsonUtility.FromJson<ServerStat>(rv);
        Debug.Log(stat.serverStatus);

        switch (stat.serverStatus)
        {
            case 0:
                Debug.Log("Server Online");
                return true;
            case 1:
                Debug.Log("Server Offline");
                return false;
            case 2:
                Debug.Log("Server Testing");
                return false;
            default:
                return false;
        }
    }
}

public class ServerStat
{
    public int serverStatus = -1;
}