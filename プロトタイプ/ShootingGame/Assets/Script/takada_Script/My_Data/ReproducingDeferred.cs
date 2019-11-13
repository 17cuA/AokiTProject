using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproducingDeferred : MonoBehaviour
{
	//変化にかかる時間
	public float deletionTime = 0.0f;

	//経過時間
	public float elapsedTime = 0.0f;
	public ParticleSystem particleSystem;
	public bool onceFlag = false;

	void Start()
	{
		particleSystem = this.GetComponent<ParticleSystem>();
		particleSystem.Stop();
		onceFlag = false;
	}

	void Update()
	{
		if (deletionTime >= elapsedTime)
		{
			elapsedTime += Time.deltaTime;  //経過時間
		}
		else
		{
			if (!onceFlag)
			{
				particleSystem.Play();
				onceFlag = true;
			}
		}
	}
}
