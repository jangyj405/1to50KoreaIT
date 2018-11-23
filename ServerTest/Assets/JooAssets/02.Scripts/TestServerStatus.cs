using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;



public class TestServerStatus : MonoBehaviour
{
	// Use this for initialization
	void Start () 
    {
        
        if(Backend.IsInitialized == false)
        {
            Backend.Initialize(() => { });
        }

        BackendReturnObject bro = Backend.Utils.GetServerStatus();
        Debug.Log(bro.ToString());
        string rv = bro.GetReturnValue();
        Debug.Log(rv);
        
       serverStat stat = JsonUtility.FromJson<serverStat>(rv);
       Debug.Log(stat.serverStatus);
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}

public class serverStat
{
    public int serverStatus = 100;
}