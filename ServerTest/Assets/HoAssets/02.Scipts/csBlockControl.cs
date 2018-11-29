using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class csBlockControl : MonoBehaviour 
{
    private static csBlockControl instance;

    public int RandomRotationNumber;
    public int RandomBlinkNumber;
    public int RandomReverseNumber;
    public int RandomChangeScaleNumber;

    public static csBlockControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<csBlockControl>();
                if (instance == null)
                {
                    GameObject container = new GameObject("csBlockControl");
                    instance = container.AddComponent<csBlockControl>();
                }
            }
            return instance;
        }
    }
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







    IEnumerator Co_RandomRotation()
    {

        int[] arrayInt = GetRandomNumList(RandomRotationNumber).ToArray();

        for (int i = 0; i < arrayInt.Length; i++)
        {
            GameCore.Instance.m_TileList[arrayInt[i]].transform.DORotate(new Vector3(0f, 0f, 360f), 2.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
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
            GameCore.Instance.m_TileList[arrayInt[i]].GetComponent<SpriteRenderer>().material.DOColor(alphaColor, 1f).SetLoops(6, LoopType.Yoyo);

        }
        yield return null;

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

        for (int i = 0; i < arrayInt.Length; i++)
        {
            GameCore.Instance.m_TileList[arrayInt[i]].transform.DOScale(new Vector3(0.7f, 0.7f, 0.1f), 1f).SetLoops(-1, LoopType.Yoyo);
        }
        yield return null;
    }


    private List<int> GetRandomNumList(int ListLen)
    {

        List<int> resultList = new List<int>();
        Dictionary<int, bool> intDic = new Dictionary<int, bool>();

        for (int i = 0; i < ListLen; i++)
        {
            int RandomNum = Random.Range(GameCore.Instance.m_NumOrder - 1, GameCore.Instance.hasOnGame);

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
