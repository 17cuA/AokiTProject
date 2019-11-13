using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public AudioSource audioSource; //ユニティ側にて設定
	public AudioClip audioClip;         //unity側から設定

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			audioSource.PlayOneShot(audioClip);

		}
	}
}
