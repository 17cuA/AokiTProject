/*
 * 20190703 作成
 */
/* exe起動時のみに処理される */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpProgram : MonoBehaviour
{
	private static StartUpProgram instance = null;

	void Awake()
	{
		if (instance) { return; }
		Cursor.visible = false;
		//Screen.SetResolution(1920, 1080, true);
		Application.targetFrameRate = 60;
		//CameraSetUp.DualCameraSetUp cameraSetup = new CameraSetUp.DualCameraSetUp();
		instance = FindObjectOfType<StartUpProgram>();
	}
}
