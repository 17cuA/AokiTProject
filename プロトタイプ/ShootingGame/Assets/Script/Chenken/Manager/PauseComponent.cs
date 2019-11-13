using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseComponent : MonoBehaviour
{
	private static List<PauseComponent> targets = new List<PauseComponent>();
	private Behaviour[] pauseBehaviours = null;

	private void Start()
	{
		targets.Add(this);
	}

	private void OnDestroy()
	{
		targets.Remove(this);
	}

	private void OnPause()
	{
		if(pauseBehaviours != null)
		{
			return;
		}

		pauseBehaviours = Array.FindAll(GetComponentsInChildren<Behaviour>(), (obj) => { return obj.enabled; });

		foreach(var com in pauseBehaviours)
		{
			com.enabled = false;
		}
	}

	private void OnResume()
	{
		if(pauseBehaviours  == null)
		{
			return;
		}

		foreach(var com in pauseBehaviours)
		{
			com.enabled = true;
		}

		pauseBehaviours = null;
	}

	public static void Pause()
	{
		foreach(var obj in targets)
		{
			obj.OnPause();
		}
	}

	public static void Resume()
	{
		foreach(var obj in targets)
		{
			obj.OnResume();
		}
	}
}
