using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class CJooCreateNickName : MonoBehaviour
{
	[SerializeField]
	private InputField nickNameField = null;

	[SerializeField]
	private GameObject duplicationError = null;

	[SerializeField]
	private GameObject serverError = null;

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
				//Change Scene
			}
		}
	}
	
	public void OnClickBtnCreateNickName()
	{
		if(nickNameField.text == "")
		{
			Debug.Log("닉넴을 입력해라");
			return;
		}
		BackendReturnObject bro = Backend.BMember.CreateNickname(false, nickNameField.text);
		bool isServerOkay = CJooBackendCommonErrors.IsAvailableWithServer(bro);
		if(!isServerOkay)
		{
			Debug.Log("Server is Now Inavailable");
			PopServerError();
			return;
		}
		string serverStatusCode = bro.GetStatusCode();
		int code = Convert.ToInt32(serverStatusCode);
		switch(code)
		{
			case 409:
				Debug.Log("중복 닉네임");
				PopDuplicationError();
				break;

			default:
				IsSucceeded = true;
				break;
		}
	}

	void PopServerError()
	{
		//recommended : use DoTween;
		serverError.SetActive(true);
	}

	public void OnClickBtnCloseApp()
	{
		Application.Quit();
	}


	void PopDuplicationError()
	{
		//recommended : use DoTween;
		duplicationError.SetActive(true);
	}

	public void OnClickBtnCloseError()
	{
		//recommended : use DoTween;
		duplicationError.SetActive(false);
	}
}
