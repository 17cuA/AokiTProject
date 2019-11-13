using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class ParticleAlphaBehaviour : PlayableBehaviour
{
	[SerializeField, Range(0f, 1f)] float alpha;
	public float Alpha { set { alpha = value; } get { return alpha; } }
}
