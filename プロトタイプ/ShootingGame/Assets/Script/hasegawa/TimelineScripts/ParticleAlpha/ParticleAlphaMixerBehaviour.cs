using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ParticleAlphaMixerBehaviour : PlayableBehaviour
{
	ParticleSystem trackBindingParticleSystem;
	float initialAlphaValue;

	public override void OnGraphStop(Playable playable)
	{
		if (trackBindingParticleSystem == null) { return; }
		trackBindingParticleSystem.startColor = new Color(trackBindingParticleSystem.startColor.r, trackBindingParticleSystem.startColor.g, trackBindingParticleSystem.startColor.b, initialAlphaValue);
		trackBindingParticleSystem = null;
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		if (trackBindingParticleSystem == null)
		{
			trackBindingParticleSystem = playerData as ParticleSystem;
			initialAlphaValue = trackBindingParticleSystem.startColor.a;
		}
		int inputCount = playable.GetInputCount();
		float alpha = 0f;
		for (int i = 0; i < inputCount; ++i)
		{
			float inputWeight = playable.GetInputWeight(i);
			ScriptPlayable<ParticleAlphaBehaviour> inputPlayable = (ScriptPlayable<ParticleAlphaBehaviour>)playable.GetInput(i);
			ParticleAlphaBehaviour input = inputPlayable.GetBehaviour();
			alpha += input.Alpha * inputWeight;
		}
		trackBindingParticleSystem.startColor = new Color(trackBindingParticleSystem.startColor.r, trackBindingParticleSystem.startColor.g, trackBindingParticleSystem.startColor.b, alpha);
	}
}
