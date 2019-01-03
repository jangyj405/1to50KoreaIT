using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using Newtonsoft.Json;

public class CJooItemUse : MonoBehaviour
{
	public static CJooItemUse jooItem = null;
	void Awake()
	{
		jooItem = this;
	}
	public CJooOneItemButton[] items = null;
	// Use this for initialization
	void Start ()
	{
		SetItemsFromServer();
	}
	
	public bool[] IsItemsUsed()
	{
		List<bool> tBoolList = new List<bool>();
		for(int i = 0; i< items.Length;i++)
		{
			tBoolList.Add(items[i].toggle.isOn);
		}
		return tBoolList.ToArray();
	}
	public static string inDate = "";
	void SetItemsFromServer()
	{
		BackendReturnObject bro = Backend.GameInfo.GetPrivateContents("item");
		string tReturnValue = bro.GetReturnValue();
		ItemFromServerRows tItems = JsonConvert.DeserializeObject<ItemFromServerRows>(tReturnValue);
		
		try
		{
			inDate = tItems.rows[0].inDate.S;
			for (int i = 0; i < items.Length; i++)
			{
				items[i].ItemCount = tItems.rows[0].itemDict.M[string.Format("item{0}", (i + 1).ToString("00"))].ToInt();
			}
		}
		catch
		{
			inDate = "";
			for (int i = 0; i < items.Length; i++)
			{
				items[i].ItemCount = 0;
			}
		}
	}
}
