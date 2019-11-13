/*
 * 20190827 作成
 * author hasegawa yuuta
 */
/* 時間を飛ばすときのトリガー */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeTrigger : MonoBehaviour
{
	[SerializeField] bool trigger = false;
	public bool Trigger { get { return trigger; } set { trigger = value; } }
}
