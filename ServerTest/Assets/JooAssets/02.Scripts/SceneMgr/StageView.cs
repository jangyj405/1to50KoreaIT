﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageView : MonoBehaviour {

	void Start () 
    {
        SceneManager.LoadScene("AddtiveSceneETC", LoadSceneMode.Additive);
	}
		
}