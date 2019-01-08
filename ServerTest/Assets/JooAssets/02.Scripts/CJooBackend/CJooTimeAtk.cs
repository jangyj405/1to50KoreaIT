using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using Newtonsoft.Json;
using System;
using UnityEngine.UI;

public class CJooTimeAtk : MonoBehaviour
{
	/*
	private static class RankTime
	{
		private static readonly DateTime StartTime = new DateTime(2018, 12, 30, 15, 00,00,DateTimeKind.Utc);
		private static DateTime availTime;
		public static bool IsAvailableScore(CJooTime pTime)
		{
			if (pTime == null)
			{
				return false;
			}
			DateTime dateTime = DateTime.UtcNow;
			int count = 0;
			while (true)
			{
				TimeSpan timeSpan = dateTime - (StartTime.AddDays(count * 7));
				if (timeSpan < new TimeSpan(7, 0, 0, 0))
				{
					availTime = StartTime.AddDays(count * 7);
					break;
				}
				else
				{
					count++;
				}
			}
			try
			{
				DateTime scoreTime = new DateTime(pTime.Year, pTime.Month, pTime.Day, pTime.Hour, pTime.Minute, pTime.Second);

				if (availTime - scoreTime > new TimeSpan(0))
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			catch
			{
				return false;
			}
		}
	}
	*/


	public static string inDate = "";
	public static string rankUUID = "";
	public CJooTimeAtkRank panelRankPF;
	public GameObject content = null;
	// Use this for initialization
	void Start ()
	{
		Initial();
		BackendReturnObject broRank = Backend.Rank.RankList();
        try
        {
            rankUUID = broRank.GetReturnValuetoJSON()["rows"][0]["uuid"]["S"].ToString();
        }
	    catch
        {

        }
		GetMyRankFromServer(rankUUID);
		SetRankFromServer(rankUUID);
	}
	

	void Initial()
	{
		BackendReturnObject userBro = Backend.BMember.GetUserInfo();
		UserMetaData meta = JsonConvert.DeserializeObject<UserMetaData>(userBro.GetReturnValue());
		string tinDate = "";
		try
		{
			tinDate = meta.row.inDate;
		
		}
		catch
		{
			
		}
		string updateTime = "";
		int tScoreInt = 0;
		try
		{
			if (tinDate.Equals(""))
			{
				return;
			}
			BackendReturnObject tBro = Backend.GameInfo.GetPublicContentsByGamerIndate("score", tinDate);
			string tScore = tBro.GetReturnValuetoJSON()["rows"][0]["InfiniteScore"]["N"].ToString();
			inDate = tBro.GetReturnValuetoJSON()["rows"][0]["inDate"]["S"].ToString();
			updateTime = tBro.GetReturnValuetoJSON()["rows"][0]["updatedAt"]["S"].ToString();
			//Debug.Log(tScore);
			//Debug.Log(inDate);
			tScoreInt = System.Convert.ToInt32(tScore);

		}
		catch
		{
			tScoreInt = 0;
			inDate = "";

		}
		finally
		{
			//bool isAvail = RankTime.IsAvailableScore(new CJooTime(updateTime));
			CJooStageClearData.Instance.SetTimeAtkScore(tScoreInt, inDate);
		}



	}

	void OnDisable()
	{
		//0.87 1 0.6 1
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
			tRankPanel.GetComponent<Image>().color = new Color(0.87f, 1f, 0.6f, 1f);
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
