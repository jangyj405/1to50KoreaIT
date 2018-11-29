﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameCore : MonoBehaviour {

    private static GameCore instance;

    public float m_HelpTIme;
    public Transform m_StartPos;
    public Vector2 m_TileSize;
    public Vector2 m_TileSpacing;
    public Vector2Int m_TileNum;
    public int m_MaxGameNum;
    public GameObject m_TilePrefab;
    public TMPro.TMP_Text[] m_NextText;
    public TMPro.TMP_Text m_TimeScoreText;
    public GameObject m_StartText;
    public GameObject m_GameOverText;
    public GameObject m_LockText;
    public TMPro.TMP_Text m_CountText;
    Tile[,] m_TileMap;
    float m_TimeScore;
    int m_Index;
    public int m_NumOrder;
    int m_MissNum = 0;
    public int m_LockNum = 5;
    int m_stageIndex = 0;
    public float m_LimitTime = 200;

    public static GameCore Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameCore>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameCore");
                    instance = container.AddComponent<GameCore>();
                }
            }
            return instance;
        }
    }

    public int hasOnGame = 0;

	private csMapData m_MapData;

    public List<Tile> m_TileList = new List<Tile>();


	// Use this for initialization
	IEnumerator Start () {
        Input.multiTouchEnabled = false;
        m_StartText.SetActive(true);
        yield return null;

        while (!Input.GetMouseButtonDown(0))
            yield return null;
        m_StartText.SetActive(false);

		MapSettingInit ();
        Init();

        m_NumOrder = 1;
        StartCoroutine("UpdateNextNum");
        m_CountText.gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            m_CountText.text = (3 - i).ToString();
            yield return new WaitForSeconds(1);
        }
        m_CountText.text = "Go!";
        yield return new WaitForSeconds(1);
        m_CountText.gameObject.SetActive(false);
        //-----------------------------------------------


        /*
        StartCoroutine("RandomRotation");

        StartCoroutine("RandomBlink");

        StartCoroutine("RandomReverse");

        StartCoroutine("RandomChangeScale");

        InvokeRepeating("RandomRotation", 7.0f, 7.0f);

        InvokeRepeating("RandomBlink",7.0f,7.0f);

        InvokeRepeating("RandomReverse", 7.0f, 7.0f);

        InvokeRepeating("RandomChangeScale", 7.0f, 7.0f);
        */
        
        
        //-----------------------------------------------

        
		if (CRyuGameDataMgr.GetInst().GetMapStageLevel == 1)
        {
            //StartCoroutine("RandomRotation");
            csBlockControl.Instance.RandomRotation();
            
        }
        
       

        StartCoroutine("UpdateTimer");
        float t = Time.time;
        while (true)
        {
            Tile tile = GetTouchTile();
            if (tile != null)
            {
                if (tile.m_Num == m_NumOrder)
                {
                    StartCoroutine("TouchTile", tile);
                    m_NumOrder++;
                    t = Time.time;

                }
                else
                {

                    m_MissNum++;
                    Debug.Log("" + m_MissNum);
                    tile.Miss();
                   
                }

            }
            if(m_MissNum>=m_LockNum)
            {
                m_LockText.SetActive(true);
                yield return new WaitForSeconds(2.0f);
                m_LockText.SetActive(false);
                m_MissNum = 0;

            }
           
            if (Time.time - t > m_HelpTIme)
            {
                int index = m_NumOrder - 1;
                if (index >= 0 && index < m_MaxGameNum)
                {
                    if (m_TileList[index] != null && !m_TileList[index].IsPlaying())
                    {
                        m_TileList[index].Blink();
                    }
                }
            }
            if (m_TimeScore >= m_LimitTime)
            {
                Debug.Log("제한시간 초과");
                break;
            }
			if (m_NumOrder > m_MaxGameNum) {
				
				csStageClearData.GetInstance ().SetClearTime (m_MapData.GetMapId, m_TimeScoreText.text.ToString());
				break;
			}
            yield return null;
        }
        StopCoroutine("UpdateTimer");

        yield return new WaitForSeconds(0.25f);

        m_GameOverText.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        while (!Input.GetMouseButtonDown(0))
            yield return null;

        SceneManager.LoadScene(0);
    }

    IEnumerator TouchTile(Tile tile)
    {
        tile.FadeOut();
        yield return new WaitForSeconds(0.25f);
        if(m_NumOrder>26)
        {
            tile.gameObject.SetActive(false);
            //Destroy(tile);
        }
       
        while (tile.IsPlaying())
            yield return null;

        if (m_Index < m_MaxGameNum)
        {
           
            tile.m_Num = m_Index + 1;
            tile.m_NumText.text = tile.m_Num.ToString();
            m_TileList.Add(tile);
            m_Index++;
            tile.FadeIn();
            hasOnGame++;
            
        }

    }
    IEnumerator UpdateNextNum()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                int num = m_NumOrder + i;
                if (num <= m_MaxGameNum)
                    m_NextText[i].text = num.ToString();
                else
                    m_NextText[i].text = string.Empty;
            }
            yield return null;
        }
    }

    IEnumerator UpdateTimer()
    {
        yield return null;

        while (true)
        {
            m_TimeScore += Time.deltaTime;
            m_TimeScoreText.text = string.Format("{0:000.00}", m_TimeScore);
            yield return null;
        }
    }

    Tile GetTouchTile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*
            Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (col != null)
            {
                return col.GetComponent<Tile>();
            }
            */
            Vector2Int gridPos = WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (gridPos.x < 0 || gridPos.x >= m_TileNum.x
                || gridPos.y < 0 || gridPos.y >= m_TileNum.y)
                return null;


            return (m_TileMap[gridPos.x, gridPos.y].gameObject.activeSelf == true) ? m_TileMap[gridPos.x, gridPos.y] : null;
        }
        

        return null;
    }

    void Init()
    {
        m_Index = 0;
        int[] numArr = new int[m_TileNum.x * m_TileNum.y];
        for (int i = 0; i < numArr.Length ; i++)
            numArr[i] = i + 1;
        numArr = ShuffleArray(numArr);

        m_TileMap = new Tile[m_TileNum.x, m_TileNum.y];

        for (int y = 0; y < m_TileNum.y; y++)
            for (int x = 0; x < m_TileNum.x; x++)
            {
                Tile tile = Instantiate(m_TilePrefab).GetComponent<Tile>();
                m_TileMap[x, y] = tile;
                tile.transform.position = m_StartPos.position + new Vector3((x * (m_TileSize.x + m_TileSpacing.x)), (y * (-m_TileSize.y - m_TileSpacing.y)));
                tile.transform.localScale = new Vector3(m_TileSize.x, m_TileSize.y, 1);
                tile.m_Num = numArr[m_Index];
                tile.m_NumText.text = tile.m_Num.ToString();
                m_TileList.Add(tile);
                m_Index++;
                hasOnGame++;
            }
        m_TileList.Sort((a, b) => a.m_Num.CompareTo(b.m_Num));
    }
    Vector2Int WorldToGrid(Vector2 pos)
    {
        Vector2 startPos = new Vector2(
            m_StartPos.position.x - ((m_TileSize.x + m_TileSpacing.x) / 2f)
            , m_StartPos.position.y + ((m_TileSize.y + m_TileSpacing.y) / 2f));

        Vector3 look = pos - startPos;
        look.y *= -1;
        //Debug.DrawLine(startPos, pos);
        return new Vector2Int(
        Mathf.FloorToInt((look.x / (m_TileSize.x + m_TileSpacing.x)))
        , Mathf.FloorToInt((look.y / (m_TileSize.y + m_TileSpacing.y)))
        );
    }

    public void OnDrawGizmos()
    {
        if (m_StartPos == null)
            return;
        Gizmos.color = Color.green;
        for (int y = 0; y < m_TileNum.y; y++)
            for (int x = 0; x < m_TileNum.x; x++)
            {
                Gizmos.DrawWireCube(m_StartPos.position 
                    + new Vector3((x * (m_TileSize.x + m_TileSpacing.x)), (y * (-m_TileSize.y - m_TileSpacing.y)))
                    , m_TileSize);
            }
    }
    public T[] ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }
        return array;
    }
    
	void MapSettingInit()
	{
		m_MapData = csMapMgr.GetInstance ().MapSetting (CRyuGameDataMgr.GetInst().GetMapStageLevel);
		Debug.Log (m_MapData.GetMapId);
		Debug.Log (m_MapData.RotationCount);
		m_MaxGameNum = m_MapData.MaxGameNum;
        csBlockControl.Instance.RandomRotationNumber = m_MapData.RotationCount;
        csBlockControl.Instance.RandomBlinkNumber = m_MapData.BlinkCount;
        csBlockControl.Instance.RandomReverseNumber = m_MapData.ReverseCount;
        csBlockControl.Instance.RandomChangeScaleNumber = m_MapData.ScaleCount;


	}
    
   
}
