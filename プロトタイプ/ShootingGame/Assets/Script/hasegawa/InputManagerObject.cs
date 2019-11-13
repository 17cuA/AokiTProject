/*
 * 20190910 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// InputManagerをいい感じにできなかったため作成
/// </summary>
public class InputManagerObject : MonoBehaviour
{
	[SerializeField, Tooltip("入力情報")] InputManager inputManager = new InputManager();
	[SerializeField, Tooltip("設定状態にするキー")] KeyCode settingKey = KeyCode.F5;
	public InputManager Manager { get { return inputManager; } }
	/// <summary>
	/// 入力の設定を行っている最中かどうか
	/// </summary>
	public bool IsInputSetting { get; private set; }
	[SerializeField] Vector2 debugAreaPosition;
	DemoMovieControl demoMovieControl;

	void Start()
	{
		bool findSame = false;
		InputManagerObject[] temps = FindObjectsOfType<InputManagerObject>();
		for (int i = 0; i < temps.Length; ++i)
		{
			if (temps[i].name == gameObject.name && !findSame)
			{
				findSame = true;
			}
			else if (temps[i].name == gameObject.name && findSame)
			{
				Destroy(gameObject);
				return;
			}
		}
		inputManager.Init();
		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
		if (SceneManager.GetActiveScene().name != "Title") { return; }
		if (!demoMovieControl) { demoMovieControl = FindObjectOfType<DemoMovieControl>(); }
		if (Input.GetKeyDown(settingKey))
		{
			IsInputSetting = true;
			demoMovieControl.IsStopDemoMovie = false;
		}
		if (IsInputSetting)
		{
			IsInputSetting = !inputManager.SettingButton();
			demoMovieControl.IsStopDemoMovie = IsInputSetting;
		}
	}
	//void OnGUI()
	//{
	//	if (IsInputSetting) { return; }
	//	string displayText = "";
	//	Rect displayAreaSize = new Rect(debugAreaPosition.x, debugAreaPosition.y, 500f, 0f);
	//	foreach(string key in inputManager.Button.Keys)
	//	{
	//		if (Input.GetButton(inputManager.Button[key]))
	//		{
	//			displayText += "Input " + key + "\n";
	//			displayAreaSize.height += 60f;
	//		}
	//	}
	//	if (displayText == "") { return; }
	//	GUI.TextField(displayAreaSize, displayText);
	//	GUI.skin.textField.fontSize = 50;
	//}
}
