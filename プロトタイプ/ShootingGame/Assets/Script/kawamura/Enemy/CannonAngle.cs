//作成者：川村良太
//砲台の敵の向きを変えるスクリプト。180度の範囲で動く

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAngle : MonoBehaviour
{
    public GameObject playerObj;   //プレイヤー（向く対象）情報を入れる変数
    Vector3 playerPos;      //プレイヤー（向く対象）の座標を入れる変数
    Vector3 myPos;     //自分の座標を入れる変数
    Vector3 dif;            //対象と自分の座標の差を入れる変数



    public bool imasuyo = false;
    public bool isPlayerActive = true;
    float radian;                   //ラジアン
    public float degree;            //角度
    public float positiveDegree;    //正の数で表した角度	
    public float myRotaZ;
    public float rotaZ_ChangeValue; //角度の増減値
    public float standardDegree;    //最初の向き（これを基準に左右90度まで回転）
    public float standardDig_high;  //最初の向きから90足した数
    public float standardDig_low;   //最初の向きから90引いた数

    public bool isPlus;
    public bool isMinus;
    public bool isRotaChange;

    private void Awake()
    {

    }
    void Start()
    {
		//最初の角度を見て保存
        standardDegree = transform.eulerAngles.z;
        myRotaZ = transform.eulerAngles.z;
        //最大の角度設定
        standardDig_high = standardDegree + 90.0f;
        if (standardDig_high > 360)
        {
            isPlus = true;
            isMinus = false;
        }
        ////角度を直す
        //if (standardDig_high > 360)
        //{
        //    standardDig_high -= 360.0f;
        //}

        //最小の角度設定
        standardDig_low = standardDegree - 90.0f;
        if (standardDig_low < 0)
        {
            isMinus = true;
            isPlus = false;
        }
        ////角度を直す
        //if (standardDig_low < 0)
        //{
        //    standardDig_low += 360.0f;
        //}

    }

    void Update()
    {
        //プレイヤー（向く対象）情報が入っていなくて、プレイヤーが生きていたら
        if (playerObj == null && isPlayerActive)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }

		//プレイヤーが入っていたら
        if (playerObj)
        {
			//プレイヤーが生きていたら
            if (isPlayerActive)
            {
                if (playerObj.activeInHierarchy)
                {
                    //imasuyo = true;
                }
                else
                {
                    playerObj = null;
                    isPlayerActive = false;
                }

            }
        }
        //プレイヤー（向く対象）の座標を入れる
        if (playerObj)
        {
            playerPos = playerObj.transform.position;
        }
        //自分の座標を入れる
        myPos = this.transform.position;

        //角度計算の関数呼び出し
        DegreeCalculation();

        if (isPlus)
        {
            if (positiveDegree < 0)
            {
                positiveDegree += 360.0f;
            }

            if (degree > 0)
            {
                positiveDegree += 360;
            }
        }
        else if (isMinus)
        {
            
        }
        else
        {
            if (positiveDegree < 0)
            {
                positiveDegree += 360.0f;
            }
        }

        if (isRotaChange)
        {
            if (positiveDegree < myRotaZ)
            {
                myRotaZ -= rotaZ_ChangeValue;
                if (myRotaZ < standardDig_low)
                {
                    myRotaZ = standardDig_low;
                }
            }
            else if (positiveDegree > myRotaZ)
            {
                myRotaZ += rotaZ_ChangeValue;
                if (myRotaZ > standardDig_high)
                {
                    myRotaZ = standardDig_high;
                }
            }
        }

        if (positiveDegree < (myRotaZ + 1) && positiveDegree > (myRotaZ - 1))
        {
            isRotaChange = false;
        }
        else
        {
            isRotaChange = true;
        }
        //自分を対象の方向へ向かせる
        this.transform.rotation = Quaternion.Euler(0, 0, myRotaZ);

    }

    //角度を求める関数
    void DegreeCalculation()
    {
        //座標の差を入れる
        dif = playerPos - myPos;

        //ラジアンを求める
        radian = Mathf.Atan2(dif.y, dif.x);

        //角度を求める
        degree = radian * Mathf.Rad2Deg;
        positiveDegree = degree;
    }
}
