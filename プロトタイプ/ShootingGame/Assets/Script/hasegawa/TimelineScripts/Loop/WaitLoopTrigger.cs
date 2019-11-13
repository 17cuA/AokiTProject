using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitLoopTrigger : MonoBehaviour
{
	[SerializeField]
	private bool trigger = false;
	public bool Trigger
	{
		get { return trigger; }
		set { trigger = value; }
	}
}
