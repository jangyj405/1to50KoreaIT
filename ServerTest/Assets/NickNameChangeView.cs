using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NickNameChangeView : MonoBehaviour {

    public GameObject ConfrimPanel;

    public void OverlapConfirmButtonClick()
    {
        //닉네임 길이가 지나치게 길때 20바이트
        //"닉네임은 10글자 이내로 입력하세요"

        //아무것도 입력 안했을때
        //"닉네임을 입력하세요"

        //특수문자 불가능
        //"특수문자는 사용이 불가능합니다"

        //띄어쓰기 불가능
        //"띄어쓰기는 불가능합니다"

        //숫자, 영문, 한글 포함하여 10글자
        //""


        //서버통신 원할하지 않을떄
        //"서버통신이 원할하지 않습니다"
        //"잠시 후 다시 시도해 주십시오"

        //닉네임이 중복될때        
        //"이미 존재하는 닉네임입니다"

        //닉네임이 성공적일때
        //"닉네임이 성공적으로 변경되었습니다"
    }

    public void ChangeButtonClick()
    {

        ConfrimPanel.SetActive(true);
    }

    public void CloseClick()
    {
        if (ModeSelect.stageModeKind == StageModeKind.StageMode)
        {
            FadeInOut.instance.FadeIn(SceneNames.stageScene);
        }
        else if (ModeSelect.stageModeKind == StageModeKind.TimeAttackMode)
        {
            FadeInOut.instance.FadeIn(SceneNames.timeAttackScene);
        }
    }

    public void ConfirmButtonClick()
    {
        //중복확인에서 성공적일때만 누를수 있게하기

    }
	
}
