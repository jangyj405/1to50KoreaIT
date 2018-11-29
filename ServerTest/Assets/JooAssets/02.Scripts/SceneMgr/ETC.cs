using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ETC : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DiaPurchasClick()
    {
        SceneManager.LoadScene("DiaPurchas");
    }

    public void HeartPurchasClick()
    {
        SceneManager.LoadScene("HeartPurchas");
    }
}
