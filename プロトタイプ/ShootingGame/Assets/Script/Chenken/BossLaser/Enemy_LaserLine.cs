using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(591)]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy_LaserLine : Enemy_Bullet
{
	private bool isFixed = false;
	public bool IsFixed
	{
		get
		{
			return isFixed;
		}
		set
		{
			isFixed = value;
		}
	}
	private Vector3 fixedDirection;
	public Vector3 FixedDirection
	{
		get
		{
			return fixedDirection;
		}
		set
		{
			fixedDirection = value;
		}
	}

	private TrailRenderer trailRenderer;
	public TrailRenderer TrailRenderer
	{
		get
		{
			return trailRenderer;
		}
	}

	private int frame = 0;
	public int Frame
	{
		get
		{
			return frame;
		}
	}


	private void Awake()
	{
		base.shot_speed = 0.8f;
		base.attack_damage = 1;

		this.trailRenderer = GetComponent<TrailRenderer>();
	}

	protected new void Start()
	{
		base.Travelling_Direction = FixedDirection;
	}

	private new void Update()
	{
		if (transform.position.x >= 30.0f || transform.position.x <= -30.0f
			|| transform.position.y >= 13.5f || transform.position.y <= -13.5f)
			{
				gameObject.SetActive(false);
			}

		transform.position += fixedDirection * shot_speed;
		frame++;
	}
}
