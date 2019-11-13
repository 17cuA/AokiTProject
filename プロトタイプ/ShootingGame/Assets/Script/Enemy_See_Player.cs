//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/06/07
//----------------------------------------------------------------------------------------------
//  ホーミングするエネミー
//----------------------------------------------------------------------------------------------
// 2019/06/07：移動処理
//----------------------------------------------------------------------------------------------
using UnityEngine;

public class Enemy_See_Player : MonoBehaviour
{
	[SerializeField]
	[Header("移動速度")]
	private float speed;
	[SerializeField]
	[Header("回転時間")]
	private float rotation_time;

	private Transform Player_Transform { get; set; }		// プレイヤーのデータ
	
	void Start()
    {
        
    }

    void Update()
    {
		// 向きを切り替える処理
		float step = speed * Time.deltaTime;

		Vector3 Direction = Player_Transform.position - transform.position;

		//指定した方向にゆっくり回転する場合
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Direction), step);

		transform.position = transform.right * speed;
    }
}
