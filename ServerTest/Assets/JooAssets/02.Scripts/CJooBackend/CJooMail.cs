using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BackEnd;

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
	public JsonTest test;

	[SerializeField]
	public JsonS sentDate;

	[SerializeField]
	public JsonS receiverNickname;

	[SerializeField]
	public JsonS title;
	
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
	void OnEnable()
	{

	}
	// Use this for initialization
	void Start ()
	{
		GetPostListFromServer();
		BackendReturnObject bro = Backend.Social.Post.GetPostList();
		string result = bro.GetReturnValue();
		Debug.Log(result);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void GetPostListFromServer()
	{
		//todo//
		 BackendReturnObject bro = Backend.Social.Message.GetReceivedMessageList();
		string result = bro.GetReturnValue();
		Debug.Log(result);
		CJooPostFromUserRows tData = JsonUtility.FromJson<CJooPostFromUserRows>(result);
		Debug.Log(tData.rows[0].content.S);
	}

}
