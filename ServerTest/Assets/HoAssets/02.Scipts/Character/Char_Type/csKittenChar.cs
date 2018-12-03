using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 하트 최대 갯수 증가
public class csKittenChar : csCharacter 
{
	private float MaxHeratRate;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Init()
	{
		CharName = "Kitten";
		CharLevel = 1;
		CharExp = 0;
		MaxCharExp = 10;
		MaxHeratRate = 0.25f;

	}

	public float KittenEffect()
	{
		return MaxHeratRate;
	}
	public void LevelUP()
	{
		base.LevelUP ();
		MaxHeratRate = charLevel * MaxHeratRate * 0.8f;

	}
}
