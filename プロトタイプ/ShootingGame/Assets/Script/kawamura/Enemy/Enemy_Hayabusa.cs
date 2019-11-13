//作成者：川村良太
//ハヤブサエネミーの挙動

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hayabusa : character_status
{
    public enum Status
    {
        Up,
        Down,
    }

    public Status eStatus;

    Vector3 velocity;

	public int turnCnt;

    public float speedX;
    public float speedY;
    public float speedZ;
	public float saveSpeedZ;

    public float rotaX;
    public float saveRotaX;
    public float rotaX_ChangeValue;
    public float turnDelayCnt;
    public float turnDelayMax;         //曲がるときにまっすぐ進む間隔

	public float turnPosX;

    public bool isFirstTurn;
    public bool isTurn;
    public bool isDelayStart;
	public bool isTurnCheck;
	public bool magarimasita = false;
    new void Start()
    {
		isTurnCheck = true;
        isFirstTurn = true;

		saveSpeedZ = speedZ;
        //rotaX = transform.eulerAngles.x;
        //saveRotaX = rotaX;

        HP_Setting();
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        velocity = gameObject.transform.rotation * new Vector3(0, 0, -speedZ);
        gameObject.transform.position += velocity * Time.deltaTime;


		if (isFirstTurn)
        {
			if (transform.position.x < -turnPosX)
            {
                isFirstTurn = false;
                isTurn = true;
            }
        }

        if(isTurn)
        {
			//speedZ = 4;
			Turn_RightAngle();
        }
		if (turnCnt == 2)
		{
			isTurn = false;
			isTurnCheck = false;
			speedZ = saveSpeedZ;
		}

		if (isDelayStart)
        {
            if (turnDelayCnt >= turnDelayMax)
            {
                isTurn = true;
				isDelayStart = false;
				turnDelayCnt = 0;
            }
            else
            {
                turnDelayCnt++;
            }
        }
    }

    //曲がる方向と曲がる間隔(90度回転した後まっすぐ進む間隔)の設定
    public void SetStatus(Status setStatus, float set)
    {
        eStatus = setStatus;
        turnDelayMax = set;
    }

    //90度曲がる(自分の向きを変える)関数
    void Turn_RightAngle()
    {
		if(isTurnCheck)
		{
			switch (eStatus)
			{
				case Status.Up:
					rotaX = rotaX - rotaX_ChangeValue;
					magarimasita = true;
					if (rotaX < saveRotaX - 90f)
					{
						saveRotaX -= 90f;
						rotaX = saveRotaX;
						isTurn = false;
						speedZ = saveSpeedZ;
						isDelayStart = true;
						magarimasita = false;
						turnCnt++;
					}
					transform.rotation = Quaternion.Euler(rotaX, -90f, 90f);

					break;

				case Status.Down:
					rotaX = rotaX + rotaX_ChangeValue;
					if (rotaX > saveRotaX + 90f)
					{
						saveRotaX += 90;
						rotaX = saveRotaX;
						isTurn = false;
						isDelayStart = true;
						turnCnt++;
						if (turnCnt == 2)
						{
							isTurnCheck = false;
							speedZ = saveSpeedZ;

						}

					}
					transform.rotation = Quaternion.Euler(rotaX, transform.eulerAngles.y, transform.eulerAngles.z);
					break;
			}
		}
	}
}
