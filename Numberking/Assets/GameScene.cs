using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour {
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
    public TMPro.TMP_Text m_CountText;
    float m_TimeScore;
    int m_Index;
    int m_NumOrder;
    List<Tile> m_TileList = new List<Tile>();
	// Use this for initialization
	IEnumerator Start () {
        Input.multiTouchEnabled = false;
        m_StartText.SetActive(true);
        yield return null;

        while (!Input.GetMouseButtonDown(0))
            yield return null;
        m_StartText.SetActive(false);

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
            if (m_NumOrder > m_MaxGameNum)
                break;
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
        }

        Destroy(tile);
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
            Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (col != null)
            {
                return col.GetComponent<Tile>();
            }
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
            }
        m_TileList.Sort((a, b) => a.m_Num.CompareTo(b.m_Num));
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
}
