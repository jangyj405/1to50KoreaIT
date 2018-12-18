using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using Newtonsoft.Json;
using System;

public class CJooPostItemFromAdmin : CJooPostItem
{
	protected int[] itemCount;
	public int[] ItemCount
	{
		get
		{
			return itemCount;
		}
		protected set
		{
			itemCount = value;
		}
	}

	public override string Content
	{
		get
		{
			return content;
		}

		protected set
		{
			content = value;
		}
	}

	public override string InDate
	{
		get
		{
			return inDate;
		}
		protected set
		{
			inDate = value;
		}
	}

	public override void GetItems()
	{
		CJooTempItemContainer.Instance.ClearContainer();

		BackendReturnObject broReceive = Backend.Social.Post.ReceiveAdminPostItem(InDate);
		Debug.Log(broReceive.GetReturnValue());

		string tValue = broReceive.GetReturnValue();
		Item tItem = JsonConvert.DeserializeObject<Item>(tValue);
		List<string> keys = new List<string>();
		List<int> values = new List<int>();

		keys.AddRange(new string[] { "item01", "item02", "item03", "item04", "item05" });
		values.Add(tItem.item.M["item01"].ToInt());
		values.Add(tItem.item.M["item02"].ToInt());
		values.Add(tItem.item.M["item03"].ToInt());
		values.Add(tItem.item.M["item04"].ToInt());
		values.Add(tItem.item.M["item05"].ToInt());

		CJooTempItemContainer.Instance.AddToContainer(keys.ToArray(), values.ToArray());
		//{"item":{"M":{"num":{"N":"1"},"item05":{"S":"3"},"item04":{"S":"3"},"item03":{"S":"3"},"item02":{"S":"3"},"item01":{"S":"3"},"content":{"S":"1위 선물"}}}}
		UpdateItem();
		CJooTempItemContainer.Instance.ClearContainer();
		DeleteThis();
	}

	[Serializable]
	struct DictItem
	{
		public Dictionary<string, JsonS> M;
	}

	[Serializable]
	struct Item
	{
		public DictItem item;
	}


	private void UpdateItem()
	{
		
		BackendReturnObject bro = Backend.GameInfo.GetPrivateContents("item");
		string tReturnValue = bro.GetReturnValue();
		ItemFromServerRows itemFromServer = JsonConvert.DeserializeObject<ItemFromServerRows>(tReturnValue);
		string tInDate = itemFromServer.rows[0].inDate.S;
		List<string> keys = new List<string>();
		List<int> values = new List<int>();

		keys.AddRange(new string[] { "item01", "item02", "item03", "item04", "item05" });
		int val01 = Convert.ToInt32(itemFromServer.rows[0].itemDict.M["item01"].N);
		int val02 = Convert.ToInt32(itemFromServer.rows[0].itemDict.M["item02"].N);
		int val03 = Convert.ToInt32(itemFromServer.rows[0].itemDict.M["item03"].N);
		int val04 = Convert.ToInt32(itemFromServer.rows[0].itemDict.M["item04"].N);
		int val05 = Convert.ToInt32(itemFromServer.rows[0].itemDict.M["item05"].N);
		values.AddRange(new int[] { val01, val02, val03, val04, val05 });
		CJooTempItemContainer.Instance.AddToContainer(keys.ToArray(), values.ToArray());

		Param itemParam = new Param();
		itemParam.Add("itemDict", CJooTempItemContainer.Instance.DicItemContainer);
		Backend.GameInfo.Update("item", tInDate, itemParam);
	}
	protected override void DeleteThis()
	{
		Destroy(gameObject);
	}

	public void Initial(string pContent, string pInDate, int[] pItemCount)
	{
		Content = pContent;
		InDate = pInDate;
		ItemCount = pItemCount;
		txtContent.text = string.Format("{0} 보상이 도착했습니다!", Content);
	}
}
