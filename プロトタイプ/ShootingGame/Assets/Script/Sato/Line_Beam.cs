// レーザーSphereCast当たり判定型
// 作成者:佐藤翼
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Line_Beam : MonoBehaviour {

	public float range = 57.0f;	//レーザーの長さ
    Ray shotRay;			//レーザーの判定に使用するray
    RaycastHit shotHit;  //レーザーが当たったかの確認
	ParticleSystem beamParticle;	//パーティクルの格納
    LineRenderer lineRenderer;      //LineRendererの格納
	Transform Laser_Size;　//レーザーの太さ
	bool isEnable = true;	//当たり判定の有効化
	float hitstop;	//当たった際の変数

	// Use this for initialization
	void Awake () {
        beamParticle = GetComponent<ParticleSystem> (); 　//particle情報を格納
        lineRenderer = GetComponent<LineRenderer> ();   //linerenderer情報を格納
	}

	//Update is called once per frame

	void Update()
	{
		Laser_Size = this.transform;    //laserの太さを変更

		//if (Input.GetMouseButton(1))        //レーザー発射
		//{
			/*shot();*/ //shot関数を呼び出す
		//}
		//else disableEffect();//エフェクトを止める関数を呼び出す
	}

	//発射に使用する関数
	public void shot()
	{
		//beamParticle.Stop();	//レーザーparticleを止める
		beamParticle.Play();   //レーザーparticleを再生
		lineRenderer.enabled = true;	//linerendererを有効化
		lineRenderer.SetPosition(0, transform.position);	//linerendererの生成位置を固定
		shotRay.origin = transform.position;	//rayの原点を移動する場所に
		shotRay.direction = transform.forward;  //rayの向きをplayerの向いている方向に
		int layerMask = LayerMask.GetMask(new string[] { "Wall", "Enemy" });

		var radius = transform.lossyScale.x * 0.5f;	//レーザーの長さを伸ばす

		var isHit = Physics.SphereCast(transform.position, radius, transform.forward , out shotHit, layerMask);		//スフィアキャストによる当たり判定を取得

		if (isHit)		//当たったものに大しての処理
		{
			hitstop = shotHit.distance;     //SphereCastが何かに当たった際その場所で止まる
			//スフィアキャストがwalllayerに衝突したとき
			if (Physics.SphereCast(transform.position, radius, transform.forward , out shotHit, layerMask,LayerMask.GetMask("Wall")))
			{
				hitstop = shotHit.distance;	//その場所で止める
			}
			//スフィアキャストがenemylayerに衝突したとき
			if (Physics.SphereCast(shotRay, radius, out shotHit, hitstop, LayerMask.GetMask("Enemy")))
			{
				//Destroy(shotHit.collider.gameObject);
				shotHit.collider.GetComponent<character_status>().Damege_Process(1);
			}
		}
	}

	//デバック用（sceneviewに表示されている赤い玉です）
	void OnDrawGizmos()
	{
		if (isEnable == false)
			return;

		var radius = transform.lossyScale.x * 0.5f;

		var isHit = Physics.SphereCast(transform.position, radius, transform.forward * 10, out shotHit);
		Gizmos.color = Color.red;
		if (isHit)
		{
			Gizmos.DrawRay(transform.position, transform.forward * shotHit.distance);
			Gizmos.DrawWireSphere(transform.position + transform.forward * (shotHit.distance), radius);
		}
		else
		{
			Gizmos.DrawRay(transform.position, transform.forward * 100);
		}
	}

    void OnParticleCollision(GameObject obj)
    {
        Debug.Log("衝突");
        //ダメージ処理に使用
        //☺obj.GetComponent<DamageScript>().Damage(attackPower);
    }

//レーザーのエフェクトを止める関数
    public void disableEffect()
    {
        beamParticle.Stop();　//レーザーのparticleを止める
        lineRenderer.enabled = false;	//linerendererを無効化
    }
}