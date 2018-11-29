﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/Script Object/Map Data",fileName = "Stage_001")]
public class csMapData : ScriptableObject {

	[SerializeField]
	private string MapId;
	//[SerializeField]
	private float MapClearTime;
    [SerializeField]
    private int RotationCount;
    [SerializeField]
    private int BlinkCount;
    [SerializeField]
    private int ReverseCount;
    [SerializeField]
    private int ScaleCount;
    [SerializeField]
    private int TrackCount;
    [SerializeField]
    private int EmptyCount;

	public string GetMapId
	{
		get
		{
			return MapId;
		}
	}

	public float GetMapClearTime
	{
		get
		{
			return MapClearTime;
		}
	}


    public int GetRotationCount
    {
        get
        {
            return RotationCount;
        }
    }

    public int GetBlinkCount
    {
        get
        {
            return BlinkCount;
        }
    }

    public int GetReverseCount
    {
        get
        {
            return ReverseCount;
        }
    }

    public int GetScaleCount
    {
        get
        {
            return ScaleCount;
        }
    }

    public int GetTrackCount
    {
        get
        {
            return TrackCount;
        }
    }

    public int GetEmptyCount
    {
        get
        {
            return EmptyCount;
        }
    }

    public int GetNothingCount
    {
        get
        {
            return 25 - (RotationCount + BlinkCount + ReverseCount + ScaleCount + TrackCount + EmptyCount);
        }
    }


}