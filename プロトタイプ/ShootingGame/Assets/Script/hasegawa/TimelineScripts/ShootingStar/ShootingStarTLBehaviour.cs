/*
 * 20190729 作成
 * author hasegawa yuuta
 */
/* 流れ星のアニメーターで再生を行う動作の実装 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class ShootingStarTLBehaviour : PlayableBehaviour
{
	public ShootingStarAnimator shootingStarAnimator { get; set; }
	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		shootingStarAnimator.AnimationRandomShootingStar();
	}
}
