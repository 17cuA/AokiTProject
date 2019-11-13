using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai_Mini_Group : MonoBehaviour
{
	public GameObject item;
	public GameObject parentObj;
	GameObject[] childObjects;

	Vector3 velocity;
	Vector3 defaultPos;
	public Vector3 startPos;
	public Vector3 endPos;

	public int EmptyNum;


	//[Header("入力用 スピードX")]
	public float speedX;
	[Header("入力用 これもスピードX")]
	public float defaultSpeedX_Value;
	public float lerpSpeed;
	public float moveDelayCnt;      //いったん止まってから動き出すためのカウント
	[Header("入力用 一時停止時に止まっている時間フレーム")]
	public float moveDelayMax;      //一時停止の時間

	public int childNum;                    //最初の敵(子供)の総数
	public int remainingEnemiesCnt;         //残っている敵の数
	public int defeatedEnemyCnt = 0;        //倒された敵の数
	public int notDefeatedEnemyCnt = 0;     //倒されずに画面外に出た数
	public string myName;

	public bool once = true;
	public bool isChildRoll = false;        //子供が回転し始めるときにtrue
	public bool isChildMove = false;        //子供が動き始める(上下の移動)
	public bool isLerp = true;
	public bool isMove = false;              //自分（親）が動くときに使う
	public bool isStop = false;
	public bool isDead = false;

	private void Awake()
	{
		myName = gameObject.name;
		childNum = transform.childCount;
		remainingEnemiesCnt = childNum;
		childObjects = new GameObject[childNum];
		for (int i = 0; i < childNum; i++)
		{
			childObjects[i] = transform.GetChild(i).gameObject;
		}

	}

	private void OnEnable()
	{

		//defeatedEnemyCnt = 0;
		//notDefeatedEnemyCnt = 0;
	}
	void Start()
	{
		defaultSpeedX_Value = speedX;
		isStop = false;
		//remainingEnemiesCnt = childNum;
	}

	void Update()
	{
		if (once)
		{
			EmptyNum = Random.Range(1, 7);
			//EmptyNum = Random.Range(1, 3);

			//if(EmptyNum==1)
			//{
			//	EmptyNum = 1;
			//}
			//else if(EmptyNum==2)
			//{
			//	EmptyNum = 6;
			//}

			for (int i = 0; i < childObjects.Length; i++)
			{
				childObjects[i].gameObject.SetActive(true);
			}
			defaultPos = transform.position;
			startPos = defaultPos;

			endPos = new Vector3(defaultPos.x - 3.5f, -1.21f, 0);
			isChildMove = false;
			isMove = false;
			once = false;
		}
		//if (transform.position.x < 6 && !isStop)
		if (isLerp)
		{
			transform.localPosition = Vector3.Lerp(startPos, endPos, lerpSpeed);
			lerpSpeed += 0.035f;
			if (transform.localPosition == endPos)
			{
				isLerp = false;
				lerpSpeed = 0;
			}
		}

		if (isMove)
		{
			velocity = gameObject.transform.rotation * new Vector3(speedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

		}
		else if (transform.position == endPos && !isStop)
		{
			isStop = true;
			speedX = 0;
			isChildRoll = true;
			isChildMove = true;
			//isMove = false;
		}
		else if (isStop)
		{
			moveDelayCnt++;
			if (moveDelayCnt > moveDelayMax)
			{
				isMove = true;
				speedX = defaultSpeedX_Value;
				moveDelayCnt = 0;
			}
		}

		//倒された敵の数と倒されずに画面外に出た敵の数の合計が最初の子供の数と同じになったら
		if (defeatedEnemyCnt + notDefeatedEnemyCnt >= childNum)
		{
			SettingReset();
			gameObject.SetActive(false);
			//Destroy(this.gameObject);
			//gameObject.SetActive(false);

			//isDead = true;
			//Died_Process();
		}
	}

	void SettingReset()
	{
		//倒されたのと画面外に出たカウントをリセット
		notDefeatedEnemyCnt = 0;
		defeatedEnemyCnt = 0;
		remainingEnemiesCnt = childNum;
		moveDelayCnt = 0;

		once = true;
		isChildRoll = false;
		isChildMove = false;
		isStop = false;
		isLerp = true;
	}
	//アイテムを落とす群かを設定する(trueで落とす、falseで落とさない)
	public void WhetherToDropTheItem(bool isDrop)
	{
		//isItemDrop = isDrop;
	}
}