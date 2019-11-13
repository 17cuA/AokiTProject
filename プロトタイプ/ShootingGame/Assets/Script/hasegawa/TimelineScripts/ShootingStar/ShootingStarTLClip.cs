/*
 * 20190729 作成
 * author hasegawa yuuta
 */
/* 流れ星のアニメーターで再生を行うクリップの作成 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class ShootingStarTLClip : PlayableAsset, ITimelineClipAsset
{
	public ClipCaps clipCaps
	{
		get { return ClipCaps.None; }
	}
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		ScriptPlayable<ShootingStarTLBehaviour> playable = ScriptPlayable<ShootingStarTLBehaviour>.Create(graph);
		ShootingStarTLBehaviour behaviour = playable.GetBehaviour();
		behaviour.shootingStarAnimator = FindObjectOfType<ShootingStarAnimator>();
		return playable;
	}
}
