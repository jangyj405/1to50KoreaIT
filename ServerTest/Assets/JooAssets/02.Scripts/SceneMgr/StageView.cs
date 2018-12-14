using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using BackEnd;
using Newtonsoft.Json;
using System.Linq;
using System.Linq.Expressions;

[Serializable]
public class JsonTableStage : JsonTableBase
{
	[SerializeField]
	public Dictionary<string, int> StageRecord;
}

[Serializable]
public class JsonTableStageRow
{
	[SerializeField]
	public JsonTableStage[] row;
}







public class StageView : MonoBehaviour
{
	public static StageView stageView = null;
	public int SelectedStage;

    public GameObject startScreen;
	public CJooOneStageButton[] stageButtons = null;
	private int radix = 0;
	private Dictionary<string, int> stageDict;
	public KeyValuePair<string, int>[] stageKVPair;
	void Start()
	{
		SceneManager.LoadScene("AddtiveSceneETC", LoadSceneMode.Additive);

		for (int i = 0; i < stageButtons.Length; i++)
		{
			stageButtons[i].Stage = radix * 10 + i + 1;
			stageButtons[i].RecordText = "-";
			stageButtons[i].IsInteractable = false;
		}
		//todo 
		stageDict = GetStageDataFromServer();
		
		//받은 값으로 세팅하기
		//값이 없으면(기록이 없으면) 버튼 interactable false
		string tFormat = string.Format("Stage_{0}", radix.ToString("00"));
		string tFromatNext = string.Format("Stage_{0}" + "0", (radix + 1).ToString("00"));
		Debug.Log(tFromatNext);
		if (stageDict != null)
		{
			stageKVPair = stageDict.Where((t) => (t.Key.Contains(tFormat) && t.Key.Last() != '0') || t.Key.Equals(tFromatNext))
														   .OrderBy((t) => t.Key)
														   .Select((t) => t)
														   .ToArray();

			for (int i = 0; i < stageKVPair.Length; i++)
			{
				stageButtons[i].RecordText = (stageKVPair[i].Value * 0.01).ToString("000.00");
				stageButtons[i].IsInteractable = true;
			}
		}
		else
		{
			stageDict = new Dictionary<string, int>();
			stageDict.Add("Stage_000", 0);
		}
		for(int i =0; i< stageButtons.Length;i++)
		{
			if(stageDict.ContainsKey(string.Format("Stage_{0}", (stageButtons[i].Stage-1).ToString("000"))))
			{
				stageButtons[i].IsInteractable = true;
			}
		}
	}

	private Dictionary<string, int> GetStageDataFromServer()
	{
		BackendReturnObject bro = Backend.GameInfo.GetPublicContents("stage");
		string tReturnValue = bro.GetReturnValue();
		JsonTableStageRow stageData = JsonConvert.DeserializeObject<JsonTableStageRow>(tReturnValue);
		try
		{
			return stageData.row[0].StageRecord;
		}
		catch
		{
			return null;
		}
	}


	void Awake()
	{
		stageView = this;
	}

    public void StageClick()
    {
		startScreen.SetActive(true);
		/*
        startScreen.SetActive(true);
		//Debug.Log (EventSystem.current.currentSelectedGameObject.name);
		if ((int.Parse(EventSystem.current.currentSelectedGameObject.name.Substring (12)) > CRyuGameDataMgr.GetInst ().GetMapStageLevel)) 
        {
			Debug.Log ("Lock");
		} 
        else
        {
			csMapMgr.GetInstance ().MapSetting (CRyuGameDataMgr.GetInst ().GetMapStageLevel);
		}
		Debug.Log (CRyuGameDataMgr.GetInst().GetMapStageLevel);
		*/


	}

    public void StageCloseClick()
    {
        startScreen.SetActive(false);
    }

    public void GameStartClick()
    {
		csMapMgr.GetInstance().MapSetting(SelectedStage);
		SceneManager.LoadScene(SceneNames.stageModeScene);
    }
		
}
