//作成者：川村良太
//固定砲台のスクリプト　今は特に動きがないので死ぬ処理だけ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Battery : character_status
{
    new void Start()
    {
		HP_Setting();
		base.Start();
    }

    void Update()
    {
		if (hp < 1)
		{
			Died_Process();
		}
    }
}
