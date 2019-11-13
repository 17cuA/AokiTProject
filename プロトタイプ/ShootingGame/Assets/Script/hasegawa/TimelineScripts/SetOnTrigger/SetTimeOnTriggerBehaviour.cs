/*
 * 20190827 作成
 * author hasegawa yuuta
 */
/* トリガーがオンになったときに設定したフレーム数に飛ばす動作の実装 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class SetTimeOnTriggerBehaviour : PlayableBehaviour
{
	public PlayableDirector Director { get; set; }
	public SetTimeTrigger SetTimeTrigger { get; set; }
	public WaitLoopTrigger WaitLoopTrigger { get; set; }	// ループを抜けてしまうかもしれないため
	public int SetFrame { get; set; } = 0;					// とばした先のフレーム
	public int DelayFrame { get; set; } = 0;				// 遅延させるフレーム数
	int frame = 0;											// 現在のフレーム数

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		// ディレクターがなければスキップしない
		if (!Director) { return; }
		// トリガーがオンでなければスキップしない
		if (!SetTimeTrigger.Trigger) { return; }
		// 遅延
		if (frame < DelayFrame) { ++frame; return; }
		// ループを切る
		WaitLoopTrigger.Trigger = true;
		// 設定した任意のフレームまでスキップする
		double time = SetFrame / 60.0;
		Director.time = time;
		// スキップしたのでトリガーをオンにする
		SetTimeTrigger.Trigger = true;
	}
}
