using System.Collections;
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

	public int ToInt()
	{
		try
		{
			int result = Convert.ToInt32(S);
			return result;
		}
		catch
		{
			return 0;
		}
	}
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

[Serializable]
public class JsonTableHeartRows
{
	[SerializeField]
	public JsonTableHeart[] rows;
}

[Serializable]
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
		Debug.Log(HeartCount.N);
		Debug.Log(RecordedDate.S);
		try
		{
			tTwh.HeartCount = Convert.ToInt32(this.HeartCount.N);
		}
		catch
		{
			tTwh.HeartCount = 0;
			tTwh.isFirst = true;
		}
		try
		{
			tTwh.RemainTime = Convert.ToInt32(this.RemainTime.N);
		}
		catch
		{
			tTwh.RemainTime = 0;
			tTwh.isFirst = true;
		}

		//tTwh.HeartCount = (HeartCount == null)? 0 : Convert.ToInt32(this.HeartCount.N);
		tTwh.RecordedDate = this.RecordedDate.S;
		//tTwh.RemainTime = (RemainTime == null) ? 0 : Convert.ToInt32(this.RemainTime.N);
		Debug.Log(tTwh.RemainTime);
		return tTwh;
	}
}



public class CJooHeart : MonoBehaviour
{
	public static CJooHeart jooHeart = null;
	public static int heartCountStc = 0;
	public static string inDate = "";
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
			if(CurHeart >= maxHeart)
			{
				remainTime = -1;
				DisplayOnUI();
				return;
			}
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


	void Awake()
	{
		jooHeart = this;
		inDate = "";
	}

	// Use this for initialization
	void Start()
	{
	
		TimeWithHeart tTwh = GetTimeWithHeartFromServer();
		Initial(tTwh);
		StartCoroutine(TimeDecrease());

	}

	private TimeWithHeart GetTimeWithHeartFromServer()
	{
		BackendReturnObject bro = Backend.GameInfo.GetPrivateContents("heart");
		string tResultValue = bro.GetReturnValue();
		JsonTableHeartRows heartTable = JsonUtility.FromJson<JsonTableHeartRows>(tResultValue);
		Debug.Log(tResultValue);
		
		try
		{
			TimeWithHeart tTwh = heartTable.rows[0].GetTimeWithHeart();
			inDate = heartTable.rows[0].inDate.S;
			return tTwh;
		}
		catch
		{
			TimeWithHeart tTwh = new TimeWithHeart();
			tTwh.isFirst = true;
			return tTwh;
		}
	}
	struct TempInDate
	{
		public string inDate;
	}

	private string InsertHeartDataToServer(bool pIsFirst)
	{
		int tRemainTime = (pIsFirst) ? -1 : RemainTime;

		Param heartParam = new Param();
		heartParam.Add("HeartCount", CurHeart);

		BackendReturnObject bro = Backend.Utils.GetServerTime();
		string tTime = bro.GetReturnValue();
		ServerTime sTime = JsonUtility.FromJson<ServerTime>(tTime);
		CJooTime jooTime = new CJooTime(sTime);
		Debug.Log(sTime.utcTime);
		Debug.Log(jooTime.ToString());
		heartParam.Add("RecordedDate", jooTime.ToString());
		heartParam.Add("RemainTime", tRemainTime);
		BackendReturnObject broInsert = (pIsFirst) ?
			Backend.GameInfo.Insert("heart", heartParam) : Backend.GameInfo.Update("heart", inDate, heartParam);
		Debug.Log(broInsert.GetReturnValue());
		try
		{
			TempInDate tIndate = JsonUtility.FromJson<TempInDate>(broInsert.GetReturnValue());
			return tIndate.inDate;
		}
		catch
		{
			Debug.Log("Called");
			string tIndate = inDate;
			return tIndate;
		}
	}


	public void Initial(TimeWithHeart pTimeWithHeart)
	{
		if (pTimeWithHeart.isFirst)
		{
			CurHeart = maxHeart;

			inDate = InsertHeartDataToServer(pTimeWithHeart.isFirst);
			return;
		}

		if (pTimeWithHeart.RemainTime == -1)
		{
			CurHeart = pTimeWithHeart.HeartCount;
			return;
		}

		if (pTimeWithHeart.HeartCount >= maxHeart)
		{
			CurHeart = pTimeWithHeart.HeartCount;
			return;
		}

		BackendReturnObject broServerTime = Backend.Utils.GetServerTime();
		ServerTime time = JsonUtility.FromJson<ServerTime>(broServerTime.GetReturnValue());

		CJooTime nowTime = new CJooTime(time);
		CJooTime recordedTime = new CJooTime(pTimeWithHeart.RecordedDate);

		CJooTime timeGap = nowTime - recordedTime;
		int gapSecond = timeGap.ToSecond();
		Debug.Log(timeGap.ToString());
		Debug.Log(gapSecond);
		if(gapSecond == -1)
		{
			CurHeart = maxHeart;
		}
		else
		{
			if (gapSecond - pTimeWithHeart.RemainTime < 0)
			{
				Debug.Log("Called If");
				RemainTime = pTimeWithHeart.RemainTime - gapSecond;
				CurHeart = pTimeWithHeart.HeartCount;
			}
			else
			{
				int refillHeart = (gapSecond - pTimeWithHeart.RemainTime) / 300 + 1;
				int newRemainTime = 300 - (gapSecond - pTimeWithHeart.RemainTime) % 300;

				CurHeart = pTimeWithHeart.HeartCount + refillHeart;
				if(CurHeart >= maxHeart)
				{
					CurHeart = maxHeart;
				}
				else
				{
					RemainTime = newRemainTime;
				}
			}
		}
		
	}

	public void OnClickBtnUseHeart(Action act)
	{
		if (CurHeart <= 0)
		{
			Debug.Log("하트가 모자라!");
			return;
		}
		CurHeart--;
		inDate = InsertHeartDataToServer(false);
		if(act != null)
		{
			act();
		}
	}


	void OnGUI()
	{
		if(GUI.Button(new Rect(0f,0f, 100f,100f),"Use Heart") == true)
		{
			OnClickBtnUseHeart(null);
		}
	}

	void OnDisable()
	{
		heartCountStc = CurHeart;
	}
}

