using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class ImageAlphaClip : PlayableAsset, ITimelineClipAsset
{
	public ClipCaps clipCaps
	{
		get { return ClipCaps.Blending; }
	}
	[SerializeField] public ImageAlphaBehaviour template;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<ImageAlphaBehaviour>.Create(graph, template);
	}
}
