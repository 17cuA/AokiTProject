using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_XOnly : MonoBehaviour
{
	public float speedX;
	Vector3 velocity;

	void Start()
	{

	}

	void Update()
	{
		velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
		gameObject.transform.position += velocity * Time.deltaTime;

	}
}
