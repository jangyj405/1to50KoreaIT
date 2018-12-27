using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CUIControll : MonoBehaviour {

	
    public void BtnStageModeBackButton()
    {
        SoundManager.instance.BGMMainMenu();
        FadeInOut.instance.FadeIn(SceneNames.stageScene);
    }
    public void BtnTimeModeBackButton()
    {
        SoundManager.instance.BGMMainMenu();
        FadeInOut.instance.FadeIn(SceneNames.timeAttackScene);
    }

}
