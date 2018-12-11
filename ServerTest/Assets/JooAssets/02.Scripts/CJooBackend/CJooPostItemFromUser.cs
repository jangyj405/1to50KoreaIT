using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CJooPostItemFromUser : CJooPostItem
{
	
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

	protected string senderNickname;
	public string SenderNickname
	{
		get
		{
			return senderNickname;
		}
		protected set
		{
			senderNickname = value;
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

	public void Initial(string pContent, string pInDate, string pSenderNickName)
	{
		Content = pContent;
		InDate = pInDate;
		SenderNickname = pSenderNickName;
		txtContent.text = string.Format("{0}님께서 하트를 보냈습니다!", SenderNickname);
	}
}
