using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotMove : MonoBehaviour
{
	Vector3 notMovePos;
	Quaternion notMoveRota;

	private void Awake()
	{
		notMovePos = transform.position;
		notMoveRota = transform.rotation;
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = notMovePos;
		transform.rotation = notMoveRota;
    }
}
