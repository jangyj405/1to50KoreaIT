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

    public virtual void InitialOneFriend(string pNickname, string pInDate)
    {
        NickName = pNickname;
        inDate = pInDate;
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
}
