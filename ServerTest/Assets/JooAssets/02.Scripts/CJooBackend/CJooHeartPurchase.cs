using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class CJooHeartPurchase : MonoBehaviour
{
	[SerializeField]
	private Text tbcText = null;

	[SerializeField]
	private GameObject panelConfirm = null;

	[SerializeField]
	private Text txtConfirm = null;

	private string selectedProduct = "";

	private static readonly Dictionary<string, string> heartProducts = new Dictionary<string, string>()
	{
		{ "heart005", "8f7283a0-0ffd-11e9-a1ce-0bf9d1bde867"},
		{ "heart011", "95733a10-0ffd-11e9-97d6-a3a20419b215"},
		{ "heart025", "9b63eaf0-0ffd-11e9-a1ce-0bf9d1bde867"},
		{ "heart040", "a178c5a0-0ffd-11e9-97d6-a3a20419b215"},
		{ "heart055", "a94ed850-0ffd-11e9-a1ce-0bf9d1bde867"},
		{ "heart099", "ad646090-0ffd-11e9-97d6-a3a20419b215"}
	};
	/// <summary>
	/// key - product ID.
	/// value.key - TBC.
	///	value.value - heart.
	/// </summary>
	private static readonly Dictionary<string, KeyValuePair<int, int>> productValues = new Dictionary<string, KeyValuePair<int, int>>()
	{
		{"heart005", new KeyValuePair<int, int>(5,5) },
		{"heart011", new KeyValuePair<int, int>(10,11) },
		{"heart025", new KeyValuePair<int, int>(20,25) },
		{"heart040", new KeyValuePair<int, int>(30,40) },
		{"heart055", new KeyValuePair<int, int>(40,55) },
		{"heart099", new KeyValuePair<int, int>(70,99) },
	};

	void Start()
	{
		SetTbcOnUI();
	}

	void SetTbcOnUI()
	{
		BackendReturnObject bro = Backend.TBC.GetTBC();
		LitJson.JsonData tbcData = bro.GetReturnValuetoJSON();
		try
		{
			string tbc = tbcData["amountTBC"].ToString();
			tbcText.text = tbc;
		}
		catch
		{
			tbcText.text = string.Empty;
			Debug.Log(bro.GetReturnValue());
		}
	}

	public void OnClickBtnSelectProduct(string pProductID)
	{
		selectedProduct = pProductID;
		txtConfirm.text = string.Format("스타 {0}개를\n사용하여 하트 {1}개를\n구매하시겠습니까?", 
			productValues[selectedProduct].Key, productValues[selectedProduct].Value);
		panelConfirm.SetActive(true);
	}

	public void OnClickBtnCancelBuying()
	{
		selectedProduct = "";
		panelConfirm.SetActive(false);
	}

	public void OnClickBtnBuyHeart()
	{
		string UUID = heartProducts[selectedProduct];
		BackendReturnObject buyBro = Backend.TBC.UseTBC(UUID, selectedProduct);
		bool isServerAvail = CJooBackendCommonErrors.IsAvailableWithServer(buyBro);
		if (!isServerAvail)
		{
			panelConfirm.SetActive(false);
			//서버 응답 실패
			return;
		}
		string status = buyBro.GetStatusCode();
		if (status.Equals("400"))
		{
			panelConfirm.SetActive(false);
			//fail buying - TBC부족!!
			return;
		}
		UpdateHeart(selectedProduct);
		SetTbcOnUI();
		panelConfirm.SetActive(false);
	}

	void UpdateHeart(string pProductID)
	{
		int chargeHeartInt = productValues[pProductID].Value;

		BackendReturnObject heartBro = Backend.GameInfo.GetPrivateContents("heart");
		string heartOnServer = heartBro.GetReturnValue();
		JsonTableHeartRows heartTable = JsonUtility.FromJson<JsonTableHeartRows>(heartOnServer);
		int heartRecorded = heartTable.rows[0].HeartCount.ToInt();
		string tIndate = heartTable.rows[0].inDate.S;

		Param heartParam = new Param();
		heartParam.Add("HeartCount", heartRecorded + chargeHeartInt);
		Backend.GameInfo.Update("heart", tIndate, heartParam);
	}
}
