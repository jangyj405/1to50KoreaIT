﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;
using UnityEngine.UI;


[Serializable]
public class JsonSuper 
{
	[SerializeField]
	public JsonTest[] rows = new JsonTest[1];
	//public ArrayList rows = new ArrayList();
}


[Serializable]
public class JsonTest
{
	[SerializeField]
	public JsonS client_date = new JsonS();
	[SerializeField]
	public JsonS inDate = new JsonS();
	
	[SerializeField]
	public JsonS updatedAt = new JsonS();

	[SerializeField]
	public JsonN Test01 = new JsonN();
	[SerializeField]
	public JsonS Test02 = new JsonS();
}

[Serializable]
public class JsonS
{
	[SerializeField]
	public string S = "";
}
[Serializable]
public class JsonN
{
	[SerializeField]
	public string N = "";
}


[Serializable]
public class JsonTableBase
{
	[SerializeField]
	public JsonS client_date;
	[SerializeField]
	public JsonS nickname;
	[SerializeField]
	public JsonS inDate;
	[SerializeField]
	public JsonS updatedAt;
}

[SerializeField]
public class JsonTableHeart : JsonTableBase
{
	[SerializeField]
	public JsonN HeartCount;
	[SerializeField]
	public JsonS RecordedDate;
	[SerializeField]
	public JsonN RemainTime;

	//todo 20181210
	public TimeWithHeart GetTimeWithHeart()
	{
		TimeWithHeart tTwh = new TimeWithHeart();
		tTwh.HeartCount = (HeartCount == null)? 0 : Convert.ToInt32(this.HeartCount.N);
		tTwh.RecordedDate = this.RecordedDate.S;
		tTwh.RemainTime = (HeartCount == null) ? 0 : Convert.ToInt32(this.RemainTime.N);
		return tTwh;
	}
}



public class CJooHeart : MonoBehaviour
{
	[SerializeField]
	private int maxHeart = 5;

	[SerializeField]
	private int curHeart = 0;
	private int CurHeart
	{
		get
		{
			return curHeart;
		}
		set
		{
			curHeart = value;
			DisplayHeart();
			if (curHeart < maxHeart)
			{
				if(RemainTime > 0)
				{
					return;
				}
				else
				{
					RemainTime = resetTime;
				}
			}
			else
			{
				RemainTime = -1;
			}
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
			if(remainTime == 0)
			{
				CurHeart++;
			}
			else
			{
				DisplayOnUI();
			}
		}
	}
	[SerializeField]
	Text heartText = null;
	void DisplayHeart()
	{
		heartText.text = CurHeart.ToString() + "/" + maxHeart.ToString();
	}

	[SerializeField]
	Text timeText = null;
	void DisplayOnUI()
	{
		timeText.text = (RemainTime == -1) ? "Max" : RemainTime.ToString();
	}
	IEnumerator TimeDecrease()
	{
		while(true)
		{
			if(RemainTime == -1)
			{
				yield return null;
			}
			else
			{
				yield return new WaitForSeconds(1f);
				RemainTime--;
			}
		}
	}
	// Use this for initialization
	void Start()
	{
		//Param param = new Param();
		//param.Add("Test01", 111);
		//param.Add("Test02", "Test");
		//
		//Backend.GameInfo.Insert("message", param);
		//

		//BackendReturnObject bro = Backend.GameInfo.GetTableList();
		//Debug.Log(bro.GetReturnValue());
		//Tables table = JsonUtility.FromJson<Tables>(bro.GetReturnValue());
		//Debug.Log(table.privateTables.Length);
		//Debug.Log(table.publicTables.Length);

		/*
		//됐음!!
		BackendReturnObject bro = Backend.GameInfo.GetPrivateContents("message");
		Debug.Log(bro.GetReturnValue());

		JsonSuper super = JsonUtility.FromJson<JsonSuper>(bro.GetReturnValue());
		Debug.Log(super.rows[0].Test01.N);
			//= new JsonSuper();

		//string aaa = JsonUtility.ToJson(super);
		//Debug.Log(aaa);
	    */
		//todo passed data from server
		//TimeWithHeart twh = new TimeWithHeart();
		//twh.HeartCount = 3;
		//twh.RecordedDate = "2018-11-29T05:05:00";
		//twh.RemainTime = 200;

		TimeWithHeart tTwh = GetTimeWithHeartFromServer();
		Initial(tTwh);
		StartCoroutine(TimeDecrease());
		

		//BackendReturnObject bro = Backend.Social.Post.GetPostList();
		//string asdf = bro.GetReturnValue();
		//Debug.Log(asdf);

		//BackendReturnObject bro = Backend.Social.GetGamerIndateByNickname("Mailer");
		//Debug.Log(bro.GetReturnValue());
		//PlayerPrefs.DeleteAll();
		//return;
		
		/*
		string MyIndate = Backend.BMember.GetUserInfo().GetReturnValue();

		UserMetaData user = JsonUtility.FromJson<UserMetaData>(MyIndate);


		string inDate = "2018-11-29T07:42:23.092Z";
		PostItem item = new PostItem()
		{
			Title = "Test",
			Content = "TestMail",
			TableName = "Heart",
			RowInDate = user.row.inDate,
			Column = "TestCol"
		};
		Backend.Social.Post.SendPost(inDate, item);
		*/

		/*
		BackendReturnObject bro = Backend.BMember.GetUserInfo();
		Debug.Log(bro.GetReturnValue());
		UserMetaData meta = JsonUtility.FromJson<UserMetaData>(bro.GetReturnValue());
		Debug.Log(meta.row.inDate);
		*/
		/*
		//Test
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
		*/
	}

	private TimeWithHeart GetTimeWithHeartFromServer()
	{
		BackendReturnObject bro = Backend.GameInfo.GetPrivateContents("heart");
		string tResultValue = bro.GetReturnValue();
		JsonTableHeart heartTable = JsonUtility.FromJson<JsonTableHeart>(tResultValue);
		if(heartTable == null)
		{
			return null;
		}
		TimeWithHeart tTwh = heartTable.GetTimeWithHeart();
		return tTwh;
	}

	public void Initial(TimeWithHeart pTimeWithHeart)
	{
		if(pTimeWithHeart == null)
		{
			CurHeart = maxHeart;
			return;
		}

		BackendReturnObject broServerTime = Backend.Utils.GetServerTime();
		ServerTime time = JsonUtility.FromJson<ServerTime>(broServerTime.GetReturnValue());

		CJooTime nowTime = new CJooTime(time);
		CJooTime recordedTime = new CJooTime(pTimeWithHeart.RecordedDate);

		CJooTime timeGap = nowTime - recordedTime;
		int gapSecond = timeGap.ToSecond();
		if(gapSecond == -1)
		{
			CurHeart = maxHeart;
		}
		else
		{
			if (gapSecond - pTimeWithHeart.RemainTime < 0)
			{
				RemainTime = pTimeWithHeart.RemainTime - gapSecond;
				CurHeart = pTimeWithHeart.HeartCount;
			}
			else
			{
				int refillHeart = (gapSecond - RemainTime) / 300;
				int newRemainTime = (gapSecond - RemainTime) % 300;

				CurHeart = pTimeWithHeart.HeartCount + refillHeart;
				RemainTime = newRemainTime;
			}
		}
	}

	public void OnClickBtnUseHeart()
	{
		if (CurHeart <= 0)
		{
			Debug.Log("하트가 모자라!");
			return;
		}
		CurHeart--;
	}
}

