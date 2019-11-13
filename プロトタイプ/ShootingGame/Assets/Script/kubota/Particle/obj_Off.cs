//自動でparticleをオフにする機能

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Off : MonoBehaviour
{
	[SerializeField, Header("移動速度")] private float speed;

	ParticleSystem particle;    //particleの情報を取得
	int frame;                //生成されてからの時間をカウント
	public int frame_Max;
	private void Start()
	{
		particle = GetComponent<ParticleSystem>();
		frame = 0;
	}
	void Update()
    {
		Vector3 temp = transform.position;
		temp.x -= speed;
		transform.position = temp;

		frame++;
		if(frame > frame_Max)
		{
			frame = 0;
			particle.Stop();
			gameObject.SetActive(false);
		}
    }
}
