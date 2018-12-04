﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class CJooCreateNickName : MonoBehaviour
{
	[SerializeField]
	private InputField nickNameField = null;

	[SerializeField]
	private Text errorMessage = null;

	[SerializeField]
	private GameObject panelExpire = null;

	[SerializeField]
	private Button expireButton = null;

	[SerializeField]
	private GameObject succeededCreate = null;

	[SerializeField]
	private Button createButton = null;

	bool isSucceeded = false;
	bool IsSucceeded
	{
		get
		{
			return isSucceeded;
		}
		set
		{
			isSucceeded = value;
			if(isSucceeded == true)
			{
				StartCoroutine(NickNameCheckTimer());
				createButton.interactable = true;
			}
			else
			{
				panelExpire.SetActive(true);
				createButton.interactable = false;
			}
		}
	}
	

	void Start()
	{
		createButton.interactable = false;
	}

	public void OnClickBtnCheckDuplicate()
	{
		if(nickNameField.text == "")
		{
			DisplayNoInputError();
			Debug.Log("닉넴을 입력해라");
			return;
		}

		if(nickNameField.text.Contains(" "))
		{
			BlankError();
			return;
		}

		if (nickNameField.text.Length > 10)
		{
			OverlengthError();
			return;
		}

		if(IsContainsSpecialChars(nickNameField.text))
		{
			SpecialCharactersError();
			return;
		}

		BackendReturnObject userBro = Backend.Social.GetGamerIndateByNickname(nickNameField.text);
		bool isServerOkay = CJooBackendCommonErrors.IsAvailableWithServer(userBro);
		if(!isServerOkay)
		{
			Debug.Log("Server is Now Inavailable");
			DisplayServerError();
			return;
		}

		FindUserAsNickname uData = JsonUtility.FromJson<FindUserAsNickname>(userBro.GetReturnValue());
		string inDate = "";
		try
		{
			inDate = uData.rows[0].inDate.S;
			Debug.Log("중복 닉네임");
			DisplayDuplicationError();
		}
		catch
		{
			DisplayAvailMessage();
			IsSucceeded = true;
		}
	}


	public void OnClickBtnCreateNickName()
	{
		BackendReturnObject bro = Backend.BMember.CreateNickname(false, nickNameField.text);
		bool isServerOkay = CJooBackendCommonErrors.IsAvailableWithServer(bro);
		if (!isServerOkay)
		{
			Debug.Log("Server is Now Inavailable");
			DisplayServerError();
			return;
		}
		string serverStatusCode = bro.GetStatusCode();
		int code = Convert.ToInt32(serverStatusCode);
		switch (code)
		{
			case 409:
				Debug.Log("중복 닉네임");
				DisplayDuplicationError();
				isSucceeded = false;
				createButton.interactable = false;
				break;

			default:
				CreateNickNameSucceeded();
				break;
		}
	}


	//닉네임 길이가 지나치게 길때 20바이트
	//"닉네임은 10글자 이내로 입력하세요"
	void OverlengthError()
	{
		errorMessage.text = "닉네임은 10글자 이내로 입력하세요";
	}
	//아무것도 입력 안했을때
	//"닉네임을 입력하세요"
	void DisplayNoInputError()
	{
		errorMessage.text = "닉네임을 입력하세요";
	}

	//특수문자 불가능
	//"특수문자는 사용이 불가능합니다"
	void SpecialCharactersError()
	{
		errorMessage.text = "특수문자는 사용이 불가능합니다";
	}

	bool IsContainsSpecialChars(string pStr)
	{
		string normalChars = "^[a-zA-Z0-9가-힣]+$";
		bool tResult = Regex.IsMatch(pStr, normalChars);
		return !tResult;
	}

	//띄어쓰기 불가능
	//"띄어쓰기는 불가능합니다"
	void BlankError()
	{
		errorMessage.text = "띄어쓰기는 불가능합니다";
	}

	//서버통신 원할하지 않을떄
	//"서버통신이 원할하지 않습니다"
	//"잠시 후 다시 시도해 주십시오"
	void DisplayServerError()
	{
		errorMessage.text = "서버통신이 원할하지 않습니다\n잠시 후 다시 시도해 주십시오";
	}

	//닉네임이 중복될때        
	//"이미 존재하는 닉네임입니다"
	void DisplayDuplicationError()
	{
		errorMessage.text = "이미 존재하는 닉네임입니다";
	}

	//중복 체크 성공
	void DisplayAvailMessage()
	{
		errorMessage.text = "사용 가능한 닉네임입니다";
	}

	//닉네임이 성공적일때
	//"닉네임이 성공적으로 생성되었습니다"
	void CreateNickNameSucceeded()
	{
		succeededCreate.SetActive(true);
	}

	public void OnClickBtnGoToModeSelect()
	{
		SceneManager.LoadScene(SceneNames.modeSelectScene);
	}

	public void OnClickBtnClosePanelExpire()
	{
		panelExpire.SetActive(false);
	}




	IEnumerator NickNameCheckTimer()
	{
		int time = 0;
		while(IsSucceeded)
		{
			
			time++;
			if(time == 60)
			{
				IsSucceeded = false;
			}
			yield return new WaitForSeconds(1f);
		}
	}
}
