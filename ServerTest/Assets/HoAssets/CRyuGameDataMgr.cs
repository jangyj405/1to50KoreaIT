using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRyuGameDataMgr
{
    private static CRyuGameDataMgr mInstance = null;

	private int mStageIndex = 1;




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
		get
		{
			return mStageIndex;
		}
	}
}
