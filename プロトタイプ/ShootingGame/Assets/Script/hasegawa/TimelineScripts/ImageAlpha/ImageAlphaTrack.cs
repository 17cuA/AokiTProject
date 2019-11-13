using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

[TrackColor(0.5f, 1f, 0.5f)]
[TrackClipType(typeof(ImageAlphaClip))]
[TrackBindingType(typeof(Image))]
public class ImageAlphaTrack : TrackAsset
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		foreach(TimelineClip tc in GetClips())
		{
			ImageAlphaClip clip = (ImageAlphaClip)tc.asset;
			tc.displayName = string.Format("{0:0.00}", clip.template.Alpha);
		}
		return ScriptPlayable<ImageAlphaMixerBehaviour>.Create(graph, inputCount);
	}
}
