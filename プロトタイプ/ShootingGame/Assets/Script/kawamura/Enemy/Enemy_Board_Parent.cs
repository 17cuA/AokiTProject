//作成者：川村良太
//バキュラの3Dオブジェクトの一番上の親につけるオブジェクトのスクリプト（どのサイズのバキュラにもつける）
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_Board_Parent : MonoBehaviour
{
    public GameObject parentObj;
    public GameObject childObj;
	public GameObject quarterObj;					//4分の1オブジェクト
	public GameObject quarter_OneSixteenthObj;		//16分の1オブジェクト

	public GameObject saveQuaterObj;

	public Quaternion randRota_TopLeft;
	public Quaternion randRota_TopRight;
	public Quaternion randRota_BottomLeft;
	public Quaternion randRota_BottomRight;

	public Enemy_Board_Parent ebp;
    public EnemyGroupManage egm;

	Vector3 velocity;
	public int divisionCnt = 0;
    public float createSpeedX;
	public float speedX;
    public float speedX_Max;
    public float speedX_Min;
    public float damageDelay;

	public string myName;
	public string parentName;

	float rotaZ;

	public bool isDead = false; //攻撃で死んだとき
    public bool isDisappearance = false;　//画面外で消えるとき

    public bool isCreate = false;	//分裂後かどうか
    public bool isDamage;
	private void Awake()
	{
		myName = gameObject.name;
        childObj = transform.GetChild(0).gameObject;
		//speedX = 15;
	}
	void Start()
    {
        parentObj = transform.parent.gameObject;
		parentName = parentObj.name;
        egm = parentObj.GetComponent<EnemyGroupManage>();

		//ボスの時に出るバキュラなら
        if (parentName == "Enemy_BossBacula_Four(Clone)")
        {
			//移動しないようにする
            speedX = 0;
            speedX_Max = 0;
            speedX_Min = 0;
        }
		//それ以外はスピードを入れる
        else
        {
            speedX_Max = speedX;
        }
    }

    void Update()
    {
		//分裂直後の状態
		if (isCreate)
		{
			velocity = gameObject.transform.rotation * new Vector3(-createSpeedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
			createSpeedX -= 1.0f;
			if (createSpeedX == 0)
			{
				//speedX = 0;
				if (transform.rotation.z > 0)
				{
					transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z - 10.0f);
					if (transform.rotation.z < 0)
					{
						transform.rotation = Quaternion.Euler(0, 0, 0);
						isCreate = false;
					}
				}
				else if (transform.rotation.z < 0)
				{
					transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + 10.0f);
					if (transform.rotation.z > 0)
					{
						transform.rotation = Quaternion.Euler(0, 0, 0);
						isCreate = false;
					}

				}
				//isCreate = false;
			}
		}

		if (parentObj.name == "Enemy_BossBacula_Four")
		{

		}
		else if (!isCreate)
		{
			velocity = gameObject.transform.rotation * new Vector3(speedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

		}

        if(isDamage)
        {
            damageDelay++;
            if (damageDelay > 10)
            {
                isDamage = false;
            }
        }
        else if (!isDamage)
        {
            if (speedX > speedX_Max)
            {
                speedX -= 0.05f;
                if (speedX < speedX_Max)
                {
                    speedX = speedX_Max;
                }
            }
        }
		//if (speedX > 0)
		//{
		//	speedX -= 1.0f;
		//	if (speedX < 0)
		//	{
		//		speedX = 0;
		//	}
		//}

		if (myName != "Enemy_Bacula"&& myName != "Enemy_BossBacula")
		{
			if (transform.position.y > 4.4f)
			{
				transform.position = new Vector3(transform.position.x, 4.4f, transform.position.z);
			}
			else if (transform.position.y < -4.4f)
			{
				transform.position = new Vector3(transform.position.x, -4.4f, transform.position.z);
			}
		}

		randRota_TopLeft = new Quaternion(0, 0, Random.Range(180, 270), 0);
		randRota_TopRight = new Quaternion(0, 0, Random.Range(270, 360), 0);
		randRota_BottomLeft = new Quaternion(0, 0, Random.Range(0, 90), 0);
		randRota_BottomRight = new Quaternion(0, 0, Random.Range(90, 180), 0);

        //死んだとき自分より小さいバキュラを生成
		if (isDead)
		{
			if (myName == "Enemy_BossBacula")
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, transform.position, Quaternion.identity);
			}
			DeathTreatment();

		}
        else if(isDisappearance)
        {
            DisappearanceTreatment();
        }
    }

    //-------------ここから関数-------------------

    //攻撃によって死亡した時の処理
    void DeathTreatment()
    {
        if (myName == "Enemy_Bacula_OneSixteenth(Clone)")
        {
            //群を管理している親の残っている敵カウントマイナス
            egm.remainingEnemiesCnt--;
            //倒された敵のカウントプラス
            egm.defeatedEnemyCnt++;
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    //右上に生成
                    case 0:
						if (myName == "Enemy_Board" || myName == "Enemy_Bacula" || myName == "Enemy_BossBacula")
                        {
                            saveQuaterObj = Instantiate(quarterObj, childObj.transform.position, transform.rotation);
                            ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
                            saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(195, 255));
                            saveQuaterObj.transform.parent = parentObj.transform;
                            //ebp.divisionCnt = 1;
                            ebp.isCreate = true;
                            ebp.createSpeedX = 15;
                            ebp = null;
                            saveQuaterObj = null;

                        }
                        else if (myName == "Enemy_Board_Quarter(Clone)" || myName == "Enemy_Bacula_Quarter(Clone)")
                        {
                            saveQuaterObj = Instantiate(quarter_OneSixteenthObj, childObj.transform.position, transform.rotation);
                            ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
                            saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(195, 255));
                            saveQuaterObj.transform.parent = parentObj.transform;
                            //ebp.divisionCnt = 1;
                            ebp.isCreate = true;
                            ebp.createSpeedX = 15;
                            ebp = null;
                            saveQuaterObj = null;
                            //isDead = false;

                            //gameObject.SetActive(false);

                        }
                        else
                        {
                            //何もしない

                        }
                        //else if (myName == "Enemy_Board_OneSixteenth(Clone)" || myName == "Enemy_Bacula_OneSixteenth(Clone)")
                        //{
                        //	//なにもしない
                        //}

                        break;
                    case 1:
                        //左上に生成
                        if (myName == "Enemy_Board" || myName == "Enemy_Bacula" || myName == "Enemy_BossBacula")
                        {
                            saveQuaterObj = Instantiate(quarterObj, childObj.transform.position, transform.rotation);
                            ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
                            saveQuaterObj.transform.parent = parentObj.transform;
                            //ebp.divisionCnt = 1;
                            ebp.isCreate = true;
                            ebp.createSpeedX = 15;
                            ebp = null;
                            saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(285, 345));
                            saveQuaterObj = null;

                        }
                        else if (myName == "Enemy_Board_Quarter(Clone)" || myName == "Enemy_Bacula_Quarter(Clone)")
                        {
                            saveQuaterObj = Instantiate(quarter_OneSixteenthObj, childObj.transform.position, transform.rotation);
                            ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
                            saveQuaterObj.transform.parent = parentObj.transform;
                            //ebp.divisionCnt = 1;
                            ebp.isCreate = true;
                            ebp.createSpeedX = 15;
                            ebp = null;
                            saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(285, 345));
                            saveQuaterObj = null;

                        }
                        else if (myName == "Enemy_Board_OneSixteenth(Clone)" || myName == "Enemy_Bacula_OneSixteenth(Clone)")
                        {
                            //何もしない
                        }

                        break;
                    case 2:
                        //左下に生成
                        if (myName == "Enemy_Board" || myName == "Enemy_Bacula" || myName == "Enemy_BossBacula")
                        {
                            saveQuaterObj = Instantiate(quarterObj, childObj.transform.position, transform.rotation);
                            ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
                            saveQuaterObj.transform.parent = parentObj.transform;
                            //ebp.divisionCnt = 1;
                            ebp.isCreate = true;
                            ebp.createSpeedX = 15;
                            ebp = null;
                            saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(15, 75));
                            saveQuaterObj = null;

                        }
                        else if (myName == "Enemy_Board_Quarter(Clone)" || myName == "Enemy_Bacula_Quarter(Clone)")
                        {
                            saveQuaterObj = Instantiate(quarter_OneSixteenthObj, childObj.transform.position, transform.rotation);
                            ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
                            saveQuaterObj.transform.parent = parentObj.transform;
                            //ebp.divisionCnt = 1;
                            ebp.isCreate = true;
                            ebp.createSpeedX = 15;
                            ebp = null;
                            saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(15, 75));
                            saveQuaterObj = null;
                        }
                        else if (myName == "Enemy_Board_OneSixteenth(Clone)" || myName == "Enemy_Bacula_OneSixteenth(Clone)")
                        {
                            //何もしない
                        }

                        break;
                    case 3:
                        //右下に生成
                        if (myName == "Enemy_Board" || myName == "Enemy_Bacula" || myName == "Enemy_BossBacula")
                        {
                            saveQuaterObj = Instantiate(quarterObj, childObj.transform.position, transform.rotation);
                            ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
                            saveQuaterObj.transform.parent = parentObj.transform;
                            //ebp.divisionCnt = 1;
                            ebp.isCreate = true;
                            ebp.createSpeedX = 15;
                            ebp = null;
                            saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(105, 165));
                            saveQuaterObj = null;

                        }
                        else if (myName == "Enemy_Board_Quarter(Clone)" || myName == "Enemy_Bacula_Quarter(Clone)")
                        {
                            saveQuaterObj = Instantiate(quarter_OneSixteenthObj, childObj.transform.position, transform.rotation);
                            ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();
                            saveQuaterObj.transform.parent = parentObj.transform;
                            //ebp.divisionCnt = 1;
                            ebp.isCreate = true;
                            ebp.createSpeedX = 15;
                            ebp = null;
                            saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(105, 165));
                            saveQuaterObj = null;
                        }
                        else if (myName == "Enemy_Board_OneSixteenth(Clone)" || myName == "Enemy_Bacula_OneSixteenth(Clone)")
                        {
                            //何もしない
                        }
                        //ebp = saveQuaterObj.GetComponent<Enemy_Board_Parent>();

                        break;
                }

            }
        }
        isDead = false;

        gameObject.SetActive(false);
    }

    void DisappearanceTreatment()
    {
        if (myName == "Enemy_Board" || myName == "Enemy_Bacula" || myName == "Enemy_BossBacula")
        {
            //群を管理している親の残っている敵カウントマイナス
            egm.remainingEnemiesCnt -= 16;
            //画面外にでた敵のカウントプラス
            egm.notDefeatedEnemyCnt += 16;

        }
        else if (myName == "Enemy_Board_Quarter(Clone)" || myName == "Enemy_Bacula_Quarter(Clone)")
        {
            //群を管理している親の残っている敵カウントマイナス
            egm.remainingEnemiesCnt -= 4;
            //画面外にでた敵のカウントプラス
            egm.notDefeatedEnemyCnt += 4;

        }
        else if (myName == "Enemy_Bacula_OneSixteenth(Clone)")
        {
            //群を管理している親の残っている敵カウントマイナス
            egm.remainingEnemiesCnt--;
            //画面外にでた敵のカウントプラス
            egm.notDefeatedEnemyCnt++;
        }
        isDisappearance = false;
        gameObject.SetActive(false);

    }
}
