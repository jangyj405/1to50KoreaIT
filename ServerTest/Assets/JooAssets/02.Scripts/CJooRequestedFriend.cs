using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;

public class CJooRequestedFriend : Friend
{
	public void AcceptRequest()
	{
		BackendReturnObject acceptBro = Backend.Social.Friend.AcceptFriend(InDate);
		bool isServerOK = CJooBackendCommonErrors.IsAvailableWithServer(acceptBro);
		if(!isServerOK)
		{
			//fail to Accept because of server
			return;
		}

		string statusCodeStr = acceptBro.GetStatusCode();
		int statusCodeInt = Convert.ToInt32(statusCodeStr);
		switch(statusCodeInt)
		{
			case 412:
				//프렌드 리스트 꽉참
				//Display Error Message
				return;

			default:
				break;
		}

		Debug.Log(NickName + "과 친구가 되었다!");
		FriendView.friendView.FriendAddReal(NickName, InDate, false);
		Destroy(gameObject);
	}

	public override void FriendDeleteClick()
	{
		//todo Reject Friend Request
		BackendReturnObject rejectBro = Backend.Social.Friend.RejectFriend(InDate);
		bool isServerOK = CJooBackendCommonErrors.IsAvailableWithServer(rejectBro);
		if (!isServerOK)
		{
			//fail to Accept because of server
			return;
		}
		Debug.Log(NickName + "의 친구요청을 거절했다!");
		Destroy(gameObject);
	}
}
