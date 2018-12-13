using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CJooOneStageButton : MonoBehaviour
{
	private int stage;

	[SerializeField]
	private Text stageText = null;

	[SerializeField]
	private Text recordText = null;
	public string RecordText
	{
		get
		{
			return recordText.text;
		}
		set
		{
			recordText.text = value;
		}
	}


	[SerializeField]
	private Button stageButton = null;

	public int Stage
	{
		get
		{
			return stage;
		}
		set
		{
			stage = value;
			stageText.text = string.Format("Stage{0}", stage.ToString());
			//todo record Text Setting

		}
	}

	public void OnClickBtnSelectStage()
	{
		StageView.stageView.StageClick();
		StageView.stageView.SelectedStage = Stage;
	}
}
