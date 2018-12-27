using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HeartView : MonoBehaviour {
    
    public GameObject HeartPurchasScreen;
    public Text wordText;
    public Dictionary<int, int> heartDia = new Dictionary<int, int>()
    {
        {5, 5},
        {11, 10},
        {25, 20},
        {40, 30},
        {55, 40},
        {99, 70}
    };
        
    public void HeartPurchasClick(int heart)
    {
        int diaCount = heartDia[heart];
        wordText.text = "다이아몬드 " + diaCount + "개를\n사용하여 하트 " + heart + "개를\n구매하시겠습니까?";
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
            FadeInOut.instance.FadeIn(SceneNames.stageScene);
        }
        else if (ModeSelect.stageModeKind == StageModeKind.TimeAttackMode)
        {
            FadeInOut.instance.FadeIn(SceneNames.timeAttackScene);
        }
    }
}


