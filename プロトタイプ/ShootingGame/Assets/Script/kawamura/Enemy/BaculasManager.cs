using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaculasManager : MonoBehaviour
{
    GameObject[] childObjects;
	GameObject createEnemyObj;

    public int childNum;                    //最初の敵(子供)の総数
    public int remainingEnemiesCnt;         //残っている敵の数
    public int defeatedEnemyCnt = 0;        //倒された敵の数
    public int notDefeatedEnemyCnt = 0;     //倒されずに画面外に出た数
    public string myName;

    public Transform itemTransform;
    public Vector3 itemPos;

	EnemyCreate eCreate;

    public bool isDead = false;
    public bool isItemDrop = true;

    private void Awake()
    {
		createEnemyObj = GameObject.Find("CreateEnemy");
		eCreate = createEnemyObj.GetComponent<EnemyCreate>();

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
        for (int i = 0; i < childNum; i++)
        {
            childObjects[i].SetActive(enabled);
        }
        //defeatedEnemyCnt = 0;
        //notDefeatedEnemyCnt = 0;
    }
    void Start()
    {
        remainingEnemiesCnt = childNum;
    }
    void Update()
    {
        if (defeatedEnemyCnt + notDefeatedEnemyCnt == childNum)
        {
            notDefeatedEnemyCnt = 0;
            defeatedEnemyCnt = 0;
            remainingEnemiesCnt = childNum;
			eCreate.SetBaculaDestroy(true);
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
            //gameObject.SetActive(false);

            //isDead = true;
            //Died_Process();
        }

    }
}
