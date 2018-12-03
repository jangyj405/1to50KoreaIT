using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//블록 방해효과 1개 랜덤 제거
public class csCamelChar : csCharacter 
{
	public enum RAND_DELETE_EFFECT{BLINK = 0, ROTATION, REVERSE, SCALE, TRACK, EMPTY};
	private int randomeffect;
	private Dictionary<int, RAND_DELETE_EFFECT> RandomEffect = new Dictionary<int,RAND_DELETE_EFFECT>()
	{
		{0, RAND_DELETE_EFFECT.BLINK},
		{1, RAND_DELETE_EFFECT.ROTATION},
		{2, RAND_DELETE_EFFECT.REVERSE},
		{3, RAND_DELETE_EFFECT.SCALE},
		{4, RAND_DELETE_EFFECT.TRACK},
		{5, RAND_DELETE_EFFECT.EMPTY},
	};
	private float EffectCount;

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
		CharName = "Camel";
		CharLevel = 1;
		CharExp = 0;
		MaxCharExp = 10;

	}

	public RAND_DELETE_EFFECT CamelEffect()
	{
		randomeffect = Random.Range ((int)RAND_DELETE_EFFECT.BLINK, (int)RAND_DELETE_EFFECT.EMPTY+1);
		return RandomEffect [randomeffect];
	}
	public void LevelUP()
	{
		base.LevelUP ();

	}
}
