using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(SceneNames.stageScene);
        }
        else if (ModeSelect.stageModeKind == StageModeKind.TimeAttackMode)
        {
            SceneManager.LoadScene(SceneNames.timeAttackScene);
        }
    }
}
