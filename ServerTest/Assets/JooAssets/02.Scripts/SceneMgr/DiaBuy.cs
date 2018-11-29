using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaBuy : MonoBehaviour {

    public GameObject DiaPurchasScreen;
	
	void Start () {
        DiaPurchasScreen.SetActive(false);
	}
	
    public void DiaPurchasClick()
    {
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
            SceneManager.LoadScene(SceneNames.stageScene);
        }
        else if (ModeSelect.stageModeKind == StageModeKind.TimeAttackMode)
        {
            SceneManager.LoadScene(SceneNames.timeAttackScene);
        }
    }


}
