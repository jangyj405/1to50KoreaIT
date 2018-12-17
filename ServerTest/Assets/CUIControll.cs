using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CUIControll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void BtnStageModeBackButton()
    {
        SceneManager.LoadScene("StageScene");
    }
    public void BtnTimeModeBackButton()
    {
        SceneManager.LoadScene("TimeAttackScene");
    }
}
