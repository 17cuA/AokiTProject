using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moai_EyeLaserRotation : MonoBehaviour
{
	public float rotaZ;
	public float rotaZ_Value;

	public bool isRoll = false;
    void Start()
    {
		//rotaZ = transform.localRotation.z;
    }

    void Update()
    {
		if (isRoll)
		{
			rotaZ -= rotaZ_Value;
			if (rotaZ < 0)
			{
				rotaZ = 0;
			}
			transform.localRotation = Quaternion.Euler(transform.localRotation.x, 90, rotaZ);
		}
	}
}
