using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper_PauseAnimation : MonoBehaviour
{
	[SerializeField] [Range(0, 255)] public float startAlpha = 178f;
	[SerializeField] [Range(0, 255)] public float transitionAlpha = 0f;
	[SerializeField] public float transitionTime = 3f;
	private float transitionTimer;
	private float currentAlpha;
	private float targetAlpha;
	private bool isBlightToDark;

	private Image pauseImage;

	private void Awake()
	{
		pauseImage = GetComponent<Image>();
		currentAlpha = startAlpha;
		targetAlpha = transitionAlpha;
		isBlightToDark = (startAlpha > transitionAlpha) ? true : false;
	}

	private void Update()
	{
		//DeltaTimeが使えない
		transitionTimer += 0.0166667f;
		if (transitionTimer >= transitionTime)
		{
			isBlightToDark = !isBlightToDark;
			transitionTimer = 0;
			return;

		}
		
		//暗→明
		if(isBlightToDark)
		{
			if(currentAlpha != startAlpha) currentAlpha = startAlpha;
			if(targetAlpha != transitionAlpha) targetAlpha = transitionAlpha;
		}
		else
		{
			if (currentAlpha != transitionAlpha) currentAlpha = transitionAlpha;
			if (targetAlpha != startAlpha) targetAlpha = startAlpha;
		}

		//円滑に
		var alpha = Mathf.SmoothStep(currentAlpha, targetAlpha, transitionTimer / transitionTime);
		pauseImage.color = new Color(1, 1, 1, alpha / 255f);
	}
}
