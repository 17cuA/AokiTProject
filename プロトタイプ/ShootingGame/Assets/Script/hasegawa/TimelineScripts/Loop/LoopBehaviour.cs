/*
 * 20190627 作成
 */
/* クリップの存在するフレーム間でループさせる動作の実装 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[System.Serializable]
public class LoopBehaviour : PlayableBehaviour
{
	public PlayableDirector Director { get; set; }
	public WaitLoopTrigger WaitLoopTrigger { get; set; }

	// PlayStateがPauseになったときに呼び出される(おそらくクリップを抜けたとき)
	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		// ループをするイベントが終わっていればループを抜ける
		if (WaitLoopTrigger && WaitLoopTrigger.Trigger)
		{
			WaitLoopTrigger.Trigger = false;
			return;
		}
		// ループのイベント判定が存在しなければループを抜ける
		else if (!WaitLoopTrigger) { return; }
		if (Director && Director.time > 0.0)
		{
			// クリップの長さ分巻き戻す
			Director.time -= playable.GetDuration();
		}
	}
}
