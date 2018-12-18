using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CJooTimeAtkRank : MonoBehaviour
{
	[SerializeField]
	private Text txtRank = null;

	[SerializeField]
	private Text txtNickName = null;

	[SerializeField]
	private Text txtScore = null;


	private int m_rank = 0;
	public int Rank
	{
		get
		{
			return m_rank;
		}
		private set
		{
			m_rank = value;
			txtRank.text = m_rank.ToString();
		}
	}

	private string m_nickname = "";
	public string Nickname
	{
		get
		{
			return m_nickname;
		}
		private set
		{
			m_nickname = value;
			txtNickName.text = m_nickname;
		}
	}

	private int m_score = 0;
	public int Score
	{
		get
		{
			return m_score;
		}
		private set
		{
			m_score = value;
			txtScore.text = m_score.ToString();
		}
	}


	public void InitialPanel(int pRank, string pNickname, int pScore)
	{
		Rank = pRank;
		Nickname = pNickname;
		Score = pScore;
	}
}
