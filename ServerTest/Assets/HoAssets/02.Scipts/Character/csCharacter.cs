using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCharacter : MonoBehaviour
{
	protected string charName;
	protected int charLevel;
	protected int charExp;

	protected string CharName
	{
		get
		{
			return charName;
		}
		set
		{
			charName = value;
		}
	}

	protected string CharLevel
	{
		get
		{
			return charLevel;
		}
		set
		{
			charLevel = value;
		}
	}

	protected string CharExp
	{
		get
		{
			return charExp;
		}
		set
		{
			charExp = value;
		}
	}




}
