/*
 * 20190614 作成
 */
 /* 使用可能なディスプレイをアクティベートする */
 /* ディスプレイの分、TargetDisplayを設定したカメラを作る */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSetUp
{
	public class DualCameraSetUp
	{
		public DualCameraSetUp()
		{
			// 使用可能なディスプレイを全てアクティベートする
			for (int i = 1; i < Display.displays.Length; ++i)
			{
				Display.displays[i].Activate();
			}
		}
	}
}