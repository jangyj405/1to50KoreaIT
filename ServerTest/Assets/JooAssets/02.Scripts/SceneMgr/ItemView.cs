using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BackEnd;
using Newtonsoft.Json;

public class ItemView : MonoBehaviour
{

	public GameObject ItemPurchasScreen;
	public GameObject DiaPanel;
	public Text diaText = null;
	public Dictionary<string, string> itemUUIDs = new Dictionary<string, string>()
	{
		{ "item01", "e1bbe200-0ffd-11e9-a1ce-0bf9d1bde867"},
		{ "item02", "e6c31d40-0ffd-11e9-97d6-a3a20419b215"},
		{ "item03", "eb441180-0ffd-11e9-a1ce-0bf9d1bde867"},
		{ "item04", "eea739b0-0ffd-11e9-97d6-a3a20419b215"}
	};
	private Dictionary<string, int> itemsHave = null;
	void Start()
	{
		ItemPurchasScreen.SetActive(false);
		diaText.text = CJooDiaCounter.GetTBCAmount().ToString();
	}

	
	public static string inDate = "";
	Dictionary<string,int> GetItemsFromServer()
	{
		BackendReturnObject bro = Backend.GameInfo.GetPrivateContents("item");
		string tReturnValue = bro.GetReturnValue();
		ItemFromServerRows tItems = JsonConvert.DeserializeObject<ItemFromServerRows>(tReturnValue);
		Dictionary<string, int> tDictItem = new Dictionary<string, int>();
		try
		{
			inDate = tItems.rows[0].inDate.S;
			for (int i = 0; i < 4; i++)
			{
				tDictItem.Add("item"+(i+1).ToString("00") ,tItems.rows[0].itemDict.M[string.Format("item{0}", (i + 1).ToString("00"))].ToInt());
			}
			return tDictItem;
		}
		catch
		{
			inDate = "";
			for(int i =0; i< 4;i++)
			{
				tDictItem.Add("item" + (i + 1).ToString(), 0);
			}
			return tDictItem;
		}
	}
	private string selectedItem = "";
	public void ItemPurchasClick(string pItemKey)
    {
        if (ItemPurchasScreen != null)
		{
			selectedItem = pItemKey;
			ItemPurchasScreen.SetActive(true);
		}
    }
	public void OnClickBtnBuyItem()
	{
		if(selectedItem.Equals(""))
		{
			ItemPurchasScreen.SetActive(false);
			return;
		}
		string tUUID = itemUUIDs[selectedItem];

		BackendReturnObject buyBro = Backend.TBC.UseTBC(tUUID, selectedItem);
		bool isServerAvail = CJooBackendCommonErrors.IsAvailableWithServer(buyBro);
		if (!isServerAvail)
		{
			ItemPurchasScreen.SetActive(false);
			DiaPanel.gameObject.SetActive(true);
			//서버 응답 실패
			return;
		}
		string status = buyBro.GetStatusCode();
		if (status.Equals("400"))
		{
			ItemPurchasScreen.SetActive(false);
			DiaPanel.gameObject.SetActive(true);
			//fail buying - TBC부족!!
			return;
		}

		itemsHave[selectedItem] = itemsHave[selectedItem] + 1;
		UpdateItem();
		DiaPanel.gameObject.SetActive(true);
	}

	private void UpdateItem()
	{
		Param itemParam = new Param();
		itemParam.Add("itemDict", itemsHave);

		Backend.GameInfo.Update("item", inDate, itemParam);
	}

    public void NoClick()
    {
        ItemPurchasScreen.SetActive(false);
    }

    public void CloseClick()
    {
        if (ModeSelect.stageModeKind == StageModeKind.StageMode)
        {
            FadeInOut.instance.FadeIn(SceneNames.stageScene);
        }
        else if (ModeSelect.stageModeKind == StageModeKind.TimeAttackMode)
        {
            FadeInOut.instance.FadeIn(SceneNames.timeAttackScene);
        }
    }

    public void ButtonDiaClose()
    {
        DiaPanel.SetActive(false);
    }
}
