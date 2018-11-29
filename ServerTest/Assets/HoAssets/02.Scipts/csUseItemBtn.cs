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
	}

	public void GodOfShieldBtn() 
	{
		isGodOfShieldItem = true;
	}

	public void GodOfDestoryBtn() 
	{
		isGodOfDestoryItem = true;
	}

	public void GodOfSlowBtn()
	{
		isGodOfSlowItem = true;
	}

	public void GodOfHintBtn()
	{
		isGodOfHintItem = true;
	}

	public void GodOfHeartBtn()
	{
		isGodOfHeartItem = true;
	}

	public void ItemUseSetting()
	{
		isGodOfTimeItem = false;
		isGodOfShieldItem = false;
		isGodOfDestoryItem = false;
		isGodOfSlowItem = false;
		isGodOfHintItem = false;
		isGodOfHeartItem = false;
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
