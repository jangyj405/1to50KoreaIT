using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csItemMgr
{
	private static csItemMgr m_Instance;
	public GameScene GameCore;
	private float GameTime;
	private csMapData MapData;

	public static csItemMgr GetInstance()
	{
		if (m_Instance == null) {
			m_Instance = new csItemMgr ();
		}
		return m_Instance;
	}

	public float UseGodOfTime()
	{
		GameCore = GameObject.Find ("GameScene").GetComponent<GameScene>();
		GameTime = GameCore.GameTime;
		GameTime -= 2.0f;
		return GameTime;
	}

	public float UseGodOfHint()
	{
		return 0.5f;
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

	public float UseGodOfSlow()
	{
		GameCore = GameObject.Find ("GameScene").GetComponent<GameScene>();
		GameTime = GameCore.GameTime;
		GameTime *= 0.9f;
		return GameTime;
	}

	public void UseGodOfDestory(Tile _tile,int _currentIndexNum)
	{
		//int RemoveIndex = _currentIndexNum;
		//for (int i = 0; i < 10; ++i) {
		//	_tile.m_Num = _currentIndexNum + 1;
		//
		//}
		//return RemoveIndex;
	}

	public void UseGodOfHeart()
	{

	}


}
