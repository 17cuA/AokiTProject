using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSmoke : character_status
{
	public float time;

	void Start()
    {
		time = 0.0f;

	}



    void Update()
    {
		if(time < 0.5f)
		{
			time += Time.deltaTime;

			if (time > 0.5f)
			{
				GetComponent<SphereCollider>().enabled = true;
			}
		}

	}
}
