using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CJooErrorMessage : MonoBehaviour
{
	[SerializeField]
	Text txtErrorID = null;
	[SerializeField]
	Text txtErrorExplain = null;

	public string errorID
	{
		get
		{
			return txtErrorID.text;
		}
		set
		{
			txtErrorID.text = value;
		}
	}

	public string errorExplain
	{
		get
		{
			return txtErrorExplain.text;
		}
		set
		{
			txtErrorExplain.text = value;
		}
	}

}
