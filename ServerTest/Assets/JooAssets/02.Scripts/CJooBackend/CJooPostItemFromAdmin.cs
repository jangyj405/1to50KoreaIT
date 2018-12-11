using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJooPostItemFromAdmin : CJooPostItem
{
	protected int[] itemCount;
	public int[] ItemCount
	{
		get
		{
			return itemCount;
		}
		protected set
		{
			itemCount = value;
		}
	}

	public override string Content
	{
		get
		{
			return content;
		}

		protected set
		{
			content = value;
		}
	}

	public override string InDate
	{
		get
		{
			return inDate;
		}
		protected set
		{
			inDate = value;
		}
	}

	public override void GetItems()
	{
		throw new System.NotImplementedException();
	}

	protected override void DeleteThis()
	{
		throw new System.NotImplementedException();
	}

	public void Initial(string pContent, string pInDate, int[] pItemCount)
	{
		Content = pContent;
		InDate = pInDate;
		ItemCount = pItemCount;
		txtContent.text = string.Format("{0} 보상이 도착했습니다!", Content);
	}
}
