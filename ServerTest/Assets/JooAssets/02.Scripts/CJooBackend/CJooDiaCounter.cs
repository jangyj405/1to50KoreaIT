using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Globalization;
using System.Text.RegularExpressions;
using System;


public static class CJooDiaCounter
{
	public static string GetTBCAmount()
	{
		/*
		string tbcAmount = Backend.TBC.GetAmountTBC();
		string strTmp = Regex.Replace(tbcAmount, @"\D", "");
		int nTmp = 0;
		try
		{
			nTmp = int.Parse(strTmp);
		}
		catch
		{
			nTmp = 0;
		}
		return nTmp;
		*/

		BackendReturnObject bro = Backend.TBC.GetTBC();
		LitJson.JsonData tbcData = bro.GetReturnValuetoJSON();
		string tbc = tbcData["amountTBC"].ToString();
		return tbc;

		/*
		int ttbc = 0;
		try
		{
			ttbc = int.Parse(tbc);
		}
		catch
		{
			ttbc = 0;
		}
		return ttbc;
		*/
	}
}
