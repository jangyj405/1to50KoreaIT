using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMapMgr
{

	private static csMapMgr m_Instance;
	private csMapData[] MapDatats;
	private csMapData tempData;
	private int MapDataCount = 0;

	public static csMapMgr GetInstance()
	{
		if (m_Instance == null) {
			m_Instance = new csMapMgr ();
		}
		return m_Instance;
	}

	public void MapInit()
	{
		MapDatats = Resources.LoadAll<csMapData> ("MapData");

	}

	public csMapData MapSetting(int _StageLevel)
	{
		MapInit ();
		for (int i = 0; i < MapDatats.Length; ++i) {
			if (MapDatats [i].name.Contains (_StageLevel.ToString())) {
				
				tempData = MapDatats [i];
				break;
			} else {
				return null;
			}
		}
		return tempData;
	}


}
