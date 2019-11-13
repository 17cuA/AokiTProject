//作成者：川村良太
//シェーダーのHSVを変えるスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSVColorController : MonoBehaviour
{
	private Material material = null;
	[Range(0f, 1f)]
	public float hue = 0f;
	[Range(0f, 1f)]
	public float sat = 1f;
	[Range(0f, 1f),Header("0で真っ黒,１が普通の色")]
	public float val = 1f;//0で真っ黒（明るさが）１が普通の色

	// Use this for initialization
	void Start()
	{
		this.material = gameObject.GetComponent<Renderer>().material;
		val = 0.4f;
	}

	// Update is called once per frame
	void Update()
	{
		this.material.SetFloat("_Hue", hue);
		this.material.SetFloat("_Sat", sat);
		this.material.SetFloat("_Val", val);
	}
}
