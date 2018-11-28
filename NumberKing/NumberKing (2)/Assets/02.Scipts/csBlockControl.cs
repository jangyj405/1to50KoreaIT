using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class csBlockControl : MonoBehaviour 
{

    public Transform Cube= null;
    public Transform Cube2 = null;
    public Transform Cube3 = null;
    public GameObject Cube4 = null;
    public Transform Cube5 = null;
    public List<Transform> Cube6 = new List<Transform>();


    public Transform[] edgeCube;


    bool _Scale = true;

    void Start () 
    {
        BlockRotation();
        BlockBlink();
        Invoke("BlockReverse",0.1f);
        BlockChangeScale();
        BlockRamdum();
        StartCoroutine(CoEdgeBlockMove());

	}


    IEnumerator CoEdgeBlockMove()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);
            EdgeBlockMove();
        }
    }

    void Update()
    {
        BlockSizeUpDown();
    }


    public void EdgeBlockMove()
    {
        //테투리 회전
        for(int i = 0; i< edgeCube.Length;i++)
        {
            Vector3 Target = Vector3.zero;
            Target = edgeCube[(i+1)%edgeCube.Length].position;
            edgeCube[i].DOMove(Target, 1f);
        }
    }

    public void BlockRamdum() 
    { 
        // 랜덤 생성
        int RandomNumber = Random.Range(0, 25);
        Cube6[RandomNumber].transform.DORotate(new Vector3(0f, 0f, 360f), 2.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }


    public void BlockRotation()
    {
        //회전
        Cube.transform.DORotate(new Vector3(0f, 0f, 360f), 2.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    public void BlockBlink()
    {
        //깜박임
        Color _color = Cube4.GetComponent<MeshRenderer>().material.color;
        Color alphaColor = new Color(_color.r, _color.g, _color.b, 0f);
        Cube4.GetComponent<MeshRenderer>().material.DOColor(alphaColor, 2f).SetLoops(-1, LoopType.Yoyo);
    }

    public void BlockReverse()
    {
        // 뒤집
        Cube5.transform.DORotate(new Vector3(0f, -180f, 0f), 3f);
    }

    public void BlockChangeScale()
    {
        // 크기 변환 원래크기 <-> 커진다
        Cube2.transform.DOScale(new Vector3(0.7f, 0.7f, 0.1f), 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void BlockSizeUpDown()
    {
        //크기변환 커졌다 <-> 작아졌다
        Size();
    }
    private void Size()
    {
        if (_Scale == true)
        {
            Cube3.transform.DOScale(new Vector3(0.7f, 0.7f, 0.1f), 2f);
            Invoke("SizeUp", 2f);
        }
        else if (_Scale == false)
        {
            Cube3.transform.DOScale(new Vector3(0.3f, 0.3f, 0.1f), 2f);
            Invoke("SizeDown", 2f);
        }
    }

    private void SizeUp()
    {
        _Scale = false;
    }
    public void SizeDown()
    {
        _Scale = true;
    }


}
