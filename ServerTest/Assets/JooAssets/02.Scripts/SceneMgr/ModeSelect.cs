using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageModeKind
{
    StageMode, TimeAttackMode
};

public static class SceneNames
{
    public const string stageScene = "StageScene";
    public const string timeAttackScene = "TimeAttackScene";
}

public class ModeSelect : MonoBehaviour
{
    public static StageModeKind stageModeKind;

    public void StageModeClick()
    {
        stageModeKind = StageModeKind.StageMode;
        SceneManager.LoadScene(SceneNames.stageScene);
    }

    public void TimeAttackModeClick()
    {
        stageModeKind = StageModeKind.TimeAttackMode;
        SceneManager.LoadScene(SceneNames.timeAttackScene);
    }    
}

