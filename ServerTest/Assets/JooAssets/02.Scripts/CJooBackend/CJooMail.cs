using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BackEnd;
using UnityEngine.UI;
using Newtonsoft.Json;

[Serializable]
public class ItemFromAdmin
{
	[SerializeField]
	public JsonItemFromAdmin[] items;
}


[Serializable]
public class JsonNum
{
	[SerializeField]
	public int N;
}

[Serializable]
public class JsonBool
{
	[SerializeField]
	public bool BOOL;
}

[Serializable]
public class CJooPostFromAdmin
{
	[SerializeField]
	public JsonS content;

	[SerializeField]
	public JsonS expirationDate;

	[SerializeField]
	public JsonS inDate;

	[SerializeField]
	public JsonItemFromAdmin item;

	[SerializeField]
	public JsonS sentDate;

	[SerializeField]
	public JsonS receiverNickname;

	[SerializeField]
	public JsonS title;
	
}

[Serializable]
public class JsonItemFromAdmin
{
	[SerializeField]
	public JsonItemContent M;
}


[Serializable]
public class JsonItemContent
{
	[SerializeField]
	public JsonN num;

	[SerializeField]
	public JsonS item01;

	[SerializeField]
	public JsonS item02;

	[SerializeField]
	public JsonS item03;

	[SerializeField]
	public JsonS item04;

	[SerializeField]
	public JsonS item05;

	[SerializeField]
	public JsonS content;
}

[Serializable]
public class JsonPostFromBackendConsole
{
	[SerializeField]
	public CJooPostFromAdmin[] fromAdmin;
}

//receiver: [Object], // 쪽지 받은사람의 inDate
//            sender: [Object], // 쪽지 보낸사람의 inDate
//            content: [Object], // 쪽지 내용
//            inDate: [Object], // 쪽지의 inDate
//            senderNickname: [Object], // 쪽지 보낸사람의 닉네임
//            isRead: [Object], // 받은사람이 읽었는지 판단하는 기준 (String : y/n)
//            receiverNickname: [Object] // 쪽지 받은사람의 닉네임
//

[Serializable]
public class CJooPostFromUser
{
	[SerializeField]
	public JsonS receiver;

	[SerializeField]
	public JsonS sender;

	[SerializeField]
	public JsonS content;

	[SerializeField]
	public JsonS inDate;

	[SerializeField]
	public JsonS senderNickname;

	[SerializeField]
	public JsonBool isRead;

	[SerializeField]
	public JsonS receiverNickname;

}

[Serializable]
public class CJooPostFromUserRows
{
	[SerializeField]
	public CJooPostFromUser[] rows = new CJooPostFromUser[1];
}

[Serializable]
public class ItemFromServerRows
{
	[SerializeField]
	public ItemFromServer[] rows;
}

[Serializable]
public class ItemFromServer : JsonTableBase
{
	[SerializeField]
	public ItemsDic itemDict;
}

[Serializable]
public class ItemsDic
{
	[SerializeField]
	//public ItemCont 
	public Dictionary<string, JsonN> M;
}

[Serializable]
public class ItemCont
{
	[SerializeField]
	public JsonN item01;
	[SerializeField]
	public JsonN item02;
	[SerializeField]
	public JsonN item03;
	[SerializeField]
	public JsonN item04;
	[SerializeField]
	public JsonN item05;
}

public class CJooMail : MonoBehaviour
{
	public static CJooMail mail = null;
	public Dictionary<string, CJooPostItem> postItemDic = new Dictionary<string, CJooPostItem>();
	public GameObject postListContainerUI = null;
	public CJooPostItemFromUser postUserPrefab;
	public CJooPostItemFromAdmin postAdminPrefab;
	void Awake()
	{
		mail = this;
	}

	void OnEnable()
	{

	}
	// Use this for initialization
	void Start ()
	{
		GetUserPostListFromServer();
		GetAdminPostListFromServer();
	}



	// Update is called once per frame
	void Update ()
	{
		
	}

	void GetUserPostListFromServer()
	{
		//todo//
		 BackendReturnObject bro = Backend.Social.Message.GetReceivedMessageList();
		string result = bro.GetReturnValue();
		Debug.Log(result);
		CJooPostFromUserRows tData = JsonUtility.FromJson<CJooPostFromUserRows>(result);
		//Debug.Log(tData.rows[0].isRead.BOOL);
		foreach(CJooPostFromUser userPost in tData.rows)
		{
			if(userPost.isRead.BOOL == true)
			{
				continue;
			}
			string tContent = userPost.content.S;
			string tInDate = userPost.inDate.S;
			string tSenderNickname = userPost.senderNickname.S;
			CJooPostItemFromUser item = Instantiate<CJooPostItemFromUser>(postUserPrefab, postListContainerUI.transform);
			item.Initial(tContent, tInDate, tSenderNickname);
			postItemDic.Add(tInDate, item);
		}
	}

