using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    protected Text _text = null;
	//private void Start () 
    //{
    //    string hashkey = Backend.Utils.GetGoogleHash();
    //    Debug.Log(hashkey);
	//}
    //
    //
    //private void Update() 
    //{
    //   string hashkey = Backend.Utils.GetGoogleHash();
    //      Debug.Log(hashkey);
	//}
    //

    void Start()
    {
        // 서버가 초기화되지 않은 경우
        if (!Backend.IsInitialized)
        {
            // 초기화
            Backend.Initialize(backendCallback);
        }
        else
        {
            backendCallback();
        }
    }

    // 초기화 함수 이후에 실행되는 callback 
    void backendCallback()
    {
        // 초기화 성공한 경우 실행
        if (Backend.IsInitialized)
        {
            // example 
            // 버전체크 -> 업데이트 
            Display();

        }
        // 초기화 실패한 경우 실행 
        else
        {

        }
    }



    void Display()
    {
        _text.text = Backend.Utils.GetGoogleHash();
    }
}
