using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/Script Object/Map Data",fileName = "Stage_001")]
public class csMapData : ScriptableObject {

	[SerializeField]
	private string MapId;
	//[SerializeField]
	private float MapClearTime;

    [SerializeField]
    private int rotationCount;
    [SerializeField]
    private float rotationTimer;

    [SerializeField]
    private int blinkCount;
    [SerializeField]
    private float blinkTimer;

    [SerializeField]
    private int reverseCount;
    [SerializeField]
    private float reverseTimer;

    [SerializeField]
    private int scaleCount;
    [SerializeField]
    private float scaleTimer;

    [SerializeField]
    private int trackCount;
    [SerializeField]
    private int emptyCount;
	[SerializeField]
	private int maxGameNum;

	[SerializeField]
	private bool islimitTimer;
	[SerializeField]
	private float limitTimer;
	[SerializeField]
	private bool isEdgeRotation;

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


    public int RotationCount
    {
        get
        {
            return rotationCount;
        }
		set
		{
			rotationCount = value;
		}

    }

    public int BlinkCount
    {
        get
        {
            return blinkCount;
        }
		set
		{
			blinkCount = value;
		}
    }

    public int ReverseCount
    {
        get
        {
            return reverseCount;
        }
		set
		{
			reverseCount = value;
		}
    }

    public int ScaleCount
    {
        get
        {
            return scaleCount;
        }
		set
		{
			scaleCount = value;
		}
    }

    public int TrackCount
    {
        get
        {
            return trackCount;
        }
		set
		{
			trackCount = value;
		}
    }

    public int EmptyCount
    {
        get
        {
            return emptyCount;
        }
		set
		{
			emptyCount = value;
		}
    }

	public int MaxGameNum
	{
		get
		{
			return maxGameNum;
		}
		set
		{
			maxGameNum = value;
		}
	}

	public bool IsLimitTimer
	{
		get
		{
			return islimitTimer;
		}
		set
		{
			islimitTimer = value;
		}
	}

	public float LimitTimer
	{
		get
		{
			return limitTimer;
		}
		set
		{
			limitTimer = value;
		}
	}

    public float RotationTimer
    {
        get
        {
            return rotationTimer;
        }
    }

    public float BlinkTimer
    {
        get
        {
            return blinkTimer;
        }
    }

    public float ScaleTimer
    {
        get
        {
            return scaleTimer;
        }
    }

    public float ReverseTimer
    {
        get
        {
            return reverseTimer;
        }
    }

	public bool IsEdgeRotation
	{
		get
		{
			return isEdgeRotation;
		}
	}


    public int NothingCount
    {
        get
        {
            return 25 - (RotationCount + BlinkCount + ReverseCount + ScaleCount + TrackCount + EmptyCount);
        }
    }


}
