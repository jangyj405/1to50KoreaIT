using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using Newtonsoft.Json;


public class CJooStageClearData
{
	private static CJooStageClearData m_instance = null;
	public static CJooStageClearData Instance
	{
		get
		{
			if(m_instance == null)
			{
				m_instance = new CJooStageClearData();
			}
			return m_instance;
		}
	}

	private Dictionary<string, int> DictStageClear = null;
	private string stageInDate = "";

	private int timeAtkScore = 0;
	private string timeAtkInDate = "";

	public void SetStageClearDict(Dictionary<string,int> pDict, string pInDate)
	{

	}

	public bool PushDataToServer(StageModeKind gameMode)
	{
		if(DictStageClear == null)
		{
			return false;
		}

		bool tResult = false;

		switch (gameMode)
		{
			case StageModeKind.StageMode:
				tResult = PushStageDataToServer();
				break;

			case StageModeKind.TimeAttackMode:
				tResult = PushTimeAtkDataToServer();
				break;
		}

		return tResult;
	}

	private bool PushStageDataToServer()
	{
		bool isFirst = (timeAtkInDate == "");
		//BackendReturnObject bro = Backend.GameInfo.Update();
		return false;
	}

	private bool PushTimeAtkDataToServer()
	{
		return false;
	}

	public void AddStageClearToDict(KeyValuePair<string, int> pClearData)
	{
		if(DictStageClear == null)
		{
			return;
		}
		DictStageClear.Add(pClearData.Key, pClearData.Value);
	}
}
