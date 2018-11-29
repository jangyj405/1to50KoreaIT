using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csStageClearData
{
	private static csStageClearData m_Instance;

	//private float ClearTime;
	//private string StageId;
	Dictionary <string, string> ClearTimeData = new Dictionary<string, string>();
	public static csStageClearData GetInstance()
	{
		if (m_Instance == null) {
			m_Instance = new csStageClearData ();
		}
		return m_Instance;
	}

	//public void SetClearTime(float _time)
	//{
	//	ClearTime = _time;
	//}
	//
	//public void SetStageId(string _stageId)
	//{
	//	StageId = _stageId;		
	//}

	public void SetClearTime(string _stageId, string _clearTime)
	{
		ClearTimeData.Add (_stageId, _clearTime);		
	}

	public Dictionary<string,string> GetClearTimeData
	{
		get
		{
			return ClearTimeData;
		}
	}

}
