using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemContainer
{
    public int GodOfTimeCount;
    public int GodOfShildCount;
    public int GodOfHeartCount;
    public int GodOfSlowCount;
    public int GodOfHintCount;
}

public class csItemMgr
{
	private static csItemMgr m_Instance;
	public GameCore GameCore;
	private float GameTime;
	private csMapData MapData;

	public static csItemMgr GetInstance()
	{
		if (m_Instance == null) {
			m_Instance = new csItemMgr ();
		}
		return m_Instance;
	}

    private csItemMgr()
    {

    }


    public ItemContainer container = null;
   
    public void Initial()
    {
        //container = GetItemFromServer();

    }

	public float UseGodOfTime(float _AdvantageTime)
	{
		//GameCore = GameObject.Find ("GameCore").GetComponent<GameCore>();
		//GameTime = GameCore.GameTime;
		_AdvantageTime -= 2.0f;
		return _AdvantageTime;
	}

	//public void UseGodOfHint_01(SpriteRenderer spr,TMPro.TMP_Text text)
	//{
	//	spr.color = Color.grey;
	//	text.color = Color.black;
	//
	//}
	//
	//public void UseGodOfHint_02(SpriteRenderer spr,TMPro.TMP_Text text)
	//{
	//	
	//	spr.color = Color.black;
	//	text.color = Color.white;
	//
	//}

	public Color UseGodOfHint_SpritetRender(SpriteRenderer spr,Color _color)
	{
		spr.color = _color;
		return spr.color;
	}

	public Color UseGodOfHint_Text(TMPro.TMP_Text text ,Color _color)
	{
		text.color = _color;
		return text.color;
	}

	public void UseGodOfShield(csMapData _mapData)
	{
		_mapData.RotationCount = 0;
		_mapData.BlinkCount = 0;
		_mapData.EmptyCount = 0;
		_mapData.ReverseCount = 0;
		_mapData.ScaleCount = 0;
		_mapData.TrackCount = 0;
	}

	public float UseGodOfSlow(float time)
	{
		//GameCore = GameObject.Find ("GameCore").GetComponent<GameCore>();
		//GameTime = GameCore.GameTime;
		time = (time + Time.deltaTime) * 0.9f;
		return time;
	}

	public void UseGodOfDestory()
	{

	}

	public void UseGodOfHeart()
	{

	}

}
