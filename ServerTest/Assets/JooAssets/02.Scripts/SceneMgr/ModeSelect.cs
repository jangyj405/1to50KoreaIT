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
	public const string modeSelectScene = "ModeSelect";
    public const string AccountScene = "02_AccountScene";
	public const string nicknameCreateScene = "NickNameCreate";
	public const string nicknameChangeScene = "NickNameChange";
	public const string stageModeScene = "StageModeScene";
	public const string timeAtkModeScene = "TimeModeScene";
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

