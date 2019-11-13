using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper_LoadingText : MonoBehaviour
{
	private Text text;
	[SerializeField] private int changeTime;
	private int changeTimer;

	private string changeText;

	private void Awake()
	{
		text = GetComponent<Text>();
		changeText = "";
	}
	// Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
		changeTimer++;

		if (changeTimer >= changeTime)
		{
			changeText += ".";
			changeTimer = 0;
			if (changeText == ".......")
				changeText = "";
		}

		var addText = "LOADING" + changeText;
		text.text = addText;
	}
}
