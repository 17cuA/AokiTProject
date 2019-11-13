using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
	[Header("アプリ落とすキー")]
	[SerializeField] private KeyCode shutDownKey = KeyCode.F1;
	[Header("当たり判定を外すキー")]
	[SerializeField] private KeyCode switchCollidersKey = KeyCode.F3;
	[SerializeField] private bool isColliderEnabled = true;
	[Header("入力DEBUGキー")]
	[SerializeField] private KeyCode playersOperationDebugKey = KeyCode.F4;
	private static bool isPlayersOperationDebugging = false;

    [SerializeField] private KeyCode AddRemainingKey = KeyCode.F7;

    [Header("復活キー")]
    [SerializeField] private KeyCode ResponPlayerKey = KeyCode.F8;
    [SerializeField] private int bonusRemaining = 2;

	private GameObject UIChild;
	private static Text debugText;
	private static ScrollRect scrollRect;
	private Dictionary<string, int> infos = new Dictionary<string, int>();
	[Header("何秒ごとにデータをクリアする")]
	[SerializeField] private float updateTime = 2;
	private float updateTimer;


	private void Awake()
	{
		scrollRect = GetComponentInChildren<ScrollRect>();
		debugText = GetComponentInChildren<Text>();
		UIChild = transform.GetChild(0).gameObject;

		if(!isPlayersOperationDebugging)
		{
			UIChild.SetActive(false);
		}	
	}

	// Update is called once per frame
	void Update()
	{
		//アプリ落とすショートカット(F1)
		if (Input.GetKeyDown(shutDownKey))
		{
			OperationDebug("ゲーム強制終了", "GM");
			Application.Quit();
		}
		//コライダー有効化/無効かのショートカット(F3)
		if (Input.GetKeyDown(switchCollidersKey))
		{
			if (isColliderEnabled)
			{
				OperationDebug("全部のコライダーを外した","GM");
				isColliderEnabled = false;
				var player1 = Obj_Storage.Storage_Data.GetPlayer();
                var player2 = Obj_Storage.Storage_Data.GetPlayer2();

			}
			else
			{
				OperationDebug("外したのコライダーを元に戻した", "GM");
				isColliderEnabled = true;
				var player1 = Obj_Storage.Storage_Data.GetPlayer();
                var player2 = Obj_Storage.Storage_Data.GetPlayer2();

			}
		}
		//プレイヤー操作のショートカット(F4)
		if (Input.GetKeyDown(playersOperationDebugKey))
		{
			if (isPlayersOperationDebugging)
			{
				isPlayersOperationDebugging = false;
				if (UIChild.activeSelf)
					UIChild.SetActive(false);

				Application.logMessageReceived -= HandleLog;
			}
			else
			{
				isPlayersOperationDebugging = true;
				if (!UIChild.activeSelf)
					UIChild.SetActive(true);

				Application.logMessageReceived += HandleLog;
			}
		}

        if(Input.GetKeyDown(AddRemainingKey))
        {
            var player1 = Obj_Storage.Storage_Data.GetPlayer();
            var player2 = Obj_Storage.Storage_Data.GetPlayer2();

            player1.GetComponent<Player1>().Remaining = 9;
            player2.GetComponent<Player2>().Remaining = 9;
        }

        if(Input.GetKeyDown(ResponPlayerKey))
        {
            if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
                return;

            var player1 = Obj_Storage.Storage_Data.GetPlayer();
            var player2 = Obj_Storage.Storage_Data.GetPlayer2();

            if(player1.activeSelf && !player2.activeSelf)
            {
                player2.SetActive(true);
                player2.GetComponent<Player2>().ResponPreparation(bonusRemaining);

            }
            else if(player2.activeSelf && !player1.activeSelf)
            {
                player1.SetActive(true);
                player1.GetComponent<Player1>().ResponPreparation(bonusRemaining);
            }
        }

		updateTimer += Time.deltaTime;
		if(updateTimer >= updateTime)
		{
			updateTimer = 0;
			infos.Clear();
		}
	}

	/// <summary>
	/// 第一引数は表示内容
	/// 第二引数は代表者名
	/// </summary>
	/// <param name="context"></param>
	/// <param name="userName"></param>
	public static void OperationDebug(string context, string userName)
	{
		if (isPlayersOperationDebugging == false)
			return;

        if(SceneManager.GetActiveScene().name != "Stage_01")
            return;

		string addText = "\n " + userName + ": " + context;
		debugText.text += addText;
		Canvas.ForceUpdateCanvases();    
		scrollRect.verticalNormalizedPosition = 0f; 
		Canvas.ForceUpdateCanvases();  
	}

	private void HandleLog(string content, string stackTrace,LogType type)
	{
		var outPut = content;
		var stack = stackTrace;

		if(!infos.ContainsKey(outPut))
		{
			infos.Add(outPut, 1);
		}
		else
		{
			if(infos[outPut] < 1)
			{
				infos[outPut]++;
			}
			else
			{
				return;
			}
		}

		if(type == LogType.Error)
		{
			outPut = "<color=#A90404FF>" + outPut + "</color>";
			stack = "<color=#A90404FF>" + stack + "</color>";
			OperationDebug(outPut, "Error");
		}
		else if (type == LogType.Exception)
		{
			outPut = "<color=#FFD800FF>" + outPut + "</color>";
			stack = "<color=#FFD800FF>" + stack + "</color>";
			OperationDebug(outPut, "Exception");
		}
	}

}

