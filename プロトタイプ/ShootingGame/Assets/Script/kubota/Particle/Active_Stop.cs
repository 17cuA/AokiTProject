using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Stop : MonoBehaviour
{
	ParticleSystem particle;    //particleの情報を取得
	int frame;                //生成されてからの時間をカウント
	public int frame_Max;
	private void Start()
	{
		particle = GetComponent<ParticleSystem>();
		frame = 0;
	}

	// Update is called once per frame
	void Update()
    {
		if (particle.isPlaying)
		{
			frame++;
			if (frame > frame_Max)
			{
				particle.Stop();
				frame = 0;
			}
		}

	}
}
