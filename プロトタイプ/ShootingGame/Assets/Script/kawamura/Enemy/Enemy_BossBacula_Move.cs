using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossBacula_Move : MonoBehaviour
{
	Vector3 velocity;
	public float speedY;

    void Start()
    {
        
    }

    void Update()
    {
		velocity = gameObject.transform.rotation * new Vector3(0, speedY, 0);
		gameObject.transform.position += velocity * Time.deltaTime;

		if (transform.position.y < 0)
		{
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		}
	}
}
