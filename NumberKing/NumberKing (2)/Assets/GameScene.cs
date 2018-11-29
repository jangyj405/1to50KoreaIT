using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameScene : MonoBehaviour {

    private static GameScene instance;

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
    float m_TimeScore;
    int m_Index;
    int m_NumOrder;
    int m_MissNum = 0;
    public int m_LockNum = 5;
    int m_stageIndex = 0;
    public float m_LimitTime = 200;

	private int RandomRotationNumber;
	private int RandomBlinkNumber;
	private int RandomReverseNumber;
	private int RandomChangeScaleNumber;

    int hasOnGame = 0;

	private csMapData m_MapData;


    public static GameScene Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameScene>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameScene");
                    instance = container.AddComponent<GameScene>();
                }
            }
            return instance;
        }
    }


	//public void EdgeBlockMove()
	//{
	//	//테투리 회전
	//	for(int i = 0; i< edgeCube.Length;i++)
	//	{
	//		Vector3 Target = Vector3.zero;
	//		Target = edgeCube[(i+1)%edgeCube.Length].position;
	//		edgeCube[i].DOMove(Target, 1f);
	//	}
	//}

    List<Tile> m_TileList = new List<Tile>();


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


        
        StartCoroutine("RandomRotation");

        StartCoroutine("RandomBlink");

        StartCoroutine("RandomReverse");

        StartCoroutine("RandomChangeScale");

        InvokeRepeating("RandomRotation", 7.0f, 7.0f);

        InvokeRepeating("RandomBlink",7.0f,7.0f);

        InvokeRepeating("RandomReverse", 7.0f, 7.0f);

        InvokeRepeating("RandomChangeScale", 7.0f, 7.0f);
        



        //-----------------------------------------------

        
		if (CRyuGameDataMgr.GetInst().GetMapStageLevel == 1)
        {
            StartCoroutine("RandomRotation");
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
            //힌트
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

    }

    // 회전 랜덤 생성
    public void RandomRotation()
    {

        int[] arrayInt = GetRandomNumList(RandomRotationNumber).ToArray();

        for(int i=0;i< arrayInt.Length;i++)
        {
            m_TileList[arrayInt[i]].transform.DORotate(new Vector3(0f, 0f, 360f), 2.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }

    }
    // 깜박임
    public void RandomBlink()
    {
       
        int[] arrayInt = GetRandomNumList(RandomBlinkNumber).ToArray();

        for (int i = 0; i < arrayInt.Length; i++)
        {
            Color _color = m_TileList[arrayInt[i]].GetComponent<SpriteRenderer>().material.color;
            Color alphaColor = new Color(_color.r, _color.g, _color.b, 0f);
            m_TileList[arrayInt[i]].GetComponent<SpriteRenderer>().material.DOColor(alphaColor, 1f).SetLoops(6, LoopType.Yoyo);

        }
       
    }


    // 숫자 뒤집기
    public void RandomReverse()
    {
        int[] arrayInt = GetRandomNumList(RandomReverseNumber).ToArray();

        for (int i = 0; i < arrayInt.Length; i++)
        {
            m_TileList[arrayInt[i]].transform.DORotate(new Vector3(0f, -180f, 0f), 1f);
        }
    }

    // 숫자 커지기
    public void RandomChangeScale()
    {
        int[] arrayInt = GetRandomNumList(RandomChangeScaleNumber).ToArray();

        for (int i = 0; i < arrayInt.Length; i++)
        {
            m_TileList[arrayInt[i]].transform.DOScale(new Vector3(0.7f, 0.7f, 0.1f), 1f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private List<int> GetRandomNumList(int ListLen)
    {
       
        List<int> resultList = new List<int>();
        Dictionary<int, bool> intDic = new Dictionary<int, bool>();

        for (int i = 0; i < ListLen; i++)
        {
            int RandomNum = Random.Range(m_NumOrder-1, hasOnGame);
            
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



    // 번호가 생성되는곳 
    IEnumerator TouchTile(Tile tile)
    {
        tile.FadeOut();
        while (tile.IsPlaying())
            yield return null;

        if (m_Index < m_MaxGameNum)
        {
            Tile newTile = Instantiate(m_TilePrefab).GetComponent<Tile>();
            newTile.transform.position = tile.transform.position;
            newTile.transform.localScale = tile.transform.localScale;
            newTile.m_Num = m_Index + 1;
            newTile.m_NumText.text = newTile.m_Num.ToString();
            m_TileList.Add(newTile);
            m_Index++;
            newTile.FadeIn();
            hasOnGame++;
        }

        Destroy(tile);
    }

    // 지금 눌러야 하는곳 보여주는 곳
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

    // 시간 나타내는곳
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

    // 터치하는 곳
    Tile GetTouchTile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (col != null)
            {
                return col.GetComponent<Tile>();
            }
        }
        return null;
    }

    // 처음 숫자가 생성되는 곳 
    void Init()
    {
        m_Index = 0;
        int[] numArr = new int[m_TileNum.x * m_TileNum.y];
        for (int i = 0; i < numArr.Length ; i++)
            numArr[i] = i + 1;
        numArr = ShuffleArray(numArr);
        // 가로세로 숫자판이 생성되는곳
        for (int y = 0; y < m_TileNum.y; y++)
            for (int x = 0; x < m_TileNum.x; x++)
            {
                Tile tile = Instantiate(m_TilePrefab).GetComponent<Tile>();
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

    // 초록색이 생기는곳 
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
    // 랜덤으로 생성되는 곳
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
		Debug.Log (m_MapData.GetRotationCount);
		RandomRotationNumber = m_MapData.GetRotationCount;
		RandomBlinkNumber = m_MapData.GetBlinkCount;
		RandomReverseNumber = m_MapData.GetReverseCount;
		RandomChangeScaleNumber = m_MapData.GetScaleCount;


	}
   
}
