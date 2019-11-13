/*
 * 20190628 作成
 */
/* 設定した任意のフレームまでスキップ行うクリップの作成 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[System.Serializable]
public class SetTimeClip : PlayableAsset, ITimelineClipAsset
{
	public ClipCaps clipCaps
	{
		get { return ClipCaps.None; }
	}
	[SerializeField] private int skipFrame;	// スキップする任意のフレーム

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		// 情報の取得
		ScriptPlayable<SetTimeBehaviour> playable = ScriptPlayable<SetTimeBehaviour>.Create(graph);
		SetTimeBehaviour behaviour = playable.GetBehaviour();
		// 情報の設定
		behaviour.Director = owner.GetComponent<PlayableDirector>();
		behaviour.SkipFrame = skipFrame;
		return playable;
	}
}
