using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BackEnd;
using UnityEngine.UI;

[Serializable]
public class JsonNum
{
	[SerializeField]
	public int N;
}



[Serializable]
public class CJooPostFromAdmin
{
	[SerializeField]
	public JsonS content;

	[SerializeField]
	public JsonS expirationDate;

	[SerializeField]
	public JsonS inDate;

	[SerializeField]
	public JsonItemFromAdmin item;

	[SerializeField]
	public JsonS sentDate;

	[SerializeField]
	public JsonS receiverNickname;

	[SerializeField]
	public JsonS title;
	
}

[Serializable]
public class JsonItemFromAdmin
{
	[SerializeField]
	public JsonItemContent M;
}


[Serializable]
public class JsonItemContent
{
	[SerializeField]
	public JsonN num;

	[SerializeField]
	public JsonS item01;

	[SerializeField]
	public JsonS item02;

	[SerializeField]
	public JsonS item03;

	[SerializeField]
	public JsonS item04;

	[SerializeField]
	public JsonS item05;

	[SerializeField]
	public JsonS content;
}

[Serializable]
public class JsonPostFromBackendConsole
{
	[SerializeField]
	public CJooPostFromAdmin[] fromAdmin;
}

//receiver: [Object], // 쪽지 받은사람의 inDate
//            sender: [Object], // 쪽지 보낸사람의 inDate
//            content: [Object], // 쪽지 내용
//            inDate: [Object], // 쪽지의 inDate
//            senderNickname: [Object], // 쪽지 보낸사람의 닉네임
//            isRead: [Object], // 받은사람이 읽었는지 판단하는 기준 (String : y/n)
//            receiverNickname: [Object] // 쪽지 받은사람의 닉네임
//

[Serializable]
public class CJooPostFromUser
{
	[SerializeField]
	public JsonS receiver;

	[SerializeField]
	public JsonS sender;

	[SerializeField]
	public JsonS content;

	[SerializeField]
	public JsonS inDate;

	[SerializeField]
	public JsonS senderNickname;

	[SerializeField]
	public JsonS isRead;

	[SerializeField]
	public JsonS receiverNickname;

}

[Serializable]
public class CJooPostFromUserRows
{
	[SerializeField]
	public CJooPostFromUser[] rows = new CJooPostFromUser[1];
}



public class CJooMail : MonoBehaviour
{
	public static CJooMail mail = null;
	public Dictionary<string, CJooPostItem> postItemDic = new Dictionary<string, CJooPostItem>();
	public GameObject postListContainerUI = null;
	public CJooPostItemFromUser postUserPrefab;
	public CJooPostItemFromAdmin postAdminPrefab;
	void Awake()
	{
		mail = this;
	}

	void OnEnable()
	{

	}
	// Use this for initialization
	void Start ()
	{
		GetUserPostListFromServer();
		GetAdminPostListFromServer();


	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void GetUserPostListFromServer()
	{
		//todo//
		 BackendReturnObject bro = Backend.Social.Message.GetReceivedMessageList();
		string result = bro.GetReturnValue();
		Debug.Log(result);
		CJooPostFromUserRows tData = JsonUtility.FromJson<CJooPostFromUserRows>(result);
		Debug.Log(tData.rows[0].content.S);
		foreach(CJooPostFromUser userPost in tData.rows)
		{
			if(userPost.isRead.S == "y")
			{
				continue;
			}
			string tContent = userPost.content.S;
			string tInDate = userPost.inDate.S;
			string tSenderNickname = userPost.senderNickname.S;
			CJooPostItemFromUser item = Instantiate<CJooPostItemFromUser>(postUserPrefab, postListContainerUI.transform);
			item.Initial(tContent, tInDate, tSenderNickname);
			postItemDic.Add(tInDate, item);
		}
	}

	void GetAdminPostListFromServer()
	{
		BackendReturnObject bro = Backend.Social.Post.GetPostList();
		string result = bro.GetReturnValue();
		Debug.Log(result);
		JsonPostFromBackendConsole tData = JsonUtility.FromJson<JsonPostFromBackendConsole>(result);
		Debug.Log(tData.fromAdmin[0].item.M.content.S);
		foreach (var adminPost in tData.fromAdmin)
		{
			string tContent = adminPost.content.S;
			string tInDate = adminPost.inDate.S;
			List<int> tItemList = new List<int>();
			tItemList.Add(Convert.ToInt32(adminPost.item.M.item01.S));
			tItemList.Add(Convert.ToInt32(adminPost.item.M.item02.S));
			tItemList.Add(Convert.ToInt32(adminPost.item.M.item03.S));
			tItemList.Add(Convert.ToInt32(adminPost.item.M.item04.S));
			tItemList.Add(Convert.ToInt32(adminPost.item.M.item05.S));
			CJooPostItemFromAdmin item = Instantiate<CJooPostItemFromAdmin>(postAdminPrefab, postListContainerUI.transform);
			item.Initial(tContent, tInDate, tItemList.ToArray());
			postItemDic.Add(tInDate, item);
		}
	}
}
