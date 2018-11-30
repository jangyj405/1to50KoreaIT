using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FriendData
{
	[SerializeField]
	public FriendDataValue[] rows = null;
}

[Serializable]
public class FriendDataValue
{
	[SerializeField]
	public JsonS inDate;

	[SerializeField]
	public JsonS nickname;
}