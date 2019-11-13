/*
 * 久保田達己
 * 敵キャラの動きを三次関数のグラフを使って動かす
 * 敵キャラの動きが加速と減速を行うため没となった。
 * 結局何かほかのエネミーの動きに使えるかもしれないため残しておく	
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_Trip_Enemy : MonoBehaviour
{
	private float X_speed;		//Ｘ軸の行動の時のスピード
	private float Y_speed;		//Ｙ軸の行動の時のスピード
	private float Z_speed;       //Ｚ軸の行動の時のスピード
	private float a;
	private float movetime;
	Vector3 pos;				//複雑な動きをするときに計算結果をxyzごとに入れまとめて動かす
	private float start_pos_x;
    void Start()
    {
		start_pos_x = -15;
		pos = Vector3.zero;
        X_speed = 6f;
        Y_speed = 5.5f;
        Z_speed = 1;
        a = 1.5f;
		movetime = 0;
    }

    // Update is called once per frame
    void Update()
    {
		movetime += Time.deltaTime;
		//pos = Vector3.zero;
		pos = new Vector3(start_pos_x,0,0); //中心を決めます。今回は(0,0,0)
		float x1 = (Mathf.Pow(movetime - a, 3) * X_speed) / 5;
		float x2 = Mathf.Pow(movetime - a, 2) * Y_speed;
		float x3 = (movetime - a) * Z_speed;
		pos.x += x1 - x2 + x3;
		if(transform.position.z < 0)
		{
			pos.z += transform.position.z + 3 * Time.deltaTime ;
		}

		transform.position = pos;
    }
}
