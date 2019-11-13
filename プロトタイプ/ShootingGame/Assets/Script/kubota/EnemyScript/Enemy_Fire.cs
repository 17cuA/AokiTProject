using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fire : MonoBehaviour
{
    float Shot_Delay;
    [SerializeField]
    [Tooltip("弾を発射する間隔 単位：秒")]
    int Shot_Delay_Max;

	private Transform Enemy_transform;
    private GameObject Bullet;  //弾のプレハブ、リソースフォルダに入っている物を名前から取得。
    // Start is called before the first frame update
    void Start()
    {
		Enemy_transform = transform.parent;
        Bullet = Resources.Load("Enemy_Bullet") as GameObject;
        Shot_Delay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Shot_Delay > Shot_Delay_Max && transform.position.x < 9)
        {
            Instantiate(Bullet, gameObject.transform.position,Enemy_transform.rotation);
            Shot_Delay = 0;
        }
        Shot_Delay += Time.deltaTime;
    }
}
