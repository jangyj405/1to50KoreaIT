using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CUIControll : MonoBehaviour {

	
    public void BtnStageModeBackButton()
    {
        FadeInOut.instance.FadeIn(SceneNames.stageScene);
    }
    public void BtnTimeModeBackButton()
    {
        FadeInOut.instance.FadeIn(SceneNames.timeAttackScene);
    }

    public void ReStartButtonClick()
    {
        FadeInOut.instance.FadeInReStart();        
    }
    
}
