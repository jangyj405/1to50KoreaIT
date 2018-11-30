using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csUseItemBtn : MonoBehaviour 
{
	private bool isGodOfTimeItem = false;
	private bool isGodOfShieldItem = false;
	private bool isGodOfDestoryItem = false;
	private bool isGodOfSlowItem = false;
	private bool isGodOfHintItem = false;
	private bool isGodOfHeartItem = false;

	public void GodOfTimeBtn() 
	{
		isGodOfTimeItem = true;
		csGameData.GetInstance ().IsClickTimeSkill = isGodOfTimeItem;
	}

	public void GodOfShieldBtn() 
	{
		isGodOfShieldItem = true;
		csGameData.GetInstance ().IsClickShieldSkill = isGodOfShieldItem;
	}

	public void GodOfDestoryBtn() 
	{
		isGodOfDestoryItem = true;
		csGameData.GetInstance ().IsClickDestorySkill = isGodOfDestoryItem;
	}

	public void GodOfSlowBtn()
	{
		isGodOfSlowItem = true;
		csGameData.GetInstance ().IsClickSlowSkill = isGodOfSlowItem;
	}

	public void GodOfHintBtn()
	{
		isGodOfHintItem = true;
		csGameData.GetInstance ().IsClickHintSkill = isGodOfHintItem;
	}

	public void GodOfHeartBtn()
	{
		isGodOfHeartItem = true;
		csGameData.GetInstance ().IsClickHeartSkill = isGodOfHeartItem;
	}

	public void ItemUseSetting()
	{
		isGodOfTimeItem = false;
		csGameData.GetInstance ().IsClickTimeSkill = isGodOfTimeItem;

		isGodOfShieldItem = false;
		csGameData.GetInstance ().IsClickShieldSkill = isGodOfShieldItem;

		isGodOfDestoryItem = false;
		csGameData.GetInstance ().IsClickDestorySkill = isGodOfDestoryItem;

		isGodOfSlowItem = false;
		csGameData.GetInstance ().IsClickSlowSkill = isGodOfSlowItem;

		isGodOfHintItem = false;
		csGameData.GetInstance ().IsClickHintSkill = isGodOfHintItem;

		isGodOfHeartItem = false;
		csGameData.GetInstance ().IsClickHeartSkill = isGodOfHeartItem;
	}

	public bool GetisGodOfTime
	{
		get
		{
			return isGodOfTimeItem;
		}
	}

	public bool GetisGodOfShield
	{
		get
		{
			return isGodOfTimeItem;
		}
	}

	public bool GetisGodOfDestory
	{
		get
		{
			return isGodOfDestoryItem;
		}
	}

	public bool GetisGodOfSlow
	{
		get
		{
			return isGodOfSlowItem;
		}
	}

	public bool GetisGodOfHint
	{
		get
		{
			return isGodOfHintItem;
		}
	}

	public bool GetisGodOfHeart
	{
		get
		{
			return isGodOfHeartItem;
		}
	}

}
