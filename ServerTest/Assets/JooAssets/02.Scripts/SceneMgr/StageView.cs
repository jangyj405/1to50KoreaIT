using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageView : MonoBehaviour {

    public GameObject startScreen;

	void Start () 
    {
        SceneManager.LoadScene("AddtiveSceneETC", LoadSceneMode.Additive);
	}

    public void StageClick()
    {
        startScreen.SetActive(true);
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
