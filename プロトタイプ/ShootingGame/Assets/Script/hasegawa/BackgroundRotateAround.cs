/*
 20190818 作成
 author hasegawa yuuta
*/
/* 背景を回転させる */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotateAround : MonoBehaviour
{
	[SerializeField] float rotateSpeed = 0.025f;
	void Start()
	{
	}
	void Update()
	{
		transform.RotateAround(Vector3.forward + Vector3.up * 0.2f, rotateSpeed * Time.deltaTime);
	}
}
