using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;


public abstract class CJooServerTask : MonoBehaviour
{
	public void ActivateServerTask()
	{
		Initial();
	}

	protected virtual void Initial()
	{

	}

	protected virtual void Update()
	{

	}

	protected BackendReturnObject bro = new BackendReturnObject();

}
