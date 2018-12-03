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
	private GameObject panelErrorMessage = null;


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
		GetFriendList();
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

		string tInDate = tValue.rows.inDate.S;
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
		Debug.Log(NowFriendData.rows[0].nickname.S);
		Debug.Log(NowFriendData.rows[0].inDate.S);
	}

	void GetRequestedList()
	{
		BackendReturnObject bro = Backend.Social.Friend.GetReceivedRequestList();
		string aaa = bro.GetReturnValue();
		RequestedFriendData = JsonUtility.FromJson<FriendData>(aaa);
		Debug.Log(RequestedFriendData.rows[0].nickname.S);
	}



	private void HideAllMessages()
	{
		panelAddFriendGO.SetActive(false);
		panelErrorMessage.SetActive(false);
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
		panelErrorMessage.SetActive(true);
	}
	private void HideNoInputError()
	{
		panelErrorMessage.SetActive(false);
	}

	private void DisplayServerError()
	{
		panelErrorMessage.SetActive(true);
	}
	private void HideServerError()
	{
		panelErrorMessage.SetActive(false);
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
		panelErrorMessage.SetActive(true);
	}

	private void HideNoUserError()
	{
		panelErrorMessage.SetActive(false);
	}

	private void DisplayDuplicatedError()
	{
		panelErrorMessage.SetActive(true);
	}
	private void HideDuplicatedError()
	{
		panelErrorMessage.SetActive(false);
	}

	private void DisplayRequestError()
	{
		panelErrorMessage.SetActive(true);
	}
	private void HideRequestError()
	{
		panelErrorMessage.SetActive(false);
	}

	private void DisplaySucceededMessage()
	{

	}
	private void HideSucceededMessage()
	{

	}
}
