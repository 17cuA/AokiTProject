using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void StateCallBack();
public enum StateType {WAIT, DEBUT, MOVE, STOP, ADVANCE_AND_BACK, ESCAPE, DEATH}
public class StateBase<T> where T : struct
{
	public StateCallBack EnterCallBack    { get; set;}
	public StateCallBack UpdateCallBack { get; set;}
	public StateCallBack ExitCallBack      { get; set;}

	public T StateType{ get; private set; }

	public float Duration{ get; private set; }

	private float timer = .0f;
	public float Timer
	{
		get
		{
			return timer;
		}
	}

	public bool IsDone
	{
		get
		{
			return timer <= 0;
		}
	}

	public StateBase(float duration, T type)
	{
		Duration = duration;
		StateType = type;
	}

	public void OnEnter()
	{
		EnterCallBack?.Invoke();
		timer = Duration;
	}

	public void OnUpdate()
	{
		UpdateCallBack?.Invoke();
		timer -= Time.deltaTime;
	}

	public void OnExit()
	{
		ExitCallBack?.Invoke();
		timer = Duration;
	}
}

