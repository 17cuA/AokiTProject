using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior_0 : character_status
{
    public GameObject enemyManagementGameObject;    //統括オブジェクト
    public GameObject enemyGameObject;              //オブジェクト本体
    public GameObject startingPointGameObject;      //移動の始点
    public GameObject endingPointGameObject;        //移動の終点

	public Rigidbody enemyRigidbody;                //Rigidbodyコンポーネント

    public bool movingProcessingTrigger;            //移動処理開始トリガー
    public bool isMoveProcessing;                   //移動処理中

    void Start()
    {
        //Rigidbodyの取得
        enemyRigidbody = enemyGameObject.GetComponent<Rigidbody>();
        //変数の初期化
        movingProcessingTrigger = false;
        isMoveProcessing = false;
    }

    void Update()
    {
        //移動処理
        MovingProcessing();
    }

    //移動処理
    void MovingProcessing()
    {
        //移動中でないとき
        if (!isMoveProcessing)
        {
            //移動開始フラグがたったら
            if (movingProcessingTrigger)
            {
                //始点に移動
                enemyGameObject.transform.position = startingPointGameObject.transform.position;

                enemyGameObject.transform.LookAt(endingPointGameObject.transform);
                //enemyRigidbody.rotation = Quaternion.AngleAxis(movingDirection, Vector3.back);

                //移動状態に変更
                isMoveProcessing = true;

                //力を加える
                enemyRigidbody.velocity = enemyGameObject.transform.forward.normalized * speed;
            }
        }
    }
}
