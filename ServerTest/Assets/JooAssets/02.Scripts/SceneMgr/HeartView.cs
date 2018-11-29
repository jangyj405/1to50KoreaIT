using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HeartView : MonoBehaviour {
    
    public GameObject HeartPurchasScreen;

    void Start()
    {
        HeartPurchasScreen.SetActive(false);
    }
    
    public void HeartPurchasClick()
    {
        if (HeartPurchasScreen != null)
            HeartPurchasScreen.SetActive(true);
    }

    public void NoClick()
    {
        HeartPurchasScreen.SetActive(false);
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


