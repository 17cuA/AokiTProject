//作成日2019/06/13
// プレイヤーのショットの制御
// 作成者:諸岡勇樹
/*
 * 2019/06/14 バレットの基底クラスの継承、動きの追加
 */
using UnityEngine;

public class Player_Bullet : bullet_status
{
    private new void Start()
    {
		base.Start();
		Tag_Change("Player_Bullet");
    }

	private new void Update()
    {
		base.Update();
		Moving_To_Facing();
	}
}
