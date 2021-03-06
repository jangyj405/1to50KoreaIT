﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class CJooLogin : MonoBehaviour
{
    public GameObject signup = null;
    public InputField signupID = null;
    public InputField signupPW = null;

    public GameObject login = null;
    public InputField loginID = null;
    public InputField loginPW = null;

    public GameObject serverStatusPanel = null;

    public GameObject FailLogInPanel = null;
    public Text FailLogInText = null;

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
				//Debug.Log(Backend.GameInfo.GetTableList().GetReturnValue());
				bool hasSetNickname = IsNickNameSet();
				if(hasSetNickname)
				{
                    FadeInOut.instance.FadeIn(SceneNames.modeSelectScene);
                    //SceneManager.LoadScene(SceneNames.modeSelectScene);
				}
				else
				{
                    FadeInOut.instance.FadeIn(SceneNames.nicknameCreateScene);
                    //SceneManager.LoadScene(SceneNames.nicknameCreateScene);
				}
            }
        }
    }

	/*
	void TestStageData()
	{
		Dictionary<string, string> test = new Dictionary<string, string>()
		{
			{ "Stage_001", "032:02" },
			 {"Stage_002", "032:02" },
			  {"Stage_003", "032:02" }
		};
		Dictionary<string, int> test_int = new Dictionary<string, int>()
		{
			{ "Stage_001", 3220 },
			{"Stage_002", 3220 },
			{"Stage_003", 3220 }
		};
		Param pa = new Param();
		pa.Add("StageLog", test);
		pa.Add("Stage_001", "032:02");
		pa.Add("Stage_002", 32.32f);
		pa.Add("StageFloat", test_int);
		Backend.GameInfo.Insert("stage", pa);
	}
	*/
	bool IsNickNameSet()
	{
        BackendReturnObject bro = Backend.BMember.GetUserInfo();
        string tVal = bro.GetReturnValue();
        UserMetaData metaData = JsonUtility.FromJson<UserMetaData>(tVal);
		try
		{
			return !metaData.row.nickname.Equals("");
		}
		catch
		{
			return false;
		}
	}

    void Start()
    {
#if UNITY_EDITOR
		PlayerPrefs.DeleteAll();
#endif
		InitialBackend();
        bool isServerAvailable = ServerStatus.CheckServerStatus();

        if (isServerAvailable == false)
        {
            serverStatusPanel.SetActive(true);
            return;
        }
#if UNITY_EDITOR
		TryLogin();
		return;
#endif
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
	   .Builder()
	   .RequestServerAuthCode(false)
	   .RequestIdToken()
	   .Build();
		//커스텀된 정보로 GPGS 초기화
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		//GPGS 시작.
		PlayGamesPlatform.Activate();
		GoogleLogin();


		
    }
	public void GoogleLogin()
	{
		// 이미 로그인 된 경우
		if (Social.localUser.authenticated == true)
		{
			BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google, "gpgs");
			isSucceeded = true;
		}
		else
		{
			Social.localUser.Authenticate((bool success) => {
				if (success)
				{
					// 로그인 성공 -> 뒤끝 서버에 획득한 구글 토큰으로 가입요청
					BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google, "gpgs");
					isSucceeded = true;
				}
				else
				{
					// 로그인 실패
					Debug.Log("Login failed for some reason");
					serverStatusPanel.SetActive(true);
				}
			});
		}
	}

	// 구글 토큰 받아옴
	public string GetTokens()
	{
		if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
			// 유저 토큰 받기 첫번째 방법
			string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
			// 두번째 방법
			// string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
			return _IDtoken;
		}
		else
		{
			Debug.Log("접속되어있지 않습니다. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
			return null;
		}
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
    public void OnClickBtnHideSignUp()
    {
        signup.SetActive(false);
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
                    FailLogInPanel.SetActive(true);
                    FailLogInText.text = "아이디 또는 비밀번호가 틀렸습니다";
                }
                break;

            case 403:
                {
                    Debug.Log("차단당한 유저");
                    FailLogInPanel.SetActive(true);
                    FailLogInText.text = "차단당한 유저입니다";
                }
                break;

            default:
                PlayerPrefs.SetString("UserID", ID);
                PlayerPrefs.SetString("UserPW", PW);
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
            FailLogInPanel.SetActive(true);
            FailLogInText.text = "아이디를 입력하세요";
        }
        else if (tPW == "")
        {
            Debug.Log("패스워드를 입력해라");
            FailLogInPanel.SetActive(true);
            FailLogInText.text = "비밀번호를 입력하세요";
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
					SetDefaultData();
					isSucceeded = true;
                    break;
            }

            Debug.Log(bro.ToString());
        }
    }

	void SetDefaultData()
	{
		Param heartParam = new Param();
		heartParam.Add("HeartCount", 5);

		BackendReturnObject bro = Backend.Utils.GetServerTime();
		string tTime = bro.GetReturnValue();
		ServerTime sTime = JsonUtility.FromJson<ServerTime>(tTime);
		CJooTime jooTime = new CJooTime(sTime);
		Debug.Log(sTime.utcTime);
		Debug.Log(jooTime.ToString());
		heartParam.Add("RecordedDate", jooTime.ToString());
		heartParam.Add("RemainTime", -1);
		Backend.GameInfo.Insert("heart", heartParam);

		Param itemParam = new Param();
		itemParam.Add("itemDict", new Dictionary<string, int>() { { "item01", 5 }, { "item02", 5 }, { "item03", 5 }, { "item04", 5 } });
		Backend.GameInfo.Insert("item", itemParam);
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

    public void OnClickBtnQuit()
    {
        Application.Quit();
		
    }

    public void ButtonFailLogInPanelClose()
    {
        FailLogInPanel.SetActive(false);
    }
}
