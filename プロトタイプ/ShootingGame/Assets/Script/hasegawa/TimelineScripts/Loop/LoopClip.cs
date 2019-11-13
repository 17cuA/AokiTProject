/*
 * 20190627 作成
 */
/* ループを行うクリップの作成 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[System.Serializable]
public class LoopClip : PlayableAsset, ITimelineClipAsset
{
	public ClipCaps clipCaps
	{
		get { return ClipCaps.None; }
	}

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		// 情報の取得
		ScriptPlayable<LoopBehaviour> playable = ScriptPlayable<LoopBehaviour>.Create(graph);
		LoopBehaviour behaviour = playable.GetBehaviour();
		// 情報の設定
		behaviour.Director = owner.GetComponent<PlayableDirector>();
		behaviour.WaitLoopTrigger = FindObjectOfType<WaitLoopTrigger>();
		return playable;
	}
}
