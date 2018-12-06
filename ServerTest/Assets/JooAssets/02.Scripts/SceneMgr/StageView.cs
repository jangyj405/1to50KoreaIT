using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StageView : MonoBehaviour {

    public GameObject startScreen;

	void Start () 
    {
        SceneManager.LoadScene("AddtiveSceneETC", LoadSceneMode.Additive);
	}

    public void StageClick()
    {
        startScreen.SetActive(true);
		//Debug.Log (EventSystem.current.currentSelectedGameObject.name);
		if ((int.Parse(EventSystem.current.currentSelectedGameObject.name.Substring (12)) > CRyuGameDataMgr.GetInst ().GetMapStageLevel)) {
			Debug.Log ("UnLock");
		} else {
			csMapMgr.GetInstance ().MapSetting (CRyuGameDataMgr.GetInst ().GetMapStageLevel);
		}
		Debug.Log (CRyuGameDataMgr.GetInst().GetMapStageLevel);
    }

    public void StageCloseClick()
    {
        startScreen.SetActive(false);
    }

    public void GameStartClick()
    {
        SceneManager.LoadScene("GameScene");
    }
		
}
