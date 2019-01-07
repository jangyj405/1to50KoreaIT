using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Globalization;
using System.Text.RegularExpressions;
using System;


public static class CJooDiaCounter
{
	public static int GetTBCAmount()
	{
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
	}
	
}
