using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class CJooTest : MonoBehaviour 
{
    string id = "asdf2";
    string pw = "asdf2";
	// Use this for initialization
	void Start () 
    {
        InitialBackend();
       CustomLogin();
        //CustomSignUp();
        Logout();
        CustomLogin();
	}

    void InitialBackend()
    {
        if (!Backend.IsInitialized)
        {
            Backend.Initialize(backendCallback);
        }
        else
        {
            backendCallback();
        }
    }

    // 초기화 함수 이후에 실행되는 callback 
    void backendCallback()
    {
        // 초기화 성공한 경우 실행
        if (Backend.IsInitialized)
        {
            // example 
            // 구글 해시키 획득 
            if (!Backend.Utils.GetGoogleHash().Equals(""))
                Debug.Log(Backend.Utils.GetGoogleHash());

            // 서버시간 획득
            Debug.Log(Backend.Utils.GetServerTime());

        }
        // 초기화 실패한 경우 실행 
        else
        {

        }
    }
    void Update()
    {
       
    }

    public void CustomSignUp()
    {
        Debug.Log("-------------CustomSignUp-------------");
        Debug.Log(Backend.BMember.CustomSignUp(id, pw, "tester").ToString());
    }
    public void CustomLogin()
    {
        Debug.Log("-------------CustomLogin-------------");
        BackendReturnObject isComplete = Backend.BMember.CustomLogin(id, pw);
        Debug.Log(isComplete);
    }
    public void Logout()
    {
        Debug.Log("-------------Logout-------------");
        Debug.Log(Backend.BMember.Logout().ToString());
    }

}












public class CJooBackendReturnValues
{
    BackendReturnObject bro = new BackendReturnObject();
    bool isSuccess = false;

    public BackendReturnObject Bro
    {
        get
        {
            return bro;
        }
    }
    public bool IsSuccess
    {
        get
        {
            return isSuccess;
        }
        set
        {
            isSuccess = value;
        }
    }
}

public class CJooBackendCommand
{
   
    protected CJooBackendReturnValues brvs = null;
    public CJooBackendReturnValues Brvs
    {
        get
        {
            return brvs;
        }
    }

    public CJooBackendCommand(CJooBackendReturnValues pBrvs)
    {
        brvs = pBrvs;
    }

    public IEnumerator CoBackendCommand()
    {
        Command();
        while(true)
        {
            if (brvs.IsSuccess)
            {
                Backend.BMember.SaveToken(brvs.Bro);
                brvs.IsSuccess = false;
                brvs.Bro.Clear();
                yield break;
            }
            else
            {
                yield return null;
            }
        }
    }

    public void Command()
    {
        
    }
}