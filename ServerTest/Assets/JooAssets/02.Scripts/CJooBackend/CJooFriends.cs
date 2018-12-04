using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using System;


public class CJooFriends : MonoBehaviour
{
	private enum EMessageTypes
	{
		NoInputError = 0,
		ServerError = 1,
		AddFriend = 2,
		NoUserError = 3,
		DuplicatedError = 4,
		RequestError = 5,
		SucceededRequest = 6
	}
	
	[SerializeField]
	private InputField nicknameField = null;

	Action[] DisplayMessage = null;
	Action[] HideMessage = null;


	[SerializeField]
	private CJooErrorMessage panelErrorMessage = null;


	[SerializeField]
	private GameObject panelAddFriendGO = null;
	

	private string foundUserIndate = "";
	private FriendData nowFriendData = null;
	private FriendData NowFriendData
	{
		get
		{
			return nowFriendData;
		}
		set
		{
			nowFriendData = value;
			if(nowFriendData != null)
			{
				foreach (FriendDataValue val in nowFriendData.rows)
				{
					string tNickName = val.nickname.S;
					string tInDate = val.inDate.S;
					FriendView.friendView.FriendAddReal(tNickName, tInDate);
				}
			}
			else
			{
				//HideAllFriendInfos
			}
		}
	}

	private FriendData requestedFriendData = null;
	private FriendData RequestedFriendData
	{
		get
		{
			return requestedFriendData;
		}
		set
		{
			requestedFriendData = value;
			if (requestedFriendData != null)
			{
				foreach (FriendDataValue val in requestedFriendData.rows)
				{
					string tNickName = val.nickname.S;
					string tInDate = val.inDate.S;
					FriendView.friendView.InstantiateRequestedList(tNickName, tInDate);
				}
			}
			else
			{
				//HideAllFriendInfos
			}
		}
	}


	void Awake()
	{
		DisplayMessage = new Action[7];
		DisplayMessage[(int)EMessageTypes.NoInputError] = DisplayNoInputError;
		DisplayMessage[(int)EMessageTypes.ServerError] = DisplayServerError;
		DisplayMessage[(int)EMessageTypes.AddFriend] = DisplayAddFriend;
		DisplayMessage[(int)EMessageTypes.NoUserError] = DisplayNoUserError;
		DisplayMessage[(int)EMessageTypes.DuplicatedError] = DisplayDuplicatedError;
		DisplayMessage[(int)EMessageTypes.RequestError] = DisplayRequestError;
		DisplayMessage[(int)EMessageTypes.SucceededRequest] = DisplaySucceededMessage;

		HideMessage = new Action[7];
		HideMessage[(int)EMessageTypes.NoInputError] = HideNoInputError;
		HideMessage[(int)EMessageTypes.ServerError] = HideServerError;
		HideMessage[(int)EMessageTypes.AddFriend] = HideAddFriend;
		HideMessage[(int)EMessageTypes.NoUserError] = HideNoUserError;
		HideMessage[(int)EMessageTypes.DuplicatedError] = HideDuplicatedError;
		HideMessage[(int)EMessageTypes.RequestError] = HideRequestError;
		HideMessage[(int)EMessageTypes.SucceededRequest] = HideSucceededMessage;
	}


	
	void OnEnable ()
	{
		GetUserMetaData();
		GetFriendList();
		GetRequestedList();
	}

	void InitialFriendScene()
	{
		HideAllMessages();
		nicknameField.text = "";
		foundUserIndate = "";
		NowFriendData = null;
		RequestedFriendData = null;
	}

	void GetUserMetaData()
	{
		BackendReturnObject bro = Backend.BMember.GetUserInfo();
		UserMetaData data = JsonUtility.FromJson<UserMetaData>(bro.GetReturnValue());
		Debug.Log(data.row.nickname);
		PlayerPrefs.DeleteAll();
	}

	void AddFriend()
	{
		string indate = "2018-11-29T07:42:23.092Z";
		BackendReturnObject bro = Backend.Social.Friend.RequestFriend(indate);
		Debug.Log(bro.GetStatusCode());
		PlayerPrefs.DeleteAll();
	}


