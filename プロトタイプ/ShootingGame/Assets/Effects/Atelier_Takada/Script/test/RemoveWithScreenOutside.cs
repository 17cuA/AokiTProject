using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWithScreenOutside : MonoBehaviour
{

	// Start is called before the first frame update
	void Start()
    {
	}

    // Update is called once per frame
    void Update()
    {
		if(transform.position.y <= -30.0f)
		{
			Destroy(this.gameObject);
		}
	}
}
