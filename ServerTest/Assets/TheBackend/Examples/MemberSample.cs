using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using UnityEngine.SceneManagement;
using System;

public class BMemberTest : MonoBehaviour
{

    //비동기로 가입, 로그인을 할때에는 Update()에서 처리를 해야합니다. 이 값은 Update에서 구현하기 위한 플래그 값 입니다.
    BackendReturnObject bro = new BackendReturnObject();
    bool isSuccess = false;

    // 테스트 아이디 비밀번호
    string id = "customid";
    string pw = "thebackend";

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

    // 커스텀 가입
    public void CustomSignUp()
    {
        Debug.Log("-------------CustomSignUp-------------");
        Debug.Log(Backend.BMember.CustomSignUp(id, pw, "tester").ToString());
    }

    public void ACustomSignUp()
    {
        Debug.Log("-------------ACustomSignUp-------------");
        Backend.BMember.CustomSignUp(id, pw, "tester", isComplete =>
        {
            // 성공시 - Update() 문에서 토큰 저장
            Debug.Log(isComplete.ToString());
            isSuccess = isComplete.IsSuccess();
            bro = isComplete;
        });
    }

    // 커스텀 로그인
    public void CustomLogin()
    {
        Debug.Log("-------------CustomLogin-------------");
        BackendReturnObject isComplete = Backend.BMember.CustomLogin(id, pw);
        Debug.Log(isComplete);
    }

    public void ACustomLogin()
    {
        Debug.Log("-------------ACustomLogin-------------");
        Backend.BMember.CustomLogin(id, pw, isComplete =>
        {
            // 성공시 - Update() 문에서 토큰 저장
            Debug.Log(isComplete.ToString());
            isSuccess = isComplete.IsSuccess();
            bro = isComplete;
        });
    }

    // 기기에 저장된 뒤끝 AccessToken으로 로그인 (페더레이션, 커스텀 회원가입 또는 로그인 이후에 시도 가능)
    public void LoginWithTheBackendToken()
    {
        Debug.Log("-------------LoginWithTheBackendToken-------------");
        Debug.Log(Backend.BMember.LoginWithTheBackendToken().ToString());

    }

    public void ALoginWithTheBackendToken()
    {
        Debug.Log("-------------ALoginWithTheBackendToken-------------");

        Backend.BMember.LoginWithTheBackendToken(isComplete =>
        {
            // 성공시 - Update() 문에서 토큰 저장
            Debug.Log(isComplete.ToString());
            isSuccess = isComplete.IsSuccess();
            bro = isComplete;
        });
    }


     //뒤끝 RefreshToken 을 통해 뒤끝 AccessToken 을 재발급 받습니다
    public void RefreshTheBackendToken()
    {
        Debug.Log("-------------RefreshTheBackendToken-------------");
        Debug.Log(Backend.BMember.RefreshTheBackendToken().ToString());
    }

    public void ARefreshTheBackendToken(){
        Debug.Log("-------------ARefreshTheBackendToken-------------");
        Backend.BMember.RefreshTheBackendToken(isComplete =>
        {
            // 성공시 - Update() 문에서 토큰 저장
            Debug.Log(isComplete.ToString());
            isSuccess = isComplete.IsSuccess();
            bro = isComplete;
        });
    }

    // 서버에서 뒤끝 access_token과 refresh_token을 삭제
    public void Logout()
    {
        Debug.Log("-------------Logout-------------");
        Debug.Log(Backend.BMember.Logout().ToString());
    }

    public void ALogout()
    {
        Debug.Log("-------------ALogout-------------");
        Backend.BMember.Logout(isComplete =>
        {
            Debug.Log(isComplete.ToString());
        });
    }

    // 회원 탈퇴 
    public void SignOut()
    {
        Debug.Log("-------------SignOut-------------");
        Debug.Log(Backend.BMember.SignOut("탈퇴 사유").ToString());
    }

    public void ASignOut()
    {
        Debug.Log("-------------ASignOut-------------");
        Backend.BMember.SignOut("탈퇴 사유",isComplete =>
        {
            Debug.Log(isComplete.ToString());
        });
    }

