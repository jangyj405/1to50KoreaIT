using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;


public class Friend : MonoBehaviour {

    public Text txtNickName;
    protected string inDate = "";
    public string InDate
    {
        get
        {
            return inDate;
        }
    }

    protected string nickName;
    public string NickName
    {
        get
        {
            return nickName;
        }
        protected set
        {
            nickName = value;
            txtNickName.text = nickName;
        }
    }

	public Button sendHeartButton = null;

    public virtual void InitialOneFriend(string pNickname, string pInDate, bool hasSentHeart = false)
    {
        NickName = pNickname;
        inDate = pInDate;
		if(sendHeartButton != null)
		{
			sendHeartButton.interactable = !hasSentHeart;
		}
    }

	public virtual void FriendDeleteClick()
	{
		//todo server
		BackendReturnObject deleteBro = Backend.Social.Friend.BreakFriend(InDate);
		bool isServerOK = CJooBackendCommonErrors.IsAvailableWithServer(deleteBro);
		if (!isServerOK)
		{
			//fail to Accept because of server
			return;
		}
		Debug.Log(NickName + "은 이제 내 친구가 아니야!");
		Destroy(this.gameObject);
	}

	public void OnClickBtnSendHeart()
	{
		BackendReturnObject sendBro = Backend.Social.Message.SendMessage(InDate, MessageSenders.fromUser + "OneHeart");
		bool isServerOK = CJooBackendCommonErrors.IsAvailableWithServer(sendBro);
		if (!isServerOK)
		{
			//fail to Accept because of server
			return;
		}

		string statusCodeStr = sendBro.GetStatusCode();
		int statusCodeInt = System.Convert.ToInt32(statusCodeStr);
		switch(statusCodeInt)
		{
			case 405:
				return;
		}
		sendHeartButton.interactable = false;
	}
}


public static class MessageSenders
{
	public const string fromAdmin = "Admin:";
	public const string fromUser = "User:";
	public const char splitter = ':';
}