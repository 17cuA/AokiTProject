using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeferredObjectDeletion : MonoBehaviour
{
	//変化にかかる時間
	public float deletionTime = 3.0f;
	//経過時間
	public float elapsedTime = 0.0f;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		elapsedTime += Time.deltaTime;  //経過時間
		if(deletionTime <= elapsedTime)
		{
			Destroy(this.gameObject);

		}
	}
}