	public void OnClickBtnRequestFriend()
	{
		string tNickName = nicknameField.text;
		if(tNickName == "")
		{
			DisplayMessage[(int)EMessageTypes.NoInputError]();
			return;
		}
		BackendReturnObject bro = Backend.Social.GetGamerIndateByNickname(tNickName);

		bool isAvailable = CJooBackendCommonErrors.IsAvailableWithServer(bro);
		if(!isAvailable)
		{
			DisplayMessage[(int)EMessageTypes.ServerError]();
			return;
		}

	
		FindUserAsNickname tValue = JsonUtility.FromJson<FindUserAsNickname>(bro.GetReturnValue());
		if(tValue == null)
		{
			DisplayMessage[(int)EMessageTypes.ServerError]();
			return;
		}

		//if (tValue.rows[0] == null)
		//{
		//	DisplayMessage[(int)EMessageTypes.NoUserError]();
		//	return;
		//}
		string tInDate = "";
		try
		{
			tInDate = tValue.rows[0].inDate.S;
		}
		catch
		{
			DisplayMessage[(int)EMessageTypes.NoUserError]();
			return;
		}

		
		Debug.Log(tInDate);

		if (tInDate == "")
		{
			DisplayMessage[(int)EMessageTypes.NoUserError]();
			return;
		}

		BackendReturnObject requestBro = Backend.Social.Friend.RequestFriend(tInDate);

		string statusCodeStr = requestBro.GetStatusCode();
		int statusCodeInt = Convert.ToInt32(statusCodeStr);

		switch (statusCodeInt)
		{
			case 409:
				DisplayMessage[(int)EMessageTypes.DuplicatedError]();
				return;

			case 412:
				DisplayMessage[(int)EMessageTypes.RequestError]();
				return;
		}

		DisplayMessage[(int)EMessageTypes.SucceededRequest]();
		
	}




	void GetFriendList()
	{
		BackendReturnObject bro = Backend.Social.Friend.GetFriendList();
		string list = bro.GetReturnValue();
		Debug.Log(list);

		NowFriendData = JsonUtility.FromJson<FriendData>(list);
		//Debug.Log(NowFriendData.rows[0].nickname.S);
		//Debug.Log(NowFriendData.rows[0].inDate.S);
	}

	void GetRequestedList()
	{
		BackendReturnObject bro = Backend.Social.Friend.GetReceivedRequestList();
		string jsonValue = bro.GetReturnValue();
		RequestedFriendData = JsonUtility.FromJson<FriendData>(jsonValue);
		//Debug.Log(RequestedFriendData.rows[0].nickname.S);
	}



	private void HideAllMessages()
	{
		panelAddFriendGO.SetActive(false);
		panelErrorMessage.gameObject.SetActive(false);
	}

	public void DisplayMessageinArray(int btnValue)
	{
		DisplayMessage[btnValue]();
	}

	public void OnClickBtnHideMessageInArray(int btnValue)
	{
		HideMessage[btnValue]();
	}

	private void DisplayNoInputError()
	{
		panelErrorMessage.errorID = "친구요청에 실패하였습니다";
		panelErrorMessage.errorExplain = "닉네임을\n입력하세요!";
		panelErrorMessage.gameObject.SetActive(true);
	}
	private void HideNoInputError()
	{
		panelErrorMessage.gameObject.SetActive(false);
	}

	private void DisplayServerError()
	{
		panelErrorMessage.errorID = "친구요청에 실패하였습니다";
		panelErrorMessage.errorExplain = "서버 통신이\n원활하지 않습니다!";
		panelErrorMessage.gameObject.SetActive(true);
	}
	private void HideServerError()
	{
		panelErrorMessage.gameObject.SetActive(false);
	}

	private void DisplayAddFriend()
	{
		panelAddFriendGO.SetActive(true);
	}
	private void HideAddFriend()
	{
		panelAddFriendGO.SetActive(false);
	}

	private void DisplayNoUserError()
	{
		panelErrorMessage.errorID = "친구요청에 실패하였습니다";
		panelErrorMessage.errorExplain = "해당 닉네임의 유저가 없습니다!";
		panelErrorMessage.gameObject.SetActive(true);
	}

	private void HideNoUserError()
	{
		panelErrorMessage.gameObject.SetActive(false);
	}

	private void DisplayDuplicatedError()
	{
		panelErrorMessage.errorID = "친구요청에 실패하였습니다";
		panelErrorMessage.errorExplain = "이미 친구이거나\n이미 요청을 보낸 유저입니다!";
		panelErrorMessage.gameObject.SetActive(true);
	}
	private void HideDuplicatedError()
	{
		panelErrorMessage.gameObject.SetActive(false);
	}

	private void DisplayRequestError()
	{
		panelErrorMessage.errorID = "친구요청에 실패하였습니다";
		panelErrorMessage.errorExplain = "상대 혹은 자신의 친구 목록이 최대치입니다!";
		panelErrorMessage.gameObject.SetActive(true);
	}
	private void HideRequestError()
	{
		panelErrorMessage.gameObject.SetActive(false);
	}

	private void DisplaySucceededMessage()
	{
		panelErrorMessage.errorID = "친구요청에 성공하였습니다";
		panelErrorMessage.errorExplain = "요청받은 유저가 수락하면\n친구가 됩니다!";
		panelErrorMessage.gameObject.SetActive(true);
	}
	private void HideSucceededMessage()
	{
		panelErrorMessage.gameObject.SetActive(false);
	}
}
