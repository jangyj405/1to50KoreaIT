using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour {

    public GameObject CharacterPurchasScreen;

    public void CharacterPurchasClick()
    {
        if (CharacterPurchasScreen != null)
            CharacterPurchasScreen.SetActive(true);
    }

    public void NoClick()
    {
        CharacterPurchasScreen.SetActive(false);
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
}
