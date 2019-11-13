/*
 * 20190909 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 入力するボタンの名前を保持しておく
/// </summary>
[System.Serializable]
public class InputManager
{
	[SerializeField, Tooltip("Unity側で設定するボタン名のリスト")] List<string> defaultButtonNameList = new List<string>();	// uinty側で設定するボタンの名前のリスト
	[SerializeField, Tooltip("実際に使用する名前のリスト")] List<string> useButtonNameList = new List<string>();				// スクリプト側で使用するボタン名のリスト
	[SerializeField, Tooltip("確定させるまでの時間")] float decisionTime = 5f;												// 決定するまでの時間
	float inputTime;																									// 入力を受けている時間
	Dictionary<string, string> reflectButtonNameMap = new Dictionary<string, string>();									// スクリプト側に渡すボタンの名前
	Dictionary<string, string> settingButtonNameMap = new Dictionary<string, string>();									// ボタンの再設定をするときの一時変数
	int settingButtonNum = 0;																							// 設定しているボタンの要素番号
	string previousInputButtonName = "";																				// 前フレームに入力を受けていたボタンの名前
	public Dictionary<string, string> Button { get { return reflectButtonNameMap; } }
	[SerializeField, Tooltip("設定時に表示するフォント")] Font textFont;													// 設定時に表示するテキストのフォント
	[SerializeField, Tooltip("表示するフォントのX座標")] float textPositionX = 0;											// 表示するテキストのx座標
	Text inputInfoText;
	/// <summary>
	/// コンストラクタ
	/// </summary>
	public InputManager()
	{
		if (defaultButtonNameList.Count < useButtonNameList.Count) { Debug.LogWarning("UseButtonListCount is more than DefaultButtonNameList.Count!"); }
		for (int i = 0; i < useButtonNameList.Count && i < defaultButtonNameList.Count; ++i)
		{
			reflectButtonNameMap.Add(useButtonNameList[i], defaultButtonNameList[i]);
			settingButtonNameMap.Add(useButtonNameList[i], "");
		}
	}
	/// <summary>
	/// 初期化
	/// </summary>
	public void Init()
	{
		if (defaultButtonNameList.Count < useButtonNameList.Count) { Debug.LogWarning("UseButtonListCount is more than DefaultButtonNameList.Count!"); }
		for (int i = 0; i < useButtonNameList.Count && i < defaultButtonNameList.Count; ++i)
		{
			reflectButtonNameMap.Add(useButtonNameList[i], defaultButtonNameList[i]);
			settingButtonNameMap.Add(useButtonNameList[i], "");
		}
	}
	/// <summary>
	/// 呼び出されている間、ボタンの再設定を行う
	/// </summary>
	/// <returns>再設定がすべて終わったかどうか</returns>
	public bool SettingButton()
	{
		// テキスト生成
		if (!inputInfoText)
		{
			Canvas anyCanvas = GameObject.FindObjectOfType<Canvas>();
			inputInfoText = new GameObject("InputInfo").AddComponent<Text>();
			inputInfoText.rectTransform.SetParent(anyCanvas.transform);
			inputInfoText.rectTransform.localPosition = new Vector2(textPositionX, 0f);
			inputInfoText.font = textFont;
			inputInfoText.fontSize = 50;
			inputInfoText.rectTransform.sizeDelta = new Vector2(3840f, 1080f);
		}
		bool isComplete = false;
		bool isInput = false;
		string inputButtonName = "";
		// 設定されたボタンをそれぞれ確認していく
		for (int i = 0; i < defaultButtonNameList.Count; ++i)
		{
			// すでに入力を受け付けているものはスキップする
			if (settingButtonNameMap.ContainsValue(defaultButtonNameList[i]))
			{
				continue;
			}
			// 入力を受けていたら名前を一時保存する
			if (Input.GetButton(defaultButtonNameList[i]) && !isInput)
			{
				inputButtonName = defaultButtonNameList[i];
				isInput = true;
			}
			// 同時押しされていたら解除
			else if (Input.GetButton(defaultButtonNameList[i]) && isInput)
			{
				inputButtonName = "";
				break;
			}
			// ボタンが上げられたらスキップする
			else if (Input.GetButtonUp(defaultButtonNameList[i]))
			{
				inputButtonName = "";
				continue;
			}
		}
		// 一つのボタンが押されていたら入力時間をカウントする
		if (previousInputButtonName == inputButtonName && inputButtonName != "")
		{
			inputTime += Time.deltaTime;
		}
		// そうでない時は時間リセット
		else
		{
			inputTime = 0f;
		}
		// 決定される時間になったら決定して次のボタンに移行する
		if (inputTime >= decisionTime)
		{
			settingButtonNameMap[useButtonNameList[settingButtonNum]] = inputButtonName;
			++settingButtonNum;
		}
		previousInputButtonName = inputButtonName;
		// 全て設定されたら反映と初期化をしてメソッドをtrueで返す
		if (settingButtonNum >= useButtonNameList.Count)
		{
			// ボタンの名前を設定
			foreach (string buttonName in useButtonNameList)
			{
				reflectButtonNameMap[buttonName] = settingButtonNameMap[buttonName];
			}
			// 設定用のマップを初期化
			for (int i = 0; i < useButtonNameList.Count; ++i)
			{
				settingButtonNameMap[useButtonNameList[i]] = "";
			}
			isComplete = true;
			previousInputButtonName = "";
			settingButtonNum = 0;
			GameObject.Destroy(inputInfoText);
		}
		// テキスト表示
		inputInfoText.text = "Setting Now";
		for (int i = 0; i < useButtonNameList.Count; ++i)
		{
			// ボタン名の情報
			inputInfoText.text += "\n" + useButtonNameList[i] + " : ";
			// 入力状態の情報
			if (settingButtonNum == i)
			{
				inputInfoText.text += "Setting";
			}
			else if (settingButtonNameMap[useButtonNameList[i]] == "")
			{
				inputInfoText.text += "Not set";
			}
			else if (settingButtonNameMap[useButtonNameList[i]] != "")
			{
				inputInfoText.text += "Complete";
			}
		}
		return isComplete;
	}
}