	void GetAdminPostListFromServer()
	{
		BackendReturnObject bro = Backend.Social.Post.GetPostList();
		string result = bro.GetReturnValue();
		Debug.Log(result);
		JsonPostFromBackendConsole tData = JsonUtility.FromJson<JsonPostFromBackendConsole>(result);
		//Debug.Log(tData.fromAdmin[0].item.M.content.S);
		foreach (var adminPost in tData.fromAdmin)
		{
			string tContent = adminPost.content.S;
			string tInDate = adminPost.inDate.S;
			List<int> tItemList = new List<int>();
			tItemList.Add(Convert.ToInt32(adminPost.item.M.item01.S));
			tItemList.Add(Convert.ToInt32(adminPost.item.M.item02.S));
			tItemList.Add(Convert.ToInt32(adminPost.item.M.item03.S));
			tItemList.Add(Convert.ToInt32(adminPost.item.M.item04.S));
			tItemList.Add(Convert.ToInt32(adminPost.item.M.item05.S));
			CJooPostItemFromAdmin item = Instantiate<CJooPostItemFromAdmin>(postAdminPrefab, postListContainerUI.transform);
			item.Initial(tContent, tInDate, tItemList.ToArray());
			postItemDic.Add(tInDate, item);
		}
	}

	public void OnClickBtnReceiveAll()
	{
		int tReceivedHeart = 0;
		foreach (var item in postItemDic.Values)
		{
			if (item is CJooPostItemFromUser)
			{
				Backend.Social.Message.GetReceivedMessage(item.InDate);
				tReceivedHeart++;
			}
		}
		//todo 하트 갱신
		UpdateHeart(tReceivedHeart);

		BackendReturnObject bro = Backend.Social.Post.ReceiveAdminPostAll();
		string tReturnValue = bro.GetReturnValue();

		ItemFromAdmin tItem = JsonUtility.FromJson<ItemFromAdmin>(tReturnValue);
		CJooTempItemContainer.Instance.ClearContainer();

		foreach (var adminItem in tItem.items)
		{
			List<string> keys = new List<string>();
			List<int> values = new List<int>();

			keys.AddRange(new string[] { "item01", "item02", "item03", "item04", "item05" });
			int val01 = Convert.ToInt32(adminItem.M.item01.S);
			int val02 = Convert.ToInt32(adminItem.M.item02.S);
			int val03 = Convert.ToInt32(adminItem.M.item03.S);
			int val04 = Convert.ToInt32(adminItem.M.item04.S);
			int val05 = Convert.ToInt32(adminItem.M.item05.S);
			values.AddRange(new int[] { val01, val02, val03, val04, val05 });
			CJooTempItemContainer.Instance.AddToContainer(keys.ToArray(), values.ToArray());
		}
		//todo 아이템 갱신
		UpdateItem();
	}

	void UpdateHeart(int pReceivedHeart)
	{
		BackendReturnObject bro = Backend.GameInfo.GetPrivateContents("heart");
		string tReturnValue = bro.GetReturnValue();
		JsonTableHeartRows heartTable = JsonConvert.DeserializeObject<JsonTableHeartRows>(tReturnValue);
		string tInDate = heartTable.rows[0].inDate.S;
		int heartCountOnServer = Convert.ToInt32(heartTable.rows[0].HeartCount.N);
		Param heartParam = new Param();
		heartParam.Add("HeartCount", heartCountOnServer + pReceivedHeart);
		Backend.GameInfo.Update("heart", tInDate, heartParam);
	}

	void UpdateItem()
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
		itemParam.Add("itemDict",CJooTempItemContainer.Instance.DicItemContainer);
		Backend.GameInfo.Update("item", tInDate, itemParam);
		CJooTempItemContainer.Instance.ClearContainer();
	}


#if UNITY_EDITOR
	void OnGUI()
	{
		if(GUI.Button(new Rect(0f,0f,100f,100f),"ReceiveAll"))
		{
			OnClickBtnReceiveAll();
		}
	}
#endif
}
