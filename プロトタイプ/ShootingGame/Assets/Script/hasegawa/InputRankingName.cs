/*
 * 20190814 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ランキングの名前入力用
/// </summary>
[System.Serializable]
public class InputRankingName
{
	const int kNameLength = 3;				// 名前の長さ
	char[] name = new char[kNameLength];	// 入力用文字配列

	[SerializeField] private AudioSource Move;			//文字切り替え時
	[SerializeField] private AudioSource Decition;      //文字の決定時
	[SerializeField] private AudioSource Return_Sound;			//文字の一つ前に戻るとき
	// 名前をストリングで返すプロパティ
	public string Name
	{
		get
		{
			string result = "";
			for (int i = 0; i < kNameLength; ++i)
			{
				result += name[i];
			}
			return result;
		}
	}
	List<Image> nameImageList = new List<Image>(kNameLength);	// 名前を表示するImageのリスト
	// 名前を表示するImageを格納するプロパティ
	public List<Image> NameImageList
	{
		set
		{
			int i;
			// リストの容量分格納する
			for (i = 0; i < nameImageList.Count; ++i)
			{
				nameImageList[i] = value[i];
			}
			// もし名前の文字数に足りなければ追加する
			while (nameImageList.Count < kNameLength)
			{
				nameImageList.Add(value[i]);
				++i;
			}
		}
	}
	int selectPos = 0;													// 文字の入力位置
	float previousInputY = 0f;											// 前フレームの入力情報
	const int kBlinkInvisibleFrame = 35;								// 非表示を開始するフレーム数
	const int kBlinkFrameMax = 45;										// 点滅の一回にループするフレーム数
	int blinkFrame = 0;													// 点滅させるためのフレーム数
	string selectAxisName = "Vertical";									// 選択に使用する入力軸の名前
	string decisionButtonName = "Fire1";								// 決定するボタンの名前
	string cancelButtonName = "Fire2";									// ひとつ前に戻るボタンの名前
	KeyCode decisionKeyCode = KeyCode.None;								// 決定するキーの名前
	KeyCode cancelKeyCode = KeyCode.None;								// ひとつ前に戻るボタンの名前
	public bool IsDecision { get { return selectPos >= kNameLength; } }	// 決定されたかどうか
	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="selectAxisName">選択に使用する入力軸の名前</param>
	/// <param name="defaultName">名前の規定値</param>
	public InputRankingName(string selectAxisName, string defaultName = "UFO")
	{
		for (int i = 0; i < kNameLength; ++i)
		{
			name[i] = defaultName[i];
		}
		this.selectAxisName = selectAxisName;
	}
	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="selectAxisName">選択に使用する入力軸の名前</param>
	/// <param name="decisionButtonName">決定するボタンの名前</param>
	/// <param name="cancelButtonName">ひとつ前に戻るボタンの名前</param>
	/// <param name="defaultName">名前の規定値</param>
	public InputRankingName(string selectAxisName, string decisionButtonName, string cancelButtonName, string defaultName = "UFO")
	{
		for (int i = 0; i < kNameLength; ++i)
		{
			name[i] = defaultName[i];
		}
		this.selectAxisName = selectAxisName;
		this.decisionButtonName = decisionButtonName;
		this.cancelButtonName = cancelButtonName;
	}
	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="selectAxisName">選択に使用する入力軸の名前</param>
	/// <param name="decisionKeyCode">決定するキーの名前</param>
	/// <param name="cancelKeyCode">ひとつ前に戻るキーの名前</param>
	/// <param name="defaultName">名前の規定値</param>
	public InputRankingName(string selectAxisName, KeyCode decisionKeyCode, KeyCode cancelKeyCode, string defaultName = "UFO")
	{
		for (int i = 0; i < kNameLength; ++i)
		{
			name[i] = defaultName[i];
		}
		this.selectAxisName = selectAxisName;
		this.decisionKeyCode = decisionKeyCode;
		this.cancelKeyCode = cancelKeyCode;
	}
	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="selectAxisName">選択に使用する入力軸の名前</param>
	/// <param name="decisionButtonName">決定するボタンの名前</param>
	/// <param name="decisionKeyCode">決定するキーの名前</param>
	/// <param name="cancelButtonName">ひとつ前に戻るボタンの名前</param>
	/// <param name="cancelKeyCode">ひとつ前に戻るキーの名前</param>
	/// <param name="defaultName">名前の規定値</param>
	public InputRankingName(string selectAxisName, string decisionButtonName, KeyCode decisionKeyCode, string cancelButtonName, KeyCode cancelKeyCode, string defaultName = "UFO")
	{
		for (int i = 0; i < kNameLength; ++i)
		{
			name[i] = defaultName[i];
		}
		this.selectAxisName = selectAxisName;
		this.decisionButtonName = decisionButtonName;
		this.decisionKeyCode = decisionKeyCode;
		this.cancelButtonName = cancelButtonName;
		this.cancelKeyCode = cancelKeyCode;
	}

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="selectAxisName">選択に使用する入力軸の名前</param>
	/// <param name="defaultName">名前の規定値</param>
	public void Init(string selectAxisName, string defaultName = "UFO")
	{
		nameImageList = new List<Image>(kNameLength);
		name = new char[kNameLength];
		for (int i = 0; i < kNameLength; ++i)
		{
			name[i] = defaultName[i];
		}
		this.selectAxisName = selectAxisName;
	}

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="selectAxisName">選択に使用する入力軸の名前</param>
	/// <param name="decisionButtonName">決定するボタンの名前</param>
	/// <param name="cancelButtonName">ひとつ前に戻るボタンの名前</param>
	/// <param name="defaultName">名前の規定値</param>
	public void Init(string selectAxisName, string decisionButtonName, string cancelButtonName, string defaultName = "UFO")
	{
		nameImageList = new List<Image>(kNameLength);
		name = new char[kNameLength];
		for (int i = 0; i < kNameLength; ++i)
		{
			name[i] = defaultName[i];
		}
		this.selectAxisName = selectAxisName;
		this.decisionButtonName = decisionButtonName;
		this.cancelButtonName = cancelButtonName;
	}

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="selectAxisName">選択に使用する入力軸の名前</param>
	/// <param name="decisionKeyCode">決定するキーの名前</param>
	/// <param name="cancelKeyCode">ひとつ前に戻るキーの名前</param>
	/// <param name="defaultName">名前の規定値</param>
	public void Init(string selectAxisName, KeyCode decisionKeyCode, KeyCode cancelKeyCode, string defaultName = "UFO")
	{
		nameImageList = new List<Image>(kNameLength);
		name = new char[kNameLength];
		for (int i = 0; i < kNameLength; ++i)
		{
			name[i] = defaultName[i];
		}
		this.selectAxisName = selectAxisName;
		this.decisionKeyCode = decisionKeyCode;
		this.cancelKeyCode = cancelKeyCode;
	}

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="selectAxisName">選択に使用する入力軸の名前</param>
	/// <param name="decisionButtonName">決定するボタンの名前</param>
	/// <param name="decisionKeyCode">決定するキーの名前</param>
	/// <param name="cancelButtonName">ひとつ前に戻るボタンの名前</param>
	/// <param name="cancelKeyCode">ひとつ前に戻るキーの名前</param>
	/// <param name="defaultName">名前の規定値</param>
	public void Init(string selectAxisName, string decisionButtonName, KeyCode decisionKeyCode, string cancelButtonName, KeyCode cancelKeyCode, string defaultName = "UFO")
	{
		nameImageList = new List<Image>(kNameLength);
		name = new char[kNameLength];
		for (int i = 0; i < kNameLength; ++i)
		{
			name[i] = defaultName[i];
		}
		this.selectAxisName = selectAxisName;
		this.decisionButtonName = decisionButtonName;
		this.decisionKeyCode = decisionKeyCode;
		this.cancelButtonName = cancelButtonName;
		this.cancelKeyCode = cancelKeyCode;
	}

	/// <summary>
	/// 呼び出されている間、名前を変更できる
	/// </summary>
	public void SelectingName()
	{
		// 選択位置の移動
		MoveSelectPos();
		// 文字の選択
		SelectChar();
		// 選択されている文字を点滅させる
		BlinkSelectChar();
	}
	/// <summary>
	/// 呼び出している間、選択位置を移動できる
	/// </summary>
	void MoveSelectPos()
	{
		// 決定されていたら処理しない
		if (IsDecision) { return; }
		// 選択されている鵜文字が消えないようにアクティブにする
		nameImageList[selectPos].enabled = true;
		// 文字の選択位置を変える
		if (Input.GetButtonDown(decisionButtonName) || Input.GetKeyDown(decisionKeyCode))
		{
			++selectPos;
			if (Decition.isPlaying) Decition.Stop();
			Decition.Play();
		}
		if (Input.GetKeyDown(cancelKeyCode) || Input.GetButtonDown(cancelButtonName))
		{
			--selectPos;
			if (Return_Sound.isPlaying) Return_Sound.Stop();
			Return_Sound.Play();
		}
		// 要素数が文字数より大きくならないように補正する
		if (selectPos > kNameLength)
		{
			selectPos = kNameLength;
		}
		if (selectPos < 0)
		{
			selectPos = 0;
		}
	}
	/// <summary>
	/// 呼び出している間、選択している文字を変更できる
	/// </summary>
	void SelectChar()
	{
		// 選択範囲外であれば処理しない
		if (selectPos >= kNameLength) { return; }
		float inputY = Input.GetAxisRaw(selectAxisName);
		// 前フレームも入力していたら移動しない
		if (previousInputY != 0f) { previousInputY = inputY; return; }
		// 元の文字を保存しておく
		char previousChar = name[selectPos];
		// 選択している部分の文字を変える
		if (inputY > 0f)
		{
			++name[selectPos];
			if (Move.isPlaying) Move.Stop();
			Move.Play();

		}
		if (inputY < 0f)
		{
			--name[selectPos];
			if (Move.isPlaying) Move.Stop();
			Move.Play();
		}
		// 規定文字以内に収める
		if (name[selectPos] > 'Z')
		{
			name[selectPos] = 'A';
		}
		if (name[selectPos] < 'A')
		{
			name[selectPos] = 'Z';
		}
		previousInputY = inputY;
	}
	/// <summary>
	/// 選択している文字を点滅させる
	/// </summary>
	void BlinkSelectChar()
	{
		// 選択範囲外であれば処理しない
		if (selectPos >= kNameLength) { return; }
		nameImageList[selectPos].enabled = blinkFrame < kBlinkInvisibleFrame;
		++blinkFrame;
		if (blinkFrame > kBlinkFrameMax)
		{
			blinkFrame = 0;
		}
	}
	/// <summary>
	/// 選択位置をリセットする
	/// </summary>
	public void InitSelectPosition()
	{
		selectPos = 0;
	}
}
