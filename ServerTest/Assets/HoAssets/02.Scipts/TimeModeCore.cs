using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using System.Linq.Expressions;
using DG.Tweening;
using UnityEngine.UI;


public class TimeModeCore : MonoBehaviour {

    private static TimeModeCore instance;

    public float m_HelpTIme;
    public Transform m_StartPos;
    public Vector2 m_TileSize;
    public Vector2 m_TileSpacing;
    public Vector2Int m_TileNum;
    public GameObject m_TilePrefab;
    public TMPro.TMP_Text[] m_NextText;
    public TMPro.TMP_Text m_TimeScoreText;
    public GameObject m_StartText;
    public GameObject m_GameOverText;
    public TMPro.TMP_Text m_CountText;
    //public TMPro.TMP_Text m_CurrentScore;
    public Text m_CurrentScore;
    Sprite[] m_Sprite;
    Tile[,] m_TileMap;
    float m_TimeScore;
    int m_LockNum = 5;
    int m_MissNum = 0;
    int m_Index;
    public int m_NumOrder;
    public float m_LimitTime =60f;
    public GameObject m_LockText;
    List<Tile> m_TileList = new List<Tile>();

    public GameObject TimeOverPanel;   

    public static TimeModeCore Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TimeModeCore>();
                if (instance == null)
                {
                    GameObject container = new GameObject("TimeModeCore");
                    instance = container.AddComponent<TimeModeCore>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        TimeOverPanel.transform.localScale = Vector3.one * 0.1f;  

    }

    IEnumerator Start()
    {
        Input.multiTouchEnabled = false;
      //m_StartText.SetActive(true)
      //yield return null;
      //
      // while (!Input.GetMouseButtonDown(0))
      //     yield return null;
      // m_StartText.SetActive(false);

        Init();
       SoundManager.instance.BGMGameScene();
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

        StartCoroutine("UpdateTimer");
        float t = Time.time;
        while (true)
        {
            Tile tile = GetTouchTile();
            if (tile != null)
            {
                if (tile.m_Num == m_NumOrder)
                {
                    SoundManager.instance.SFXCorrectClick();
                    StartCoroutine("TouchTile", tile);
                    m_NumOrder++;
                    t = Time.time;
                }
                else
                {
                    SoundManager.instance.SFXMissClick();
                    m_MissNum++;
                    Debug.Log("" + m_MissNum);
                    tile.Miss();

                }
            }
            if (m_MissNum >= m_LockNum)
            {
                m_LockText.SetActive(true);
                yield return new WaitForSeconds(2.0f);
                m_LockText.SetActive(false);
                m_MissNum = 0;

            }
            if (Time.time - t > m_HelpTIme)
            {
                int index = m_NumOrder - 1;
                    if (m_TileList[index] != null && !m_TileList[index].IsPlaying())
                    {
                        m_TileList[index].Blink();
                    }
               
            }
            if (m_TimeScore >= m_LimitTime)
            {
                //StopCoroutine("UpdateTimer");
				isRunningTimer = false;
				int best = CJooStageClearData.Instance.SetTimeAtkScore(m_NumOrder - 1);
				CJooStageClearData.Instance.PushDataToServer(StageModeKind.TimeAttackMode);
                m_TimeScoreText.text = string.Format("{0:000.00}", (int)m_TimeScore);
                m_CurrentScore.text= string.Format("{000}",m_NumOrder-1);
                break;

            }
            yield return null;
        }
        // StopCoroutine("UpdateTimer");

        yield return new WaitForSeconds(0.25f);
    
        m_GameOverText.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        while (!Input.GetMouseButtonDown(0))
            yield return null;

        TimeOverPanel.SetActive(true);
        m_CurrentScore.gameObject.SetActive(true);
        TimeOverPanel.transform.DOScale(1f, 0.4f);

        
        //SceneManager.LoadScene("TimeAttackScene");
    }

    IEnumerator TouchTile(Tile tile)
    {
        tile.FadeOut();
        while (tile.IsPlaying())
            yield return null;

        tile.m_Num = m_Index + 1;
        tile.m_NumText.text = tile.m_Num.ToString();
        m_TileList.Add(tile);
        m_Index++;
        tile.FadeIn();
    }
    IEnumerator UpdateNextNum()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                int num = m_NumOrder + i;
                m_NextText[i].text = num.ToString();
            }
            yield return null;
        }
    }
	bool isRunningTimer = true;
    IEnumerator UpdateTimer()
    {
        //yield return null;

        while (isRunningTimer)
        {
            m_TimeScore += Time.deltaTime;
            m_TimeScoreText.text = string.Format("{0:000.00}",m_TimeScore);
            yield return null;
        }
    }
    Tile GetTouchTile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (col != null)
            {
                return col.GetComponent<Tile>();
            }*/

            Vector2Int gridPos = WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (gridPos.x < 0 || gridPos.x >= m_TileNum.x
                || gridPos.y < 0 || gridPos.y >= m_TileNum.y)
                return null;
            if (m_TileMap[gridPos.x, gridPos.y].IsDestory())
                return null;

            return (m_TileMap[gridPos.x, gridPos.y].gameObject.activeSelf == true) ? m_TileMap[gridPos.x, gridPos.y] : null;
        }
        return null;
    }
    void Init()
    {
        m_Index = 0;
        int[] numArr = new int[m_TileNum.x * m_TileNum.y];
        for (int i = 0; i < numArr.Length; i++)
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

    public void ReStartButtonClick()
    {
        Tweener tTweener = TimeOverPanel.transform.DOScale(0.1f, 0.4f);
        tTweener.OnComplete(() => ReStartButtonClickAfter());        
    }
    private void ReStartButtonClickAfter()
    {
        TimeOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButtonClick()
    {
        Tweener tTweener = TimeOverPanel.transform.DOScale(0.1f, 0.4f);
        tTweener.OnComplete(() => MainMenuButtonClickAfter());        
    }  
    private void MainMenuButtonClickAfter()
    {
        TimeOverPanel.SetActive(false);
        SoundManager.instance.BGMMainMenu();
        SceneManager.LoadScene(SceneNames.timeAttackScene);
    }
}
