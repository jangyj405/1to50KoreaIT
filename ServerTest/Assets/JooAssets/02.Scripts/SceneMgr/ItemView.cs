using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemView : MonoBehaviour {

    public GameObject ItemPurchasScreen;

	void Start () {
        ItemPurchasScreen.SetActive(false);
	}

    public void ItemPurchasClick()
    {
        if (ItemPurchasScreen != null)
            ItemPurchasScreen.SetActive(true);
    }

    public void NoClick()
    {
        ItemPurchasScreen.SetActive(false);
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
