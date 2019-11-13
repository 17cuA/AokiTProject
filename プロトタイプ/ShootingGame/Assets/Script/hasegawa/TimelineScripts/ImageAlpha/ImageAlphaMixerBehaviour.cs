using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ImageAlphaMixerBehaviour : PlayableBehaviour
{
	Image trackBindingImage;
	float initialAlphaValue;

	public override void OnGraphStop(Playable playable)
	{
		if (trackBindingImage == null) { return; }
		trackBindingImage.color = new Color(trackBindingImage.color.r, trackBindingImage.color.g, trackBindingImage.color.b, initialAlphaValue);
		trackBindingImage = null;
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		if (trackBindingImage == null)
		{
			trackBindingImage = playerData as Image;
			initialAlphaValue = trackBindingImage.color.a;
		}
		int inputCount = playable.GetInputCount();
		float alpha = 0f;
		for (int i = 0; i < inputCount; ++i)
		{
			float inputWeight = playable.GetInputWeight(i);
			ScriptPlayable<ImageAlphaBehaviour> inputPlayable = (ScriptPlayable<ImageAlphaBehaviour>)playable.GetInput(i);
			ImageAlphaBehaviour input = inputPlayable.GetBehaviour();
			alpha += input.Alpha * inputWeight;
		}
		trackBindingImage.color = new Color(trackBindingImage.color.r, trackBindingImage.color.g, trackBindingImage.color.b, alpha);
	}
}
