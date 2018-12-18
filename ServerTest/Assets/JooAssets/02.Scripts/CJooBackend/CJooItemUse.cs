using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using Newtonsoft.Json;

public class CJooItemUse : MonoBehaviour
{
	public CJooOneItemButton[] items = null;
	// Use this for initialization
	void Start ()
	{
		SetItemsFromServer();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void SetItemsFromServer()
	{
		BackendReturnObject bro = Backend.GameInfo.GetPrivateContents("item");
		string tReturnValue = bro.GetReturnValue();
		ItemFromServerRows tItems = JsonConvert.DeserializeObject<ItemFromServerRows>(tReturnValue);
		
		try
		{
			for (int i = 0; i < items.Length; i++)
			{
				items[i].ItemCount = tItems.rows[0].itemDict.M[string.Format("item{0}", (i + 1).ToString("00"))].ToInt();
			}
		}
		catch
		{
			for (int i = 0; i < items.Length; i++)
			{
				items[i].ItemCount = 0;
			}
		}
	}
}
