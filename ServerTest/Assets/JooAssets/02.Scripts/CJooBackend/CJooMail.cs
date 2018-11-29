using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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






public class CJooMail : MonoBehaviour
{
	void OnEnable()
	{

	}
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void GetPostListFromServer()
	{

	}

}
