using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caution_Manager : MonoBehaviour
{
	[SerializeField, Header("表示フレーム")] private int display_frame;
	private int Frame { get; set; }

	private void Start()
	{
		Frame = 0;
	}

	void Update()
    {
        if(Scene_Manager.Manager.Is_Fade_Finished)
		{
			Frame++;
			if(Frame > display_frame)
			{
				Scene_Manager.Manager.Screen_Transition_To_ROGO();
			}
		}
    }
}
