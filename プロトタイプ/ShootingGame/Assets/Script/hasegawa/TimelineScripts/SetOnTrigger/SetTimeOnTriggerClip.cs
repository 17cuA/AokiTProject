/*
 * 20190827 作成
 * author hasegawa yuuta
 */
/* トリガーがオンになったときに設定したフレーム数に飛ばすクリップの作成 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class SetTimeOnTriggerClip : PlayableAsset, ITimelineClipAsset
{
	public ClipCaps clipCaps
	{
		get { return ClipCaps.None; }
	}

	[SerializeField] int setFrame = 0;
	[SerializeField] int delayFrame = 0;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		// 生成
		ScriptPlayable<SetTimeOnTriggerBehaviour> playable = ScriptPlayable<SetTimeOnTriggerBehaviour>.Create(graph);
		SetTimeOnTriggerBehaviour behaviour = playable.GetBehaviour();
		// 情報の設定
		behaviour.Director = owner.GetComponent<PlayableDirector>();
		behaviour.SetTimeTrigger = FindObjectOfType<SetTimeTrigger>();
		behaviour.WaitLoopTrigger = FindObjectOfType<WaitLoopTrigger>();
		behaviour.SetFrame = setFrame;
		behaviour.DelayFrame = delayFrame;
		return playable;
	}
}