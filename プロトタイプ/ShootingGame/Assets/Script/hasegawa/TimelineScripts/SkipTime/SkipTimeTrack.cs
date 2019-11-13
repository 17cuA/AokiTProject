/*
 * 20190628 作成
 */
/* 入力を受けて設定された任意の時間分をスキップするクリップを配置するためのトラック */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[TrackColor(0.5f, 1f, 1f)]
[TrackClipType(typeof(SkipTimeClip))]
public class SkipTimeTrack : PlayableTrack
{
}
