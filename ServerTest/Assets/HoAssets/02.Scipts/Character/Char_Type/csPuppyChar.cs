using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1~3개의 다이아를 랜덤확률로 스테이지 클리어 할 때마다 랜덤확률로 지급
public class csPuppyChar : csCharacter {

	private int DiaRandomValue;
	private float DiaRandomValueRate;
	private float DiaRandomRate;
	private float puppyEffect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init()
	{
		CharName = "Puppy";
		CharLevel = 1;
		CharExp = 0;
		MaxCharExp = 10;
		puppyEffect = 5.0f;

	}

	public int PuppyEffect()
	{
		DiaRandomValueRate = Random.Range (0f, 100f);
		DiaRandomRate = Random.Range (0f, 100f);
		if (puppyEffect <= DiaRandomRate) {
			if (DiaRandomValueRate >= 0.0f && DiaRandomValueRate <= 70.0f) {
				DiaRandomValue = 1;
			} else if (DiaRandomValueRate > 70.0f && DiaRandomValueRate <= 90.0f) {
				DiaRandomValue = 2;
			} else if (DiaRandomValue > 90.0f) {
				DiaRandomValue = 3;
			}
		}
		return DiaRandomValue;
	}
	public void LevelUP()
	{
		base.LevelUP ();
		puppyEffect = charLevel * puppyEffect * 0.8f;

	}
}
