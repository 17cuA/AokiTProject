using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Debug_Log : MonoBehaviour
{
	//実際に表示するテキスト
	private Text Log;　

	[SerializeField,Tooltip("新しいログがUI範囲内に収まるようにテキストを調整する(Truncate限定)")]
	private bool viewInRect = true;

	private void Awake()
	{
		//アタッチされているテキストの取得
		Log = this.GetComponent<Text>();
		//もしアタッチされていなかったら
		if (Log == null)
			//No text component foundをNullReferenceExceptionの後に書き出す
			throw new System.NullReferenceException("No text component found.");
		else
			print("OK");
	}

	private void OnEnable()
	{
		Application.logMessageReceived += HandleLog;
	}

	private void OnDisable()
	{
		Application.logMessageReceived -= HandleLog;
	}

	private void HandleLog(string logText, string stackTrace, LogType logType)
	{
		if (string.IsNullOrEmpty(logText))
			return;

		//テキストに記入していく
		//新しく記入されたら改行をする
		Log.text += logText + System.Environment.NewLine;

		if (viewInRect && Log.verticalOverflow == VerticalWrapMode.Truncate)
			AdjustText(Log);
	}

	/// <summary>
	/// Textの範囲内に文字列を収める
	/// </summary>
	/// <param name="t">実際に書かれるテキスト</param>
	private void AdjustText(Text t)
	{
		TextGenerator generator = t.cachedTextGenerator;
		var settings = t.GetGenerationSettings(t.rectTransform.rect.size);
		generator.Populate(t.text, settings);

		int countVisible = generator.characterCountVisible;
		if (countVisible == 0 || t.text.Length <= countVisible)
			return;

		int truncatedCount = t.text.Length - countVisible;
		var lines = t.text.Split('\n');
		foreach (string line in lines)
		{
			// 見切れている文字数が0になるまで、テキストの先頭行から消してゆく
			t.text = t.text.Remove(0, line.Length + 1);
			truncatedCount -= (line.Length + 1);
			if (truncatedCount <= 0)
				break;
		}
	}
}
