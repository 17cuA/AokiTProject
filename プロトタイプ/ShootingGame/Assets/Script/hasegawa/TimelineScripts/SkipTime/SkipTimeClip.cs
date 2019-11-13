/*
 * 20190628 作成
 */
/* 入力を受けて設定された任意の時間分をスキップするクリップの作成 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[System.Serializable]
public class SkipTimeClip : PlayableAsset, ITimelineClipAsset
{
	public ClipCaps clipCaps
	{
		get { return ClipCaps.None; }
	}
	[SerializeField] private double skipTime;	// 任意の時間

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		// 情報の取得
		ScriptPlayable<SkipTimeBehaviour> playable = ScriptPlayable<SkipTimeBehaviour>.Create(graph);
		SkipTimeBehaviour behaviour = playable.GetBehaviour();
		// 情報の設定
		behaviour.Director = owner.GetComponent<PlayableDirector>();
		behaviour.SkipTime = skipTime;
		return playable;
	}
}
