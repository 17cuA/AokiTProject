using UnityEngine;
using System.Collections;

public class MoveMaterial : MonoBehaviour
{
	public float xSpead = 0.0f;	//横
	public float ySpead = 0.0f;	//縦
	
	void Update()
	{
		float yScroll = Mathf.Repeat( Time.time * ySpead,1);
		float xScroll = Mathf.Repeat(Time.time * xSpead, 1);
		Vector2 offset = new Vector2(xScroll, yScroll);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}