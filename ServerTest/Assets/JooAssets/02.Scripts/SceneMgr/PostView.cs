using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostView : MonoBehaviour {

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
}
