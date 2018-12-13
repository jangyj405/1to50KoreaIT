using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJooTempItemContainer
{
	private static CJooTempItemContainer m_instance = null;
	public static CJooTempItemContainer Instance
	{
		get
		{
			if(m_instance == null)
			{
				m_instance = new CJooTempItemContainer();
			}
			return m_instance;
		}
	}

	private CJooTempItemContainer()
	{
		dicItemContainer.Clear();
	}

	Dictionary<string, int> dicItemContainer = new Dictionary<string, int>();
	public Dictionary<string,int> DicItemContainer
	{
		get
		{
			return dicItemContainer;
		}
	}
	public void ClearContainer()
	{
		dicItemContainer.Clear();
	}

	public void AddToContainer(string pKey, int pValue)
	{
		if(dicItemContainer.ContainsKey(pKey))
		{
			dicItemContainer[pKey] += pValue;
		}
		else
		{
			dicItemContainer.Add(pKey, pValue);
		}
	}

	public void AddToContainer(string[] pKeys, int[] pValues)
	{
		if(pKeys.Length != pValues.Length)
		{
			return;
		}
		for (int i = 0; i < pKeys.Length; i++)
		{
			AddToContainer(pKeys[i], pValues[i]);
		}
	}

	public bool GetValue(string pKey,out int oItemCount)
	{
		if(dicItemContainer.ContainsKey(pKey))
		{
			oItemCount = dicItemContainer[pKey];
			return true;
		}
		else
		{
			oItemCount = -1;
			return false;
		}
	}
}
