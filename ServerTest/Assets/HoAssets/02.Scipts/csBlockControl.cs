using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class csBlockControl : MonoBehaviour 
{
   //private static csBlockControl instance;

    private int RandomRotationNumber;
    private int RandomBlinkNumber;
    private int RandomReverseNumber;
    private int RandomChangeScaleNumber;
	private int RandomTwoTouchNumber;

	private GameCore GameCoreSript;
	private csMapData m_MapData;

	//void Start()
	//{
	//	//GameCoreSript = GameObject.FindGameObjectWithTag ("tagGameCore").GetComponent<GameCore> ();
	//	Init();
	//	RandomRotation ();
	//	RandomBlink ();
	//	RandomReverse ();
	//	RandomChangeScale ();
	//
	//}

	public void Init()
	{
		MapSettingInit ();
		//StartCoroutine("RandomRotation");
		//
		//StartCoroutine("RandomBlink");
		//
		//StartCoroutine("RandomReverse");
		//
		//StartCoroutine("RandomChangeScale");

		if (m_MapData.IsEdgeRotation == true) {
			StartCoroutine ("EdgeRotation");
		}

		InvokeRepeating ("RandomRotation", 0.5f, m_MapData.RotationTimer);

		InvokeRepeating ("RandomBlink", 0.5f, m_MapData.BlinkTimer);

		InvokeRepeating ("RandomReverse", 0.5f, m_MapData.ReverseTimer);

		InvokeRepeating ("RandomChangeScale", 0.5f, m_MapData.ScaleTimer);

	}

	void MapSettingInit()
	{
		m_MapData = csMapMgr.GetInstance ().MapSetting (CRyuGameDataMgr.GetInst().GetMapStageLevel);
		//Debug.Log (m_MapData.GetMapId);
		//Debug.Log (m_MapData.RotationCount);

		RandomRotationNumber = m_MapData.RotationCount;
		RandomBlinkNumber = m_MapData.BlinkCount;
		RandomReverseNumber = m_MapData.ReverseCount;
		RandomChangeScaleNumber = m_MapData.ScaleCount;
		RandomTwoTouchNumber = m_MapData.TrackCount;
		if (csGameData.GetInstance ().IsClickShieldSkill) {
			//csItemMgr.GetInstance ().UseGodOfShield (m_MapData);
			RandomRotationNumber = 0;
			RandomBlinkNumber = 0;
			RandomReverseNumber = 0;
			RandomChangeScaleNumber = 0;
			RandomTwoTouchNumber = 0;
		}
	}

 //public static csBlockControl Instance
 //{
 //    get
 //    {
 //        if (instance == null)
 //        {
 //            instance = FindObjectOfType<csBlockControl>();
 //            if (instance == null)
 //            {
 //                GameObject container = new GameObject("csBlockControl");
 //                instance = container.AddComponent<csBlockControl>();
 //            }
 //        }
 //        return instance;
 //    }
 //}
    public void RandomRotation()
    {
        StartCoroutine("Co_RandomRotation");
        
    }
    public void RandomBlink()
    {
        StartCoroutine("Co_RandomBlink");
       
        
    }
    public void RandomReverse()
    {
        StartCoroutine("Co_RandomReverse");
        
    }
    public void RandomChangeScale()
    {
        StartCoroutine("Co_RandomChangeScale");
        
    }

    //테두리 회전
    public void EdgeRotation()
    {
        StartCoroutine("Co_EdgeRotation");
    }




    //테두리 회전
    IEnumerator Co_EdgeRotation()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.5f);
            BkEdgeRotation();

        }
            
                
    }
    IEnumerator Co_RandomRotation()
    {

        int[] arrayInt = GetRandomNumList(RandomRotationNumber).ToArray();

		int repeatNum = (int)m_MapData.RotationTimer;
		if (repeatNum % 2 == 1) {
			repeatNum -= 1;
			if (repeatNum <= 0) {
				repeatNum = 2;
			}
		}

        for (int i = 0; i < arrayInt.Length; i++)
        {
			GameCore.Instance.m_TileList[arrayInt[i]].transform.DORotate(new Vector3(0f, 0f, 360f), 2.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(repeatNum, LoopType.Yoyo);
        }
        yield return null;

    }

    IEnumerator Co_RandomBlink()
    {

        int[] arrayInt = GetRandomNumList(RandomBlinkNumber).ToArray();

        for (int i = 0; i < arrayInt.Length; i++)
        {
            Color _color = GameCore.Instance.m_TileList[arrayInt[i]].GetComponent<SpriteRenderer>().material.color;
            Color alphaColor = new Color(_color.r, _color.g, _color.b, 0f);
			int repeatNum = (int)m_MapData.BlinkTimer;
			if (repeatNum % 2 == 1) {
				repeatNum -= 1;
				if (repeatNum <= 0) {
					repeatNum = 2;
				}
			}
			GameCore.Instance.m_TileList[arrayInt[i]].GetComponent<SpriteRenderer>().material.DOColor(alphaColor, 1f).SetLoops(repeatNum, LoopType.Yoyo);

        }
        yield return null;

    }

    //테두리 회전
    void BkEdgeRotation()
    {

        Vector2Int lastPos =GameCore.Instance.m_RotationOrder[GameCore.Instance.m_RotationOrder.Length - 1];
        Tile prevTile =GameCore.Instance.m_TileMap[lastPos.x, lastPos.y];
        for (int i = 0; i < GameCore.Instance.m_RotationOrder.Length; i++)
        {
            Vector2Int CurPos = GameCore.Instance.m_RotationOrder[i];
            Tile CurTile = GameCore.Instance.m_TileMap[CurPos.x, CurPos.y];
            prevTile.transform.DOMove(CurTile.transform.position, 0.5f);

            GameCore.Instance.m_TileMap[CurPos.x, CurPos.y] = prevTile;
            prevTile = CurTile;
        }
       
    }

    IEnumerator Co_RandomReverse()
    {
        int[] arrayInt = GetRandomNumList(RandomReverseNumber).ToArray();

        for (int i = 0; i < arrayInt.Length; i++)
        {
            GameCore.Instance.m_TileList[arrayInt[i]].transform.DORotate(new Vector3(0f, -180f, 0f), 1f);
        }
        yield return null;

    }


    IEnumerator Co_RandomChangeScale()
    {
        int[] arrayInt = GetRandomNumList(RandomChangeScaleNumber).ToArray();

		int repeatNum = (int)m_MapData.ScaleTimer;
		if (repeatNum % 2 == 1) {
			repeatNum -= 1;
			if (repeatNum <= 0) {
				repeatNum = 2;
			}
		}
        for (int i = 0; i < arrayInt.Length; i++)
        {
			GameCore.Instance.m_TileList[arrayInt[i]].transform.DOScale(new Vector3(0.7f, 0.7f, 0.1f), 1f).SetLoops(repeatNum, LoopType.Yoyo);
        }
        yield return null;
    }



    private List<int> GetRandomNumList(int ListLen)
    {

        List<int> resultList = new List<int>();
        Dictionary<int, bool> intDic = new Dictionary<int, bool>();

        for (int i = 0; i < ListLen; i++)
        {
            int RandomNum = Random.Range(GameCore.Instance.m_NumOrder - 1, GameCore.Instance.hasOnGame-1);

            if (intDic.ContainsKey(RandomNum))
            {
                i--;
                continue;
            }
            intDic.Add(RandomNum, true);
            resultList.Add(RandomNum);



        }

        return resultList;
    }

}
