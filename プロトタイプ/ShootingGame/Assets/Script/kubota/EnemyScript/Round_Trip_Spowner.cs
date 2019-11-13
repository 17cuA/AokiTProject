using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_Trip_Spowner : MonoBehaviour
{
	GameObject EnemyPrefab;
	public int DelayMax;
	int Delay;
    void Start()
    {
        EnemyPrefab = Resources.Load("Enemy/Entrance_And_Exit") as GameObject;
	}

    // Update is called once per frame
    void Update()
    {
		Delay++;
		if(Delay > DelayMax)
		{
			Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
			Delay = 0;
		}
    }
}
