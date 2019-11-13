using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager<T>  where T : struct
{
	Dictionary<T, StateBase<T>> states;
	private StateBase<T> currentState;
	public StateBase<T> Current
	{
		get
		{
			return currentState;
		}
	}

	public StateManager()
	{
		states = new Dictionary<T, StateBase<T>>();
	}

	public void Add(StateBase<T> stateBase)
	{
		if (stateBase == null)
		{
			Debug.LogWarning("Null 状態、　状態名　：" + stateBase.StateType.ToString());
			return;
		}

		if (states.ContainsKey(stateBase.StateType))
		{
			Debug.LogWarning("既に存在する状態、新たな状態として作成失敗、　状態名　：" + stateBase.StateType.ToString());
			return;
		}

		states.Add(stateBase.StateType, stateBase);
	}

	public void Remove(T type)
	{
		if (!states.ContainsKey(type))
		{
			Debug.LogWarning("存在しない状態、削除失敗、　状態名　：" + type.ToString());
			return;
		}

		states.Remove(type);
	}

	public void ChangeState(T type)
	{
		if (currentState == null)
		{
			Debug.LogError("現在状態は　Null　のため、　ほかの状態に切り替えることができない");
			return;
		}

		if (!states.ContainsKey(type))
		{
			Debug.LogWarning("存在しない状態、切り替え失敗、　状態名　：" + type.ToString());
			return;
		}

		currentState.OnExit();
		currentState = states[type];
		currentState.OnEnter();
	}

	public void Start(T type)
	{
		if (currentState != null)
		{
			Debug.LogError("現在状態は存在する、　状態初期化失敗");
			return;
		}

		if (!states.ContainsKey(type))
		{
			Debug.LogWarning("存在しない状態、状態初期化失敗、　状態名　：" + type.ToString());
			return;
		}

		currentState = states[type];
		currentState.OnEnter();
	}

	public void Update()
	{
		currentState?.OnUpdate();
	}
}
