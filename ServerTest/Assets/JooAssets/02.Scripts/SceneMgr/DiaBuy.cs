using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiaBuy : MonoBehaviour
{

    public GameObject DiaPurchasScreen;
    public Text wordText;
    Dictionary<int, int> priceDia = new Dictionary<int, int>()
    {
        {1000, 10},
        {3000, 33},
        {5000, 55},
        {10000, 120},
        {15000, 180},
        {25000, 310}
    };
	
    void Start()
    {
        DiaPurchasScreen.SetActive(false);
    }
	
    public void DiaPurchasClick(int price)
    {
        int diaCount = priceDia[price];
        wordText.text = price.ToString() + "원을 사용하여\n"+diaCount+"개의 다이아몬드를\n" + "구매하시겠습니까?";
        if(DiaPurchasScreen != null)
        {
            DiaPurchasScreen.SetActive(true);
        }         
    }

    public void NoClick()
    {
        DiaPurchasScreen.SetActive(false);
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
