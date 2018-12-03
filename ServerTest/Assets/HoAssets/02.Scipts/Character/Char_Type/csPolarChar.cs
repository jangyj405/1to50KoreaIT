using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//클리어한 시간 감소
public class csPolarChar : csCharacter {

	float DecreaseTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init()
	{
		CharName = "Polar";
		CharLevel = 1;
		CharExp = 0;
		MaxCharExp = 10;
		DecreaseTime = 0.15f;
	}

	public float PolarEffect(float Time)
	{
		Time -= DecreaseTime;
		return Time;
	}
	public void LevelUP()
	{
		base.LevelUP ();
		DecreaseTime = DecreaseTime + (CharLevel * 2);
	}
}
