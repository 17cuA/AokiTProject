using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraPosition : MonoBehaviour
{
	[SerializeField] private GameObject mainCamera;			// レンダリングをしているオブジェクト
	[SerializeField] private Vector3 offsetWorldPosition;	// ずらす

	void LateUpdate()
	{
		transform.position = mainCamera.transform.position + offsetWorldPosition;
	}
}
