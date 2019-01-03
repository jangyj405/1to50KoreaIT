using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public static class CJooDiaCounter
{
	public static int GetTBCAmount()
	{
		string tbcAmount = Backend.TBC.GetAmountTBC();
		Debug.Log(tbcAmount);
		return 0;
	}
	
}
