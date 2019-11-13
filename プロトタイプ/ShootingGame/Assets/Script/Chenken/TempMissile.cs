using System;
using System.Collections.Generic;
using UnityEngine;

public class TempMissile : MonoBehaviour
{
	public LayerMask groundLayer;
	public float checkDistance;
	private Vector3 normal;
	private Vector3 alongGround;
	public bool isGround;
	public float speed;
	public Vector3 initDirectioon;
	public bool init = true;
	public void FixedUpdate()
	{
		if (init)
			transform.position += initDirectioon * speed * Time.deltaTime;
		else
		{
			if (isGround)
				transform.position += alongGround * speed * Time.deltaTime;
			else
				transform.position += Vector3.down * speed * Time.deltaTime;
		}

		GroundCheck();

		alongGround = new Vector2(normal.y, -normal.x);

		if (alongGround.x >= 0 && alongGround.y > 0)
			Debug.Log("消滅");
	}

	private void GroundCheck()
	{
		RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, checkDistance, groundLayer);

		if (hits.Length > 0)
		{
			isGround = true;
			init = false;
		}
		else
			isGround = false;

		for(var i = 0; i < hits.Length; ++i)
		{
			normal = hits[i].normal;
		}
	}
}

