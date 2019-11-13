/*
 * 20190628 作成
 */
/* 設定した任意のフレームまでスキップする動作の実装 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SetTimeBehaviour : PlayableBehaviour
{
	public PlayableDirector Director { get; set; }
	private int skipFrame = 0;
	public int SkipFrame { set { skipFrame = value; } }	// 任意のフレーム

	// PlayStateがPlayになったときに呼び出される(おそらくクリップに入ったとき)
	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		// ディレクターがなければスキップしない
		if (!Director) { return; }
		// 設定した任意のフレーム数までスキップする
		double skipTime = skipFrame / 60.0;
		Director.time = skipTime;
	}
}
