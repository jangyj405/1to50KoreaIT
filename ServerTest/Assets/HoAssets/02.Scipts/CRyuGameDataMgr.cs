using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRyuGameDataMgr
{
    private static CRyuGameDataMgr mInstance = null;

	private int mStageIndex = 1;
    private int getCurrentStageLevel;

    private int mCurrentStageIndex = 0;
    public int CurrentStageIndex
    {
        get
        {
            return mCurrentStageIndex;
        }
        set
        {
            if(value > mStageIndex)
            {
                mCurrentStageIndex = 0;
            }
            else
            {
                mCurrentStageIndex = value;
            }
        }
    }




    protected CRyuGameDataMgr()
    {
        mInstance = null;
    }
    public static CRyuGameDataMgr GetInst()
    {
        if (mInstance == null)
        {
            mInstance = new CRyuGameDataMgr();
        }


        return mInstance;
    }
    public void CreateRyu()
    {
       
    }

    public void DestroyRyu()
    {

    }
	public int GetMapStageLevel 
	{
		get {
			return mStageIndex;
		}
	}

	public void IncreaseStageLevel()
	{
		++mStageIndex;
	}
    public int CurrentStageLevel
    {
        get
        {
            return getCurrentStageLevel;
        }
        set
        {
            getCurrentStageLevel = value;
        }
    }
}
