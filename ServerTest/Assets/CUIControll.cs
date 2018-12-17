using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CUIControll : MonoBehaviour {

	
    public void BtnStageModeBackButton()
    {
        SceneManager.LoadScene(SceneNames.stageScene);
    }
    public void BtnTimeModeBackButton()
    {
        SceneManager.LoadScene(SceneNames.timeAttackScene);
    }

    
}
