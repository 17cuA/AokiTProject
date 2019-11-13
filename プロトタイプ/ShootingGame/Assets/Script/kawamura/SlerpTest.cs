//作成者：川村良太
//Slerpの動き確認用スクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpTest : MonoBehaviour
{
	public Vector3 startMarker;
	public Vector3 endMarker;
    public Vector3 defaultPos;
	float startTime;
	float present_Location;
	public float testSpeed = 1.0f;

	private float distance_two;

	void Start()
    {
		//startMarker = new Vector3(5.0f, transform.position.y, 40.0f);
		//endMarker = new Vector3(5.0f, transform.position.y, 0);
		distance_two = Vector3.Distance(startMarker, endMarker);
        defaultPos = transform.position;
	}

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.position = defaultPos;
            startTime = 0;
        }
		present_Location = (Time.time * testSpeed) / distance_two;
		transform.position = Vector3.Slerp(startMarker, endMarker, startTime);
		startTime += Time.deltaTime;

	}
}
