using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeAttackModeView : MonoBehaviour {
    	
	void Start () {
        SceneManager.LoadScene("AddtiveSceneETC", LoadSceneMode.Additive);
	}
    public void btnOnClick()
    {
#if UNITY_EDITOR
		FadeInOut.instance.FadeIn(SceneNames.timeAtkModeScene);
		return;
#endif
		CJooHeart.jooHeart.OnClickBtnUseHeart(() => { FadeInOut.instance.FadeIn(SceneNames.timeAtkModeScene); });
	}

    public void ButtonBack()
    {
        FadeInOut.instance.FadeIn(SceneNames.modeSelectScene);
    }
}