    string nickname = "nick";
    // 닉네임 생성 
    public void CreateNickname()
    {
        Debug.Log("-------------CreateNickname-------------");
        // 중복 허용
        Debug.Log(Backend.BMember.CreateNickname(true, nickname).ToString());
    }

    public void ACreateNickname()
    {
        Debug.Log("-------------ACreateNickname-------------");
        // 중복 비허용
        Backend.BMember.CreateNickname(true, nickname, isComplete =>
        {
            Debug.Log(isComplete.ToString());
        });
    }

    // 닉네임 수정
    public void UpdateNickname()
    {
        Debug.Log("-------------UpdateNickname-------------");
        // 중복 비허용
        Debug.Log(Backend.BMember.UpdateNickname(true, nickname).ToString());
    }

    public void AUpdateNickname()
    {
        Debug.Log("-------------AUpdateNickname-------------");
        // 중복 비허용
        Backend.BMember.UpdateNickname(true, nickname, isComplete =>
        {
            Debug.Log(isComplete.ToString());
        });
    }

    // 유저 정보 받아오기 - nickname
    public void GetUserInfo()
    {
        Debug.Log("-------------GetUserInfo-------------");
        BackendReturnObject userinfo = Backend.BMember.GetUserInfo();
        Debug.Log(userinfo);
        if(userinfo.IsSuccess()){
            JsonData Userdata = userinfo.GetReturnValuetoJSON()["row"];
            JsonData nicknameJson = Userdata["nickname"];

            // 닉네임 여부를 확인 하는 로직
            if (nicknameJson != null)
            {
                string nick = nicknameJson.ToString();
                Debug.Log("NickName is NOT null which is " + nick);
            }
            else
            {
                Debug.Log("NickName is null");
            }
        }

    }

    public void AGetUserInfo()
    {
        Debug.Log("-------------AGetUserInfo-------------");
        Backend.BMember.GetUserInfo(userinfo =>
        {
            Debug.Log(userinfo.ToString());
            JsonData Userdata = userinfo.GetReturnValuetoJSON()["row"];
            JsonData nicknameJson = Userdata["nickname"];

            // 닉네임 여부를 확인 하는 로직
            if (nicknameJson != null)
            {
                string nick = nicknameJson.ToString();
                Debug.Log("NickName is NOT null which is " + nick);
            }
            else
            {
                Debug.Log("NickName is null");
            }
        });
    }

    // 푸시 토큰 입력
    public void PutDeviceToken()
    {
        Debug.Log("-------------PutDeviceToken-------------");
#if UNITY_ANDROID
        try{
            Debug.Log(Backend.Android.PutDeviceToken());
        }catch(Exception e){
            Debug.Log(e);
        }
#else
        Debug.Log(Backend.iOS.PutDeviceToken(isDevelopment.iosProd));
#endif
    }

    public void APutDeviceToken()
    {
        Debug.Log("-------------APutDeviceToken-------------");
#if UNITY_ANDROID
        Backend.Android.PutDeviceToken(bro =>
        {
            Debug.Log(bro);
        });
#else
        Backend.iOS.PutDeviceToken(isDevelopment.iosProd, bro =>
        {
            Debug.Log(bro);
        });
#endif
    }

    // 푸시 토큰 삭제
    public void DeleteDeviceToken()
    {
        Debug.Log("-------------DeleteDeviceToken-------------");
#if UNITY_ANDROID
        Debug.Log(Backend.Android.DeleteDeviceToken());
#else
        Debug.Log(Backend.iOS.DeleteDeviceToken());
#endif
    }

    public void ADeleteDeviceToken()
    {
        Debug.Log("-------------ADeleteDeviceToken-------------");
#if UNITY_ANDROID
        Backend.Android.DeleteDeviceToken(bro =>
        {
            Debug.Log(bro);
        });
#else
        Backend.iOS.DeleteDeviceToken(bro =>
        {
            Debug.Log(bro);
        });
#endif
    }
}