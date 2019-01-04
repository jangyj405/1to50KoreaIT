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
using UnityEngine.UI;

[Serializable]
public class JsonTableStage : JsonTableBase
{
	[SerializeField]
	public JsonStageRecord StageRecord;
}

[Serializable]
public class JsonTableStageRow
{
	[SerializeField]
	public JsonTableStage[] rows;
}

[Serializable]
public class JsonStageRecord
{
	[SerializeField]
	public Dictionary<string, JsonN> M;
}





public class StageView : MonoBehaviour
{
	public static StageView stageView = null;
	public int SelectedStage;
    public Text toggleText;

    public GameObject startScreen;
	public CJooOneStageButton[] stageButtons = null;
	private int radix = 0;
	private Dictionary<string, int> stageDict;
	public KeyValuePair<string, int>[] stageKVPair;
    public GameObject HeartPanel;

	void Start()
	{
		SceneManager.LoadScene("AddtiveSceneETC", LoadSceneMode.Additive);
        toggleText.text = "";
		InitButtons();
		//todo 
		string tInDate = "";
		stageDict = GetStageDataFromServer(out tInDate);
		if(!tInDate.Equals(""))
		{
			CJooStageClearData.Instance.SetStageClearDict(stageDict, tInDate);
		}
		//받은 값으로 세팅하기
		//값이 없으면(기록이 없으면) 버튼 interactable false
		
		
		SetStageButtonsAsStageDict();
	}

	private void InitButtons()
	{
		for (int i = 0; i < stageButtons.Length; i++)
		{
			stageButtons[i].Stage = radix * 10 + i + 1;
			stageButtons[i].RecordText = "-";
			stageButtons[i].IsInteractable = false;
		}
	}

	private void SetStageButtonsAsStageDict()
	{
		string tFormat = string.Format("Stage_{0}", radix.ToString("00"));
		string tFromatNext = string.Format("Stage_{0}" + "0", (radix + 1).ToString("00"));
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
		for (int i = 0; i < stageButtons.Length; i++)
		{
			if (stageDict.ContainsKey(string.Format("Stage_{0}", (stageButtons[i].Stage - 1).ToString("000"))))
			{
				stageButtons[i].IsInteractable = true;
			}
		}
	}

	public void OnClickBtnPageView(bool pIsRight)
	{
		radix = (pIsRight) ? radix + 1 : radix - 1;
		radix = Mathf.Clamp(radix, 0, 4);
		InitButtons();
		SetStageButtonsAsStageDict();
	}


	private Dictionary<string, int> GetStageDataFromServer(out string oInDate)
	{
		BackendReturnObject userBro = Backend.BMember.GetUserInfo();
		string userBroStr = userBro.GetReturnValue();

		UserMetaData metaData = JsonUtility.FromJson<UserMetaData>(userBroStr);

		string tIndate = metaData.row.inDate; 

		Debug.Log(tIndate);
		BackendReturnObject bro = Backend.GameInfo.GetPublicContentsByGamerIndate("stage", tIndate);
		string tReturnValue = bro.GetReturnValue();
		Debug.Log(tReturnValue);
		JsonTableStageRow stageData = JsonConvert.DeserializeObject<JsonTableStageRow>(tReturnValue);
		
		try
		{
			oInDate = stageData.rows[0].inDate.S;
			Dictionary<string, int> tDict = new Dictionary<string, int>();
			foreach(var item in stageData.rows[0].StageRecord.M.Keys)
			{
				int tVal = Convert.ToInt32(stageData.rows[0].StageRecord.M[item].N);
				tDict.Add(item, tVal);
			}
			return tDict;
		}
		catch(Exception e)
		{
			Debug.Log(e.Message);
			oInDate = "";
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
		CRyuGameDataMgr.GetInst().GetMapStageLevel = SelectedStage;
#if UNITY_EDITOR
		CheckItemsUsed(); FadeInOut.instance.FadeIn(SceneNames.stageModeScene);
		return;
#endif
		CJooHeart.jooHeart.OnClickBtnUseHeart(() => { CheckItemsUsed();  FadeInOut.instance.FadeIn(SceneNames.stageModeScene); });
		
	}

	private void CheckItemsUsed()
	{
		//인데이트가 없으면 오류가 발생했다는 것
		if (CJooItemUse.inDate.Equals(""))
		{
			//리턴 때린다
			return;
		}
		//체크된 아이템 배열을 가져온다
		bool[] isUsed = CJooItemUse.jooItem.IsItemsUsed();
		//체크된 아이템이 있는지 없는지 체크
		int ItemUsedCount = 0;
		for (int i = 0; i< isUsed.Length;i++)
		{
			if(isUsed[i] == true)
			{
				ItemUsedCount++;
			}
		}
		//아이템을 사용하지 않았으면 리턴때린다.
		if(ItemUsedCount == 0)
		{
			return;
		}
		//1번:시간  2번:쉴드  3번:슬로우  4번:힌트
		csGameData.GetInstance().IsClickTimeSkill = isUsed[0];
		csGameData.GetInstance().IsClickShieldSkill = isUsed[1];
		csGameData.GetInstance().IsClickSlowSkill = isUsed[2];
		csGameData.GetInstance().IsClickHintSkill = isUsed[3];

		Dictionary<string, int> itemsDict = new Dictionary<string, int>();
		string tItemStr = "item";
		for(int i = 0; i< CJooItemUse.jooItem.items.Length;i++)
		{
			string tKey = tItemStr + (i+1).ToString("00");
			int tVal = (isUsed[i]) ? CJooItemUse.jooItem.items[i].ItemCount - 1 : CJooItemUse.jooItem.items[i].ItemCount;
			itemsDict.Add(tKey, tVal);
		}
		Param tParam = new Param();
		tParam.Add("itemDict", itemsDict);
		BackendReturnObject bro = Backend.GameInfo.Update("item", CJooItemUse.inDate, tParam);
		Debug.Log(bro.GetStatusCode());
	}

	public void ButtonBack()
    {
        FadeInOut.instance.FadeIn(SceneNames.modeSelectScene);
    }

    public void ToggleClick(int index)
    {
        switch (index)
        {
            case 1:
                {
                    toggleText.text = "시간을 -2초 해줍니다";
                    break;
                }
            case 2:
                {
                    toggleText.text = "모든 방해 기능들을 제거합니다";
                    break;
                }
            case 3:
                {
                    toggleText.text = "시간을 10% 느리게가게 만듭니다";
                    break;
                }
            case 4:
                {
                    toggleText.text = "힌트를 바로 볼 수 있게 합니다";
                    break;
                }
        }
    }

    public void ButtonCloseClick()
    {
        HeartPanel.SetActive(false);
    }

}
