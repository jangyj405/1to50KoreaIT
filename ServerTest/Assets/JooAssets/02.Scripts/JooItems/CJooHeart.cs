using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;


public class TimeWithHeart
{
	public int HeartCount = 0;
	public float remainTime = 0;
	public string recordedDate = "";
}

public class CJooHeart : MonoBehaviour
{
	[SerializeField]
	private int maxHeart = 5;

	[SerializeField]
	private int curHeart = 5;
	private int CurHeart
	{
		get
		{
			return curHeart;
		}
		set
		{
			curHeart = value;
		}
	}


	[SerializeField]
	private int resetTime = 300;

	[SerializeField]
	private int remainTime = 0;
	private int RemainTime
	{
		get
		{
			return remainTime;
		}
		set
		{
			remainTime = value;
		}
	}

	// Use this for initialization
	void Start ()
	{
		//BackendReturnObject bro = Backend.Utils.GetServerTime();
		//ServerTime a = JsonUtility.FromJson<ServerTime>(bro.GetReturnValue());
		//Debug.Log(a.utcTime);

		//char[] an = { '-', 'T',':' };
		//string t = "2018-11-26T06:22:14.947Z";
		//string[] tSplit = t.Split(an);
		//foreach(string ta in tSplit)
		//{
		//	Debug.Log(ta);
		//}
		string time_0 = "2018-11-26T06:22:14.947Z";
		string time_1 = "2018-11-29T05:18:15.947Z";

		CJooTime jooTime_0 = new CJooTime(time_0);
		CJooTime jooTime_1 = new CJooTime(time_1);

		CJooTime gapTime = jooTime_1 -jooTime_0;

		Debug.Log(jooTime_0.ToString());
		Debug.Log(jooTime_1.ToString());
		Debug.Log(gapTime.ToString());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void GetHeartFromServer()
	{

	}

	public void OnClickBtnUseHeart()
	{
		if(CurHeart == 0)
		{
			return;
		}
		CurHeart--;
	}
}
