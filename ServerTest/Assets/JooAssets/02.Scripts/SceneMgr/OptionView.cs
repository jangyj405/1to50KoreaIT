using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionView : MonoBehaviour {

    public GameObject LogOutPanel;
    public GameObject WithdrawPanel;

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

    public void NickNameChangeClick()
    {
        SceneManager.LoadScene("NickNameChange");
    }

    public void LogOutClick()
    {
        LogOutPanel.SetActive(true);
    }

    public void WithdrawClick()
    {
        WithdrawPanel.SetActive(true);
    }

    public void LogOutWithdrawAfterConfirmClick()
    {
        SceneManager.LoadScene(SceneNames.AccountScene);
    }


}
