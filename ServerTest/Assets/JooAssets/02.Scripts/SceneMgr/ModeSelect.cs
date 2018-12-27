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
    public const string diaPurchaseScene = "DiaPurchase";
    public const string heartPurchaseScene = "HeartPurchase";
    public const string optionScene = "Option";
    public const string postScene = "Post";
    public const string friendScene = "Friend";
    public const string itemPurchaseScene = "ItemPurchase";
    public const string characterPurchaseScene = "CharacterPurchase";
}

public class ModeSelect : MonoBehaviour
{
    public static StageModeKind stageModeKind;

    public void StageModeClick()
    {
        stageModeKind = StageModeKind.StageMode;
        FadeInOut.instance.FadeIn(SceneNames.stageScene);
        //SceneManager.LoadScene(SceneNames.stageScene);
    }

    public void TimeAttackModeClick()
    {
        stageModeKind = StageModeKind.TimeAttackMode;
        FadeInOut.instance.FadeIn(SceneNames.timeAttackScene);
    }    
}

