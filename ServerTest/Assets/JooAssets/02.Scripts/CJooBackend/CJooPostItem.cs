using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class CJooPostItem : MonoBehaviour
{
	public Text txtContent = null;
	public abstract void GetItems();
	public abstract string Content { get; protected set; }
	public abstract string InDate { get; protected set; }

	protected string content;
	protected string inDate;

	protected abstract void DeleteThis();
}
