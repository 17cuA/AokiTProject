using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlaying : MonoBehaviour
{

	private bool isPlaying;
	[SerializeField] ParticleSystem particle;

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.X))
		{
			isPlaying= !isPlaying;
		}
	}

	public void Switch()
	{
		if (isPlaying)
		{
			particle.Play(true);
		}
		else
		{
			particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		}
		isPlaying = !isPlaying;
	}
}