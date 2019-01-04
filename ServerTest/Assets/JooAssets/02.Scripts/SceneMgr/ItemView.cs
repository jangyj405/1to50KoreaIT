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
	public Dictionary<string, int> itemPrices = new Dictionary<string, int>()
	{
		{ "item01", 3},
		{ "item02", 4},
		{ "item03", 3},
		{ "item04", 4}
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

	public void ItemPurchasClick()
    {
        if (ItemPurchasScreen != null)
            ItemPurchasScreen.SetActive(true);
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
