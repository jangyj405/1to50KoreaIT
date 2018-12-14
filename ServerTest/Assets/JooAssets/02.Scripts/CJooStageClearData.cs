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

	private CJooStageClearData()
	{

	}

	private Dictionary<string, int> DictStageClear = new Dictionary<string, int>();
	private string stageInDate = "";

	private int timeAtkScore = 0;
	private string timeAtkInDate = "";

	public void SetStageClearDict(Dictionary<string,int> pDict, string pInDate)
	{
		DictStageClear = pDict;
		stageInDate = pInDate;
	}

	public void SetTimeAtkScore(int pScore, string pInDate)
	{
		timeAtkScore = pScore;
		timeAtkInDate = pInDate;
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
		bool isFirst = (stageInDate == "");
		Param stageParam = new Param();
		stageParam.Add("StageRecord", DictStageClear);
		BackendReturnObject bro = (isFirst) ? 
			Backend.GameInfo.Insert("stage", stageParam) : Backend.GameInfo.Update("stage", stageInDate, stageParam);
		bool tResult = CJooBackendCommonErrors.IsAvailableWithServer(bro);
		return tResult;
	}

	private bool PushTimeAtkDataToServer()
	{
		bool isFirst = (timeAtkInDate == "");
		Param timeAtkParam = new Param();
		timeAtkParam.Add("InfiniteScore", timeAtkScore);
		BackendReturnObject bro = (isFirst) ?
			Backend.GameInfo.Insert("score", timeAtkParam) : Backend.GameInfo.Update("score", timeAtkInDate, timeAtkParam);
		bool tResult = CJooBackendCommonErrors.IsAvailableWithServer(bro);
		return tResult;
	}

	public void AddStageClearToDict(KeyValuePair<string, int> pClearData)
	{
		if (DictStageClear.ContainsKey(pClearData.Key))
		{
			if(DictStageClear[pClearData.Key] < pClearData.Value)
			{
				return;
			}
			DictStageClear[pClearData.Key] = pClearData.Value;
		}
		else
		{
			DictStageClear.Add(pClearData.Key, pClearData.Value);
		}
	}

	public void SetTimeAtkScore(int pScore)
	{
		if(pScore < timeAtkScore)
		{
			return;
		}
		timeAtkScore = pScore;
	}
}
