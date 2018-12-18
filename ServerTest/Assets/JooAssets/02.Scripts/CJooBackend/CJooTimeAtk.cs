using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using Newtonsoft.Json;

public class CJooTimeAtk : MonoBehaviour
{
	public static string inDate = "";

	public CJooTimeAtkRank panelRankPF;
	public GameObject content = null;
	// Use this for initialization
	void Start ()
	{
		Initial();
		BackendReturnObject broRank = Backend.Rank.RankList();
		string tUUID = broRank.GetReturnValuetoJSON()["rows"][0]["uuid"]["S"].ToString();
		GetMyRankFromServer(tUUID);
		SetRankFromServer(tUUID);
	}
	

	void Initial()
	{
		BackendReturnObject userBro = Backend.BMember.GetUserInfo();
		UserMetaData meta = JsonConvert.DeserializeObject<UserMetaData>(userBro.GetReturnValue());
		
		try
		{
			inDate = meta.row.inDate;
			BackendReturnObject tBro = Backend.GameInfo.GetPublicContentsByGamerIndate("score", inDate);
			string tScore = tBro.GetReturnValuetoJSON()["rows"][0]["InfiniteScore"]["N"].ToString();
			Debug.Log(tScore);
			int tScoreInt = System.Convert.ToInt32(tScore);
			CJooStageClearData.Instance.SetTimeAtkScore(0, inDate);
		}
		catch
		{
			return;
		}
		
	}

	void OnDisable()
	{
		
	}


	void SetRankFromServer(string pUUID)
	{
		BackendReturnObject tBro = Backend.Rank.GetRankByUuid(pUUID, 10);

		try
		{
			RankTimeAtk tRank = JsonConvert.DeserializeObject<RankTimeAtk>(tBro.GetReturnValue());
			for(int i =0; i< tRank.rows.Length;i++)
			{
				CJooTimeAtkRank tRankPanel = Instantiate<CJooTimeAtkRank>(panelRankPF, content.transform);
				tRankPanel.InitialPanel(tRank.rows[i].rank.ToInt(), tRank.rows[i].nickname.S, tRank.rows[i].score.ToInt());
			}
		}
		catch
		{
			return;
		}
	}

	void GetMyRankFromServer(string pUUID)
	{
		BackendReturnObject tBro = Backend.Rank.GetMyRank(pUUID);
		try
		{
			RankTimeAtk tRank = JsonConvert.DeserializeObject<RankTimeAtk>(tBro.GetReturnValue());

			CJooTimeAtkRank tRankPanel = Instantiate<CJooTimeAtkRank>(panelRankPF, content.transform);
			tRankPanel.InitialPanel(tRank.rows[0].rank.ToInt(), tRank.rows[0].nickname.S, tRank.rows[0].score.ToInt());
			CJooStageClearData.Instance.SetTimeAtkScore(tRank.rows[0].score.ToInt(), inDate);
		}
		catch
		{
			return;
		}
	}


	[System.Serializable]
	struct RankTimeAtk
	{
		[SerializeField]
		public OneRankTimeAtk[] rows;
	}

	[System.Serializable]
	struct OneRankTimeAtk
	{
		[SerializeField]
		public JsonS nickname;
		[SerializeField]
		public JsonN score;
		[SerializeField]
		public JsonN rank;
	}
}
