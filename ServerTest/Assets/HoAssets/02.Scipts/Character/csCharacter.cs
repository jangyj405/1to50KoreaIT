using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCharacter : MonoBehaviour
{
	protected string charName;
	protected int charLevel;
	protected int charExp;
	protected int maxCharExp;

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

	protected int CharLevel
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

	protected int CharExp
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

	protected int MaxCharExp
	{
		get
		{
			return maxCharExp;
		}
		set
		{
			maxCharExp = value;
		}
	}

	protected void LevelUP()
	{
		if (charExp >= maxCharExp) {
			charLevel += 1;
			if (charLevel >= 10) {
				charLevel = 10;
			}
			charExp = 0;
			maxCharExp *= 2;
		}
	}


}
