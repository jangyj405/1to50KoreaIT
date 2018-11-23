using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CJooLogin : MonoBehaviour
{
    public GameObject signup = null;
    public InputField signupID = null;
    public InputField signupPW = null;

    public GameObject login = null;
    public InputField loginID = null;
    public InputField loginPW = null;

    bool m_isSucceeded = false;
    bool isSucceeded
    {
        get
        {
            return m_isSucceeded;
        }
        set
        {
            m_isSucceeded = value;
            if(m_isSucceeded == true)
            {
                SceneManager.LoadScene("03_MainMenu");
            }
        }
    }
    void Start()
    {
        InitialBackend();
        TryLogin();
    }

    void Update()
    {

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

    void TryLoginTokenSync()
    {
        if(isSucceeded == true)
        {
            return;
        }
        Debug.Log("-------------LoginWithTheBackendToken-------------");
        BackendReturnObject bro = Backend.BMember.LoginWithTheBackendToken();
        string statuscode = bro.GetStatusCode();
        int statuscodeInt = Convert.ToInt32(statuscode);

        switch (statuscodeInt)
        {
            case 410:
                {
                    Debug.Log("1년이 지나 refresh_token이 만료");
                }
                break;
            case 401:
                {
                    Debug.Log("다른 기기로 로그인 하여 refresh_token이 만료");
                }
                break;

            case 403:
                {
                    Debug.Log("차단당한 유저");
                }
                break;

            default:
                isSucceeded = true;
                break;
        }

        Debug.Log(bro.ToString());
    }

    void TryLoginPrefsSync()
    {
        if (isSucceeded == true)
        {
            return;
        }
        Debug.Log("-------------LoginWithPrefs-------------");
        string ID, PW;
        GetAccountFromPrefs(out ID, out PW);
        if(ID.Equals("") || PW.Equals(""))
        {
            Debug.Log("Prefs에 저장된 아이디가 없음");
            return;
        }
        CustomLogin(ID, PW);
    }

    public void OnClickBtnDisplaySignUp()
    {
        signup.SetActive(true);
    }

    void GetAccountFromPrefs(out string oID, out string oPW)
    {
        string tID = PlayerPrefs.GetString("UserID", "");
        string tPW = PlayerPrefs.GetString("UserPW", "");
        oID = tID;
        oPW = tPW;
    }

    public void CustomLogin(string ID, string PW)
    {
        Debug.Log("-------------CustomLogin-------------");
        BackendReturnObject isComplete = Backend.BMember.CustomLogin(ID, PW);
        Debug.Log(isComplete);

        string statuscode = isComplete.GetStatusCode();
        int statuscodeInt = Convert.ToInt32(statuscode);

        switch (statuscodeInt)
        {
            case 401:
                {
                    Debug.Log("아이디가 존재하지 않음");
                }
                break;

            case 403:
                {
                    Debug.Log("차단당한 유저");
                }
                break;

            default:
                isSucceeded = true;
                break;
        }
    }

    public void OnClickBtnTryLogIn()
    {
        string tID = "";
        string tPW = "";
        tID = loginID.text;
        tPW = loginPW.text;

        if (tID == "")
        {
            Debug.Log("아이디를 입력해라");
        }
        else if (tPW == "")
        {
            Debug.Log("패스워드를 입력해라");
        }
        else
        {
            CustomLogin(tID, tPW);
        }
    }

    public void OnClickTryCreateID()
    {
        string tID = "";
        string tPW = "";
        tID = signupID.text;
        tPW = signupPW.text;

        if(tID == "")
        {
            Debug.Log("아이디를 입력해라");
        }
        else if(tPW == "")
        {
            Debug.Log("패스워드를 입력해라");
        }
        else
        {
            signup.SetActive(false);
            BackendReturnObject bro = null;
            bro = CustomSignUp(tID, tPW);

            string statuscode = bro.GetStatusCode();
            int statuscodeInt = Convert.ToInt32(statuscode);

            switch (statuscodeInt)
            {
                case 409:
                    {
                        Debug.Log("중복 ID");
                        signup.SetActive(true);
                    }
                    break;
               
                default:
                    PlayerPrefs.SetString("UserID", tID);
                    PlayerPrefs.SetString("UserPW", tPW);
                    isSucceeded = true;
                    break;
            }

            Debug.Log(bro.ToString());
        }
    }

    // 커스텀 가입
    BackendReturnObject CustomSignUp(string id, string pw)
    {
        Debug.Log("-------------CustomSignUp-------------");
        BackendReturnObject tBro = Backend.BMember.CustomSignUp(id, pw, "tester");
        Debug.Log(tBro.ToString());
        return tBro;
    }

    void TryLogin()
    {
        TryLoginTokenSync();
        TryLoginPrefsSync();
        if(isSucceeded == false)
        {
            login.SetActive(true);
        }
    }
}
