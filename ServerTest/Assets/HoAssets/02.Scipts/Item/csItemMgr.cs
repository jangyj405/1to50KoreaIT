using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemContainer
{
    //1번:시간  2번:쉴드  3번:슬로우  4번:힌트
	/// <summary>
	/// Server Item Key : item01
	/// </summary>
    public int GodOfTimeCount;
	/// <summary>
	/// Server Item Key : item02
	/// </summary>
	public int GodOfShildCount;
	/// <summary>
	/// Server Item Key : item03
	/// </summary>
	public int GodOfHeartCount;
	/// <summary>
	/// Server Item Key : item04
	/// </summary>
	public int GodOfSlowCount;
	/// <summary>
	/// Server Item Key : item05
	/// </summary>
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
		_mapData.ReverseCount = 0;
		_mapData.ScaleCount = 0;
	}

	public float UseGodOfSlow(float time)
	{
		//GameCore = GameObject.Find ("GameCore").GetComponent<GameCore>();
		//GameTime = GameCore.GameTime;
		time = (time + Time.deltaTime) * 0.9f;
		return time;
	}


}
