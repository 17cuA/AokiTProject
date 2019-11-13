using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RogoScene : MonoBehaviour
{
	int Diray;

    void Start()
    {
		Diray = 0;
    }

    void Update()
    {
		if (Scene_Manager.Manager.Is_Fade_Finished)
		{
			Diray++;
			if (Diray == 60)
			{
				Scene_Manager.Manager.Screen_Transition_To_Title();
			}
		}
	}
}
