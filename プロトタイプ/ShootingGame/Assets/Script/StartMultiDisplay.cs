//実行データの初期設定
//
using UnityEngine;
using System.Collections;

public class StartMultiDisplay : MonoBehaviour
{

    void Awake()
    {
        int maxDisplayCount = 2;
        for (int i = 0; i < maxDisplayCount && i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
			
        }


		//QualitySettings.SetQualityLevel((int)QualityLevel.Good);
		//Screen.SetResolution(3840, 1080, Screen.fullScreen);
	}
}