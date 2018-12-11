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
        
		//Debug.Log (EventSystem.current.currentSelectedGameObject.name);
		if ((int.Parse(EventSystem.current.currentSelectedGameObject.name.Substring (12)) > CRyuGameDataMgr.GetInst ().GetMapStageLevel)) 
        {
			Debug.Log ("Lock");

		} 
        else
        {
            startScreen.SetActive(true);
            //todo : Joo
            if ((int.Parse(EventSystem.current.currentSelectedGameObject.name.Substring(12)) == CRyuGameDataMgr.GetInst().GetMapStageLevel))
            {

                //csMapMgr.GetInstance ().MapSetting (CRyuGameDataMgr.GetInst ().GetMapStageLevel);

                CRyuGameDataMgr.GetInst().CurrentStageLevel = CRyuGameDataMgr.GetInst().GetMapStageLevel;

            }

            else

            {

                //csMapMgr.GetInstance ().MapSetting ((int.Parse (EventSystem.current.currentSelectedGameObject.name.Substring (12))));

                //Debug.Log ((int.Parse (EventSystem.current.currentSelectedGameObject.name.Substring (12))));

                CRyuGameDataMgr.GetInst().CurrentStageLevel = (int.Parse(EventSystem.current.currentSelectedGameObject.name.Substring(12)));

            }
            
        }
		Debug.Log (CRyuGameDataMgr.GetInst().GetMapStageLevel);
    }

    public void StageCloseClick()
    {
        startScreen.SetActive(false);
    }

    public void GameStartClick()
    {
        SceneManager.LoadScene("StageModeScene");
    }
		
}
