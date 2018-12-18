using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CJooOneItemButton : MonoBehaviour
{
	public Text txtCount = null;
	public Toggle toggle = null;
	private int itemCount = 0;
	public int ItemCount
	{
		get
		{
			return itemCount;
		}
		set
		{
			itemCount = value;
			txtCount.text = itemCount.ToString();
			if(itemCount <= 0)
			{
				toggle.interactable = false;
			}
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDisable()
	{
		toggle.isOn = false;
	}
}
