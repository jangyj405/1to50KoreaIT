using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {


    private BlockRoot block_root = null;

	// Use this for initialization
	void Start () {
        this.block_root = this.gameObject.GetComponent<BlockRoot>();

        this.block_root.initialSetUp();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
