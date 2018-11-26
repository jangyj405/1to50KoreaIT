using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System;

public class CJooTime
{
	char[] m_split = { '-', 'T', ':', '.' };
	public CJooTime(ServerTime time)
	{
		string[] splitted = time.utcTime.Split(m_split);
		Year = Convert.ToInt32(splitted[0]);
		Month = Convert.ToInt32(splitted[1]);
		Day = Convert.ToInt32(splitted[2]);

		Hour = Convert.ToInt32(splitted[3]);
		Minute = Convert.ToInt32(splitted[4]);
		Second = Convert.ToInt32(splitted[5]);
	}

	private CJooTime()
	{

	}

	private int year = 0;
	public int Year
	{
		get
		{
			return year;
		}
		private set
		{
			year = value;
		}
	}


	private int month = 0;
	public int Month
	{
		get
		{
			return month;
		}
		private set
		{
			month = value;
		}
	}

	//todo
	private int day = 0;
	public int Day
	{
		get
		{
			return day;
		}
		private set
		{
			day = value;
		}
	}


	private int hour = 0;
	public int Hour
	{
		get
		{
			return hour;
		}
		private set
		{
			int quotient = value / 24;
			int remainder = value % 24;
			if (quotient > 0)
			{
				Day += quotient;
			}
			hour = remainder;
		}
	}

	private int minute = 0;
	public int Minute
	{
		get
		{
			return minute;
		}
		private set
		{
			int quotient = value / 60;
			int remainder = value % 60;
			if (quotient > 0)
			{
				Hour += quotient;
			}
			minute = remainder;
		}
	}


	private int second = 0;
	public int Second
	{
		get
		{
			return second;
		}
		private set
		{
			int quotient = value / 60;
			int remainder = value % 60;
			if (quotient > 0)
			{
				Minute += quotient;
			}
			second = remainder;
		}
	}

	
}



public class ServerTime
{
	public string utcTime = "";
}

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
		BackendReturnObject bro = Backend.Utils.GetServerTime();
		ServerTime a = JsonUtility.FromJson<ServerTime>(bro.GetReturnValue());
		Debug.Log(a.utcTime);

		char[] an = { '-', 'T',':' };
		string t = "2018-11-26T06:22:14.947Z";
		string[] tSplit = t.Split(an);
		foreach(string ta in tSplit)
		{
			Debug.Log(ta);
		}
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
