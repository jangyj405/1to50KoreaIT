using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CJooLogin_1 : MonoBehaviour
{
	public GameObject signup = null;
	public InputField signupID = null;
	public InputField signupPW = null;

	public GameObject login = null;
	public InputField loginID = null;
	public InputField loginPW = null;

	public GameObject serverStatusPanel = null;
	string debugmessage = "";


	BackendReturnObject broPrefs = new BackendReturnObject();
	bool isSuccessPrefs = false;
	bool loginPrefs = false;

	BackendReturnObject broCustom = new BackendReturnObject();
	bool isSuccessCustom = false;
	bool loginCustom = false;

	BackendReturnObject broSignin = new BackendReturnObject();
	bool isSuccessSignin = false;
	bool signinCustom = false;

	BackendReturnObject broCheckNick = new BackendReturnObject();
	bool isSuccessNick = false;
	bool nicknamed = false;



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

	void Start()
	{
		PlayerPrefs.DeleteAll();
		//PlayerPrefs.SetString("UserID", "asdf");
		//PlayerPrefs.SetString("UserPW", "asdf");
		InitialBackend();
		bool isServerAvailable = ServerStatus.CheckServerStatus();

		if (isServerAvailable == false)
		{
			serverStatusPanel.SetActive(true);
			return;
		}

		TryLoginPrefs();
		//Backend.BMember.CustomLogin("asdf", "asdf");
		//CheckNickName(new BackendReturnObject());
	}
	bool isFirst = true;
	bool isCheckedNickName = false;
	bool isLogin = false;
	void Update()
	{
		
		if (isSuccessPrefs)
		{
			Backend.BMember.SaveToken(broPrefs);
			isSuccessPrefs = false;
			isLogin = CheckCustomLogin(broPrefs);
			broPrefs.Clear();
		}

		if (isSuccessCustom)
		{
			Backend.BMember.SaveToken(broCustom);
			isSuccessCustom = false;
			isLogin = CheckCustomLogin(broCustom);
			broCustom.Clear();
		}

		if (isSuccessSignin)
		{
			Backend.BMember.SaveToken(broSignin);
			isSuccessSignin = false;
			isLogin = CheckCustomSignup(broSignin);
			broSignin.Clear();
		}

		if(isLogin)
		{
			HasNickName = CheckNickName();
		}
		/*
		if(isSuccessNick)
		{
			Backend.BMember.SaveToken(broCheckNick);
			isSuccessNick = false;
			nicknamed = CheckNickName(broCheckNick);
			isCheckedNickName = true;
			broCheckNick.Clear();
		}

		if((loginCustom || loginPrefs || signinCustom) && isFirst)
		{
			Backend.BMember.GetUserInfo((callback) => { isSuccessNick = true; broCheckNick = callback; });
			isFirst = false;
		}
		if ((loginCustom || loginPrefs || signinCustom) && !isFirst && isCheckedNickName)
		{
			HasNickName = nicknamed;
		}
		*/
	}
	bool hasNickName = false;
	bool HasNickName
	{
		get
		{
			return hasNickName;
		}
		set
		{
			hasNickName = value;
			if(hasNickName == true)
			{
				SceneManager.LoadScene(SceneNames.modeSelectScene);
			}
			else
			{
				SceneManager.LoadScene(SceneNames.nicknameCreateScene);
			}
		}
	}
	bool CheckNickName(BackendReturnObject pBro)
	{
		BackendReturnObject tbro = Backend.BMember.GetUserInfo();
		string tVal = tbro.GetReturnValue();
		UserMetaData metaData = JsonUtility.FromJson<UserMetaData>(tVal);
		Debug.Log(tVal);
		if(metaData == null)
		{
			return false;
		}
		return !(metaData.row.nickname == "");

	}
	bool CheckNickName( )
	{
		BackendReturnObject tbro = Backend.BMember.GetUserInfo();
		string tVal = tbro.GetReturnValue();
		UserMetaData metaData = JsonUtility.FromJson<UserMetaData>(tVal);
		Debug.Log(tVal);
		if (metaData == null)
		{
			return false;
		}
		return !(metaData.row.nickname == "");

	}
	void TryLoginPrefs()
	{
		string id, pw;
		GetAccountFromPrefs(out id, out pw);
		if(id == "" || pw == "")
		{
			return;
		}
		Backend.BMember.CustomLogin(id, pw, (callback) => { isSuccessPrefs = true; broPrefs = callback; });
	}

	public void TryCreateAccount()
	{
		string id, pw;
		id = signupID.text;
		pw = signupPW.text;
		if (id == "" || pw == "")
		{
			Debug.Log("아이디 패스워드 입력 요망");
			return;
		}
		Backend.BMember.CustomSignUp(id, pw, (callback) => { isSuccessSignin = true; broSignin = callback; });
	}

	public void TryLoginCustom()
	{
		string id, pw;
		id = loginID.text;
		pw = loginPW.text;
		if (id == "" || pw == "")
		{
			Debug.Log("아이디 패스워드 입력 요망");
			return;
		}
		Backend.BMember.CustomLogin(id, pw, (callback) => { isSuccessCustom = true; broCustom = callback; });
	}

	void GetAccountFromPrefs(out string oID, out string oPW)
	{
		string tID = PlayerPrefs.GetString("UserID", "");
		string tPW = PlayerPrefs.GetString("UserPW", "");
		oID = tID;
		oPW = tPW;
	}
	bool CheckCustomLogin(BackendReturnObject pBro)
	{
		string statuscode = pBro.GetStatusCode();
		int statuscodeInt = Convert.ToInt32(statuscode);

		switch (statuscodeInt)
		{
			case 401:
				{
					Debug.Log("아이디가 존재하지 않음");
				}
				return false;

			case 403:
				{
					Debug.Log("차단당한 유저");
				}
				return false;

			default:
				PlayerPrefs.SetString("UserID", loginID.text);
				PlayerPrefs.SetString("UserPW", loginPW.text);
				return true;
		}
	}

	bool CheckCustomSignup(BackendReturnObject pBro)
	{
		string tID = "";
		string tPW = "";
		tID = signupID.text;
		tPW = signupPW.text;

		signup.SetActive(false);

		string statuscode = pBro.GetStatusCode();
		int statuscodeInt = Convert.ToInt32(statuscode);

		switch (statuscodeInt)
		{
			case 409:
				{
					Debug.Log("중복 ID");
					signup.SetActive(true);
				}
				return false;

			default:
				PlayerPrefs.SetString("UserID", tID);
				PlayerPrefs.SetString("UserPW", tPW);
				return true;
		}
	}

	public void OnClickBtnDisplaySignUp()
	{
		signup.SetActive(true);
	}
	public void OnClickBtnHideSignUp()
	{
		signup.SetActive(false);
	}
	public void OnClickBtnQuit()
	{
		Application.Quit();
	}
	/*
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
			if (m_isSucceeded == true)
			{
				bool hasSetNickname = true;//IsNickNameSet();
				if (hasSetNickname)
				{
					SceneManager.LoadScene(SceneNames.modeSelectScene);
				}
				else
				{
					SceneManager.LoadScene(SceneNames.nicknameCreateScene);
				}
			}
		}
	}

	bool IsNickNameSet()
	{
		BackendReturnObject bro = Backend.BMember.GetUserInfo();
		string tVal = bro.GetReturnValue();
		UserMetaData metaData = JsonUtility.FromJson<UserMetaData>(tVal);
		return !(metaData.row.nickname == "");
	}

	IEnumerator Start()
	{
		yield return new WaitForSeconds(1f);
		MakeCheckList();
		PlayerPrefs.DeleteAll();

		InitialBackend();
		bool isServerAvailable = ServerStatus.CheckServerStatus();

		if (isServerAvailable == false)
		{
			serverStatusPanel.SetActive(true);
			yield break;
		}
		TryLoginPrefs();
	}

	/// <summary>
	/// async
	/// </summary>
	List<Action> checkList = new List<Action>();
	void MakeCheckList()
	{
		checkList.Add(CheckPrefsLogin);
		checkList.Add(CheckCustomLogin);
		checkList.Add(CheckNickName);
	}
	bool isCheckedPrefs = false;
	bool isCheckedCustom = false;
	bool isCheckedNickName = false;
	bool hasCreatedNickName = false;

	BackendReturnObject fieldbro = new BackendReturnObject();
	bool isBroSuccess = false;
	void Update()
	{
		if (isBroSuccess)
		{
			Backend.BMember.SaveToken(fieldbro);


			foreach (var check in checkList)
			{
				check();
			}

			isBroSuccess = false;
			fieldbro.Clear();

			if ((isCheckedPrefs || isCheckedCustom) && !isCheckedNickName)
			{
				Backend.BMember.GetUserInfo((callback) => { isBroSuccess = true; fieldbro = callback; });
			}

		}

		if ((isCheckedPrefs || isCheckedCustom) && isCheckedNickName)
		{

		}
	}

	void CheckPrefsLogin()
	{
		if (isCheckedPrefs == true)
		{
			return;
		}
		string statuscode = fieldbro.GetStatusCode();
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
				isCheckedPrefs = true;
				break;
		}
	}
	void CheckCustomLogin()
	{
		if (isCheckedCustom == true)
		{
			return;
		}
		string statuscode = fieldbro.GetStatusCode();
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
				PlayerPrefs.SetString("UserID", loginID.text);
				PlayerPrefs.SetString("UserPW", loginPW.text);
				isCheckedCustom = true;
				break;
		}
	}
	void CheckNickName()
	{
		if (hasCreatedNickName == true)
		{
			return;
		}
		string tVal = fieldbro.GetReturnValue();
		UserMetaData metaData = JsonUtility.FromJson<UserMetaData>(tVal);
		hasCreatedNickName = !(metaData.row.nickname == "");

	}

	public void TryLoginPrefs()
	{
		string id, pw;
		GetAccountFromPrefs(out id, out pw);
		Backend.BMember.CustomLogin(id, pw, (callback) => { isBroSuccess = true; fieldbro = callback; });
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
		if (isSucceeded == true)
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
		debugmessage = statuscode;

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
		if (ID.Equals("") || PW.Equals(""))
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
				}
				break;

			case 403:
				{
					Debug.Log("차단당한 유저");
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
			debugmessage = "아이디를 입력해라";

		}
		else if (tPW == "")
		{
			Debug.Log("패스워드를 입력해라");
			debugmessage = "패스워드를 입력해라";
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
		if (isSucceeded == false)
		{
			login.SetActive(true);
		}
	}

	public void OnClickBtnQuit()
	{
		Application.Quit();

	}

	void OnGUI()
	{
		GUI.TextArea(new Rect(0f, 0f, 100f, 50f), debugmessage);
	}
	*/
}
