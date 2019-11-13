/*
 * 20190628 作成
 */
/* 設定した任意のフレームまでスキップするクリップを配置するためのトラック */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[TrackColor(0f, 40f / 255f, 1f)]
[TrackClipType(typeof(SetTimeClip))]
public class SetTimeTrack : TrackAsset
{
}
