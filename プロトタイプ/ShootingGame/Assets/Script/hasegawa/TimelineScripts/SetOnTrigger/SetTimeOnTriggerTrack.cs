/*
 * 20190827 作成
 * author hasegawa yuuta
 */
/* トリガーがオンになったときに設定したフレーム数に飛ばすクリップを配置するトラック */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.35f, 0.8f, 0.5f)]
[TrackClipType(typeof(SetTimeOnTriggerClip))]
public class SetTimeOnTriggerTrack : TrackAsset
{
}
