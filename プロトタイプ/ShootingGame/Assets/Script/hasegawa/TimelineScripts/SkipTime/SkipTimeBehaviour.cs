/*
 * 20190628 作成
 */
/* 入力を受けて設定された任意の時間分をスキップする動作の実装 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipTimeBehaviour : PlayableBehaviour
{
	public PlayableDirector Director { get; set; }
	public double SkipTime { get; set; }	// 任意の時間

	// クリップを通っているときに処理される
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		// 左右の入力を受けたらそれぞれスキップする
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Director.time += SkipTime;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Director.time -= SkipTime;
		}
	}
}
