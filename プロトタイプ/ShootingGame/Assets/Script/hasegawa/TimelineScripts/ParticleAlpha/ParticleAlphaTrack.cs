using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

[TrackColor(0.5f, 1f, 0.5f)]
[TrackClipType(typeof(ParticleAlphaClip))]
[TrackBindingType(typeof(ParticleSystem))]
public class ParticleAlphaTrack : TrackAsset
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		foreach(TimelineClip tc in GetClips())
		{
			ParticleAlphaClip clip = (ParticleAlphaClip)tc.asset;
			tc.displayName = string.Format("{0:0.00}", clip.template.Alpha);
		}
		return ScriptPlayable<ParticleAlphaMixerBehaviour>.Create(graph, inputCount);
	}
}
