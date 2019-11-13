//作成者：川村良太
//ゼロスフォースのスクリプト（未完成）ゲームに入れないけど一応残しておく

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ZerosForce : character_status
{
	int hpMax;
	public int saveHP;
    public int saveHP2;

	public float scale;
	public float scaleNum;
	float scaleNum_Max;
	float scaleNum_Maximum;     //スケールが最大の時に縮小させる値
	float scaleMax;
	float scaleMin;
	public float noDamageTime;
	public bool isSmall;
	public bool isBig;
	public bool isDamage = false;
    public bool isNoDamage;

    new void Start()
    {
		isBig = true;
		isSmall = true;
        isNoDamage = true;
        scale = 3.0f;
		scaleNum = 0.05f;
		scaleNum_Max = 0.05f;
		scaleNum_Maximum = 0.007f;
		scaleMax = 9.0f;
		scaleMin = 3.0f;
		noDamageTime = 0;

		HP_Setting();
		hpMax = 100;
		saveHP = hp;
        saveHP2 = hp;
		base.Start();

	}

	void Update()
    {
		//今のHPが前の状態のHPより低かったら（ダメージを受けていたら）
		if (saveHP2 > hp)
        {
			//ダメージ受けたかチェックtrue
            isDamage = true;
			//ダメージ受けていないかチェックfalse
			isNoDamage = false;
			//スケールを増減させる値をリセット
			scaleNum = scaleNum_Max;
			//HP保存
            saveHP2 = hp;
			//ダメージを受けていない時間リセット
            noDamageTime = 0;
        }
		//if (saveHP > hp)
		//{
		//	isDamage = true;
		//	saveHP = hp;
		//}

		//ダメージを受けて、
		if (isDamage && noDamageTime <= 30 && isSmall)
        {
			//スケールを小さく
            scale -= scaleNum;
			//スケールを増減させる値を小さく（小さくなるのを滑らかに）
			scaleNum *= 0.95f;
			//スケールを最小値より小さくさせない
            if (scale < scaleMin)
            {
                scale = scaleMin;
            }
        }
		//ダメージを受けていなくてスケールが最大値より小さい時
        else if (isNoDamage && scale < scaleMax)
        {
			//スケールを大きく
			scale += scaleNum;
			//HP回復
			hp++;
			//HPを最大値より大きくさせない
            if (hp > hpMax)
            {
                hp = hpMax;
            }
			//HP保存
            saveHP = hp;
            saveHP2 = hp;
			//スケールの増減値を大きく
			scaleNum *= 1.1f;
			//スケールの増減値を最大値より大きくさせない
			if (scaleNum > scaleNum_Max)
			{
				scaleNum = scaleNum_Max;
			}
        }

		//ダメージを受けている状態
		if (isNoDamage == false)
		{
			if (saveHP2 == hp)
			{
				noDamageTime++;
				if (noDamageTime > 30)
				{
					isNoDamage = true;
					scaleNum = 0.005f;
				}
			}
		}
		else
		{
			//noDamageTime = 0;
			//saveHP = hp;
		}

		if (scale >= scaleMax)
		{
			if(isBig)
			{
				scale += scaleNum_Maximum;
				if (scale > scaleMax + 0.3f)
				{
					scale = scaleMax + 0.3f;
					isBig = false;
				}
			}
			else if(!isBig)
			{
				scale -= scaleNum_Maximum;
				if (scale < scaleMax)
				{
					scale = scaleMax;
					isBig = true;
				}
			}
		}

		if (scale > scaleMin + 0.4f)
		{
			isSmall = true;
		}

		if (scale <= scaleMin + 0.4f)
		{
			if(isSmall)
			{
				if (scale <= scaleMin)
				{
					if(isSmall)
					{
						//小さくなるチェックをfalse
						isSmall = false;
						scaleNum = 0.005f;
					}
				}
			}
			else if(!isSmall)
			{
				scale += scaleNum;
				scaleNum *= 1.1f;
				if (scaleNum > scaleNum_Max)
				{
					scaleNum = scaleNum_Max;
				}
				if (scale > scaleMin + 0.4f)
				{
					if (!isSmall)
					{
						isSmall = true;
					}
				}
			}
		}
		transform.localScale = new Vector3(scale, scale, scale);

		if (hp < 1)
		{
			Reset_Status();
			Died_Process();
		}
	}
}
