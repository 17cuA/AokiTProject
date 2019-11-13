//作成日：2019/04/22
//作成者：17CU0108 川村良太
//内　容：プレイヤーを追従する敵の動き
//
//現状だと追従してエネミーがプレイヤーと同じ座標になろうとするのでガタガタします。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターステータスを継承
public class FollowUpMove : character_status
{
	public enum Enemy_Type
	{
		Front,
		Back,
		None
	}

	public Enemy_Type E_Type;	//どのように進むのかをenum型で判定させるために使う（unity側にて設定）	
    GameObject Player;	//プレイヤーの位置情報を取得するための変数

    void Start()
    {
		//capsuleCollider = GetComponent<CapsuleCollider>();  //カプセルコライダーの情報取得

		Travelling_Direction();
		Player = GameObject.Find("Player_Demo 1(Clone)");           //プレイヤーを名前で検索
		E_Type = Type_Determining();
	}

	void Update()
    {
        //プレイヤーの座標がエネミーの座標より大きいとき
        if (Player.transform.position.y > transform.position.y)
        {
			//エネミーを上に移動させる
			direction.y = 1;
        }
        //プレイヤーの座標がエネミーの座標より小さいとき
        else if (Player.transform.position.y < transform.position.y)
        {
			//エネミーを下に移動させる
			direction.y = -1;
        }
        else
        {
			//プレイヤーとｙ座標が同じときはまっすぐ進んでくる
			direction.y = 0;
        }
		Died_Process(hp);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.name == "Player_Bullet(Clone)")
		{
			//弾のダメージを取得するための弾のステータスの情報取得
			bullet_status Bs = col.gameObject.GetComponent<bullet_status>();
			//弾のダメージの値だけ体力を減らす
			hp -= (int)Bs.attack_damage;
		}
	}
	void Died_Process(float hp)
	{
		//体力が1未満だったらオブジェクトの消去
		if (hp < 1) Destroy(gameObject);
	}
	private Enemy_Type Type_Determining()
	{
		Enemy_Type type;
		if (gameObject.name == "Enemy_Test1(Clone)") type = Enemy_Type.Front;
		else if (gameObject.name == "Enemy_Test2(Clone)") type = Enemy_Type.Back;
		else type = Enemy_Type.None;
		return type;
	}
	//進行方向の決定（ｘ軸）
	private void Travelling_Direction()
	{
		switch (E_Type)
		{
			case Enemy_Type.Front:
				direction = new Vector3(-1, 0, 0);                   //進む方向を決めるための変数を初期化(プレイヤーより後ろに向かって
				transform.eulerAngles = new Vector3(0, -90, 0);
				break;
			case Enemy_Type.Back:
				direction = new Vector3(1, 0, 0);                   //進む方向を決めるための変数を初期化（プレイヤーより前に向かって）
                transform.eulerAngles = new Vector3(0, 90, 0);
                break;
			case Enemy_Type.None:
				direction = new Vector3(0, 0, 0);                   //進む方向を決めるための変数を初期化（動かない）
				break;
			default:
				break;
		}
	}
	
}
