using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Spowner : MonoBehaviour
{
	GameObject EnemyPrefab;
	private GameObject[] Enemy_Catch = new GameObject[5];
	public int DelayMax;
	int Delay;
	private int n;
    void Start()
    {
        EnemyPrefab = Resources.Load("Enemy/Entrance_And_Exit") as GameObject;
		n = 0;
	}

    // Update is called once per frame
    void Update()
    {
		Delay++;
		if(Delay > DelayMax && Enemy_Catch[4] == null)
		{
			Enemy_Catch[n] = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
			n++;
			Delay = 0;
		}
    }
}
