//作成者：川村良太
//隕石のスクリプト（古いやつ）

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Meteor : character_status
{
    Vector3 velocity;
    new void Start()
    {
        HP_Setting();
        base.Start();
    }

    new void Update()
    {
        //移動
        velocity = gameObject.transform.rotation * new Vector3(-speed, 0, 0);
        gameObject.transform.position += velocity * Time.deltaTime;

        if (hp < 1)
        {
            ParticleCreation(10);
            Died_Process();
        }
    }
}
