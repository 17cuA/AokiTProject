/*
 * プレイヤーの挙動作成
 * 久保田 達己
 * 
 * 2019/05/28	カットイン時は操作できないようにした
 * 2019/06/07	陳さんの作ったパワーアップ処理統合
 */
using UnityEngine;
using Power;
using StorageReference;

//using Power;
public class TempPlayer : MonoBehaviour
{

	public void Start()
	{
	}
	//プレイヤーがアクティブになった瞬間に呼び出される
	private void OnEnable()
	{
		//第一引数は指定パワーアップ、第二引数はリセット条件、第三引数はリセット処理
		//第二引数の条件が成立するなら、第三引数の処理が実行される
		//PowerManager.Instance.AddCheckFunction(PowerManager.Power.PowerType.SPEEDUP, () => { return player1.hp < 1; }, () => { Debug.Log("スピードアップリセット"); });
		//PowerManager.Instance.AddCheckFunction(PowerManager.Power.PowerType.MISSILE,  () => { return player1.hp < 1; }, () => { Debug.Log("ミサイルリセット"); });
		//PowerManager.Instance.AddCheckFunction(PowerManager.Power.PowerType.DOUBLE,  () => { return player1.hp < 1 || player1.bullet_Type == Player1.Bullet_Type.Laser; }, () => { Debug.Log("ダブルリセット"); });
		//PowerManager.Instance.AddCheckFunction(PowerManager.Power.PowerType.LASER,     () => { return player1.hp < 1 || player1.bullet_Type == Player1.Bullet_Type.Double; }, () => { Debug.Log("レーサーリセット"); });
		//PowerManager.Instance.AddCheckFunction(PowerManager.Power.PowerType.SHIELD,   () => { return player1.shield < 1; }, () => { Debug.Log("シールドリセット"); });
	}
	//プレイヤーのアクティブが切られたら呼び出される
	private void OnDisable()
	{
		///PowerManager.Instance.RemoveCheckFunction(PowerManager.Power.PowerType.SPEEDUP, () => { return player1.hp < 1; }, () => { Debug.Log("スピードアップリセット"); });
		///PowerManager.Instance.RemoveCheckFunction(PowerManager.Power.PowerType.MISSILE, () => { return player1.hp < 1; }, () => { Debug.Log("ミサイルリセット"); });
		///PowerManager.Instance.RemoveCheckFunction(PowerManager.Power.PowerType.DOUBLE, () => { return player1.hp < 1 || player1.bullet_Type == Player1.Bullet_Type.Laser; }, () => { Debug.Log("ダブルリセット"); });
		///PowerManager.Instance.RemoveCheckFunction(PowerManager.Power.PowerType.LASER, () => { return player1.hp < 1 || player1.bullet_Type == Player1.Bullet_Type.Double; }, () => { Debug.Log("レーサーリセット"); });
		///PowerManager.Instance.RemoveCheckFunction(PowerManager.Power.PowerType.SHIELD, () => { return player1.shield < 1; }, () => { Debug.Log("シールドリセット"); });
	}
	

	void Update()
	{



		PowerManager.Instance.Update();
		//ビットン数をパワーマネージャーに更新する
		//---------------------------
		//パワーマネージャー更新
		//PowerManager.Instance.OnUpdate(Time.deltaTime)
	}
}
