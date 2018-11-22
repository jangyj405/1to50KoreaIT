using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using System;
public class JooCustomLogin : MonoBehaviour 
{
    public InputField IDinput;
    public InputField PWinput;
    public InputField NNinput;
    //비동기로 가입, 로그인을 할때에는 Update()에서 처리를 해야합니다. 이 값은 Update에서 구현하기 위한 플래그 값 입니다.
    BackendReturnObject bro = new BackendReturnObject();
    bool isSuccess = false;


    public void ACustomLogin()
    {
        Debug.Log("-------------ACustomLogin-------------");
        Backend.BMember.CustomLogin("asdf", "asdf", isComplete =>
        {
            // 성공시 - Update() 문에서 토큰 저장
            Debug.Log(isComplete.ToString());
            isSuccess = isComplete.IsSuccess();
            bro = isComplete;
        });
    }
   
    void Start()
    {
        if (!Backend.IsInitialized)
        {
            Backend.Initialize(backendCallback);
        }
        else
        {
            backendCallback();
        }
        Invoke("WithToken", 1f);
    }

    // 초기화 함수 이후에 실행되는 callback 
    void backendCallback()
    {
        // 초기화 성공한 경우 실행
        if (Backend.IsInitialized)
        {
            // example 
            // 구글 해시키 획득 
            if (!Backend.Utils.GetGoogleHash().Equals(""))
                Debug.Log(Backend.Utils.GetGoogleHash());

            // 서버시간 획득
            Debug.Log(Backend.Utils.GetServerTime());

        }
        // 초기화 실패한 경우 실행 
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSuccess)
        {
            Backend.BMember.SaveToken(bro);
            isSuccess = false;
            bro.Clear();
        }
    }

    public void OnClickBtnDebugInputFields()
    {
        Debug.Log(IDinput.text);
        Debug.Log(PWinput.text);
        Debug.Log(NNinput.text);
    }

    public void ACustomSignUp()
    {
        Debug.Log("-------------ACustomSignUp-------------");
        OnClickBtnDebugInputFields();
        Backend.BMember.CustomSignUp(IDinput.text, PWinput.text, NNinput.text, isComplete =>
        {
            // 성공시 - Update() 문에서 토큰 저장
            Debug.Log(isComplete.ToString());
            isSuccess = isComplete.IsSuccess();
            bro = isComplete;
        });
    }
    void WithToken()
    {
        Backend.BMember.LoginWithTheBackendToken((callback) =>
        {
            Debug.Log(callback.ToString());
            isSuccess = true;
            bro = callback;
           
        });
    }

}
