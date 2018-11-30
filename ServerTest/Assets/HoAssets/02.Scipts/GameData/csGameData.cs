using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csGameData
{
	private static csGameData m_Instance;

	private bool isClickTimeSkill;
	private bool isClickShieldSkill;
	private bool isClickDestorySkill;
	private bool isClickSlowSkill;
	private bool isClickHintSkill;
	private bool isClickHeartSkill;

	public static csGameData GetInstance()
	{
		if (m_Instance == null) {
			m_Instance = new csGameData ();
		}
		return m_Instance;
	}

	public bool IsClickTimeSkill
	{
		get
		{
			return isClickTimeSkill;
		}
		set
		{
			isClickTimeSkill = value;
		}
	}

	public bool IsClickShieldSkill
	{
		get
		{
			return isClickShieldSkill;
		}
		set
		{
			isClickShieldSkill = value;
		}
	}

	public bool IsClickDestorySkill
	{
		get
		{
			return isClickDestorySkill;
		}
		set
		{
			isClickDestorySkill = value;
		}
	}

	public bool IsClickSlowSkill
	{
		get
		{
			return isClickSlowSkill;
		}
		set
		{
			isClickSlowSkill = value;
		}
	}

	public bool IsClickHintSkill
	{
		get
		{
			return isClickHintSkill;
		}
		set
		{
			isClickHintSkill = value;
		}
	}

	public bool IsClickHeartSkill
	{
		get
		{
			return isClickHeartSkill;
		}
		set
		{
			isClickHeartSkill = value;
		}
	}


}
