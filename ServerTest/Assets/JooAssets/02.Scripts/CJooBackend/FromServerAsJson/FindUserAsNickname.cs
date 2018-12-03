using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;


[Serializable]
public class FindUserAsNickname
{
	[SerializeField]
	public FindUserAsNicknameValue[] rows;
}

[Serializable]
public class FindUserAsNicknameValue
{
	[SerializeField]
	public JsonS inDate;
}

