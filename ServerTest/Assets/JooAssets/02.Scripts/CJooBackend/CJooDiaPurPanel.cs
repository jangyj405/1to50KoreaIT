using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CJooDiaPurPanel : MonoBehaviour
{
	[SerializeField]
	private Text txtMessage = null;
	
	public string Message
	{
		get
		{
			return txtMessage.text;
		}
		set
		{
			txtMessage.text = value;
		}
	}
}
