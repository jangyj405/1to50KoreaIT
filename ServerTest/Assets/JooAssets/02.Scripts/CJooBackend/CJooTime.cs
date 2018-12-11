using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class CJooTime
{
	static int[] m_daysInMonth = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
	static int GetMonthDay(int pYear, int pMonth)
	{
		//월 단위에서 벗어나면
		if (pMonth < 1 || pMonth > 12)
		{
			//-1리턴
			return -1;
		}
		//2월이 아니면 평년 값을 리턴한다.
		if (pMonth != 2)
		{
			return m_daysInMonth[pMonth];
		}
		else
		{
			if (pYear % 400 == 0)
			{
				return m_daysInMonth[2] + 1;
			}
			else if (pYear % 100 == 0)
			{
				return m_daysInMonth[2];
			}
			else if (pYear % 4 == 0)
			{
				return m_daysInMonth[2] + 1;
			}
			else
			{
				return m_daysInMonth[2];
			}
		}
	}
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

	public CJooTime(string time)
	{
		string[] splitted = time.Split(m_split);
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
			int uValue = 0;
			if (value < 0)
			{
				Year--;
				uValue = 12 + value;
			}
			else
			{
				uValue = value;
			}
			int quotient = (uValue-1) / 12;
			int remainder = (uValue - 1) % 12 + 1;
			if (quotient > 0)
			{
				Year += quotient;
			}
			month = remainder;
		}
	}


	private int day = 0;
	public int Day
	{
		get
		{
			return day;
		}
		private set
		{
			int uValue = 0;
			if (value < 1)
			{
				Month--;
				uValue = GetMonthDay(Year, Month) + value;
			}
			else
			{
				uValue = value;
			}
			int quotient = uValue / GetMonthDay(Year, Month);
			int remainder = uValue % GetMonthDay(Year, Month);
			if (quotient > 0)
			{
				Month += quotient;
			}
			day = remainder;
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
			int uValue = 0;
			if (value < 0)
			{
				Day--;
				uValue = 24 + value;
			}
			else
			{
				uValue = value;
			}
			int quotient = uValue / 24;
			int remainder = uValue % 24;
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
			int uValue = 0;
			if (value < 0)
			{
				Hour--;
				uValue = 60 + value;
			}
			else
			{
				uValue = value;
			}
			int quotient = uValue / 60;
			int remainder = uValue % 60;
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
			int uValue = 0;
			if (value < 0)
			{
				Minute--;
				uValue = 60 + value;
			}
			else
			{
				uValue = value;
			}
			int quotient = uValue / 60;
			int remainder = uValue % 60;
			if (quotient > 0)
			{
				Minute += quotient;
			}
			second = remainder;
		}
	}

	CJooTime GetClone()
	{
		CJooTime tTime = new CJooTime();
		tTime.Year = this.Year;
		tTime.Month = this.Month;
		tTime.Day = this.Day;
		tTime.Hour = this.Hour;
		tTime.Minute = this.Minute;
		tTime.Second = this.Second;

		return tTime;
	}

	public static CJooTime operator -(CJooTime time_0, CJooTime time_1)
	{
		/*
		CJooTime tTime = new CJooTime();

		tTime.Year = time_0.Year - time_1.Year;
		Debug.Log(tTime.Year);

		tTime.Month = time_0.Month - time_1.Month;
		Debug.Log(tTime.Month);

		int tDay = time_0.Day - time_1.Day;
		Debug.Log(tDay);
		if (tDay < 0)
		{
			int MonthBackward = (time_0.Month == 1) ? 12 : time_0.Month - 1;
			int dayInMonth = GetMonthDay(time_0.Year, MonthBackward);
			tDay += dayInMonth;
		}
		tTime.day = tDay;

		CJooTime cloneTime = time_0.GetClone();
		cloneTime.Hour = cloneTime.Hour - time_1.Hour;
		int tHour = cloneTime.Hour;


		tTime.Hour = tHour;
		Debug.Log(tTime.Hour);


		tTime.Minute = time_0.Minute - time_1.Minute;
		Debug.Log(tTime.Minute);

		tTime.Second = time_0.Second - time_1.Second;
		Debug.Log(tTime.Second);



		return tTime;
		*/

		CJooTime tTime = time_0.GetClone();
		tTime.Second = tTime.Second - time_1.Second;
		tTime.Minute = tTime.Minute - time_1.Minute;
		tTime.Hour = tTime.Hour - time_1.Hour;
		tTime.Day = tTime.Day - time_1.Day;
		tTime.Month = tTime.Month - time_1.Month;
		tTime.Year = tTime.Year - time_1.Year;
		return tTime;
	}

	public override string ToString()
	{
		string t =
			Year.ToString() + "-" +
			Month.ToString("00") + "-" +
			Day.ToString("00") + "T" +
			Hour.ToString("00") + ":" +
			Minute.ToString("00") + ":" +
			Second.ToString("00");
		return t;
	}
	public int ToSecond()
	{
		if(Year + Month + Day + Hour > 0)
		{
			return -1;
		}
		else
		{
			int result = Minute * 60 + Second;
			return result;
		}
	}
	
}
