using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NickNameChangeView : MonoBehaviour {

    public GameObject ConfrimPanel;

    public void OverlapConfirmButtonClick()
    {
        //닉네임 길이가 지나치게 길때
        //닉네임이 중복될때
        //아무것도 입력 안했을때
        //닉네임이 성공적일때
        //특수문자, 띄어쓰기??
        //숫자,영어, 한글 가능, 그외언어 불가능
        
    }

    public void ChangeButtonClick()
    {

        ConfrimPanel.SetActive(true);
    }

    public void CloseClick()
    {
        if (ModeSelect.stageModeKind == StageModeKind.StageMode)
        {
            SceneManager.LoadScene(SceneNames.stageScene);
        }
        else if (ModeSelect.stageModeKind == StageModeKind.TimeAttackMode)
        {
            SceneManager.LoadScene(SceneNames.timeAttackScene);
        }
    }

    public void ConfirmButtonClick()
    {
        //중복확인에서 성공적일때만 누를수 있게하기

    }
	
}
