//久保田 達己
//無線信号の文字表示ようスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Wireless_sinario : MonoBehaviour
{
	//文字の表示に使っている変数たち--------------------------------------------------------
	[SerializeField] private string[] scenarios;          // 無線セリフ、上から順に基本流れていく次のセリフにいく(unity側の設定)
	public string[] Curtain_up;
	public string[] First_half_boss_before;
	public string[] First_falf_boss_after;
	public string[] Moai_before;			//モアイのシナリオ
	public string[] Second_half_boss_before;
	public string[] Second_half_boss_after;

	[SerializeField] Text uiText;                   //uitextへの参照
	[SerializeField]
	[Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;             //１文字の表示にかかる時間
	private string currentText = string.Empty;           //現在の文字列
	private float timeUntilDisplay = 0;                   //表示にかかる時間
	private float timeElapsed = 1;                         //文字列の表示を開始した時間
	public int currentLine = 0;                      //現在の行番号
	private int lastUpdateCharacter = -1;       //表示中の文字数
	private int VoiceNo;                    //無線の声の情報を取得数するための要素数として使用
											//-------------------------------------------------------------------------------
	public int frame = 0;                   // フレーム管理するためのフレームカウント用の変数
	public bool Is_Display;               //Onになったら文章表示

	public static bool Is_using_wireless;       //外部scriptから変更するためにつかう
												//-------------------------------------------------------------------------------
	public enum Sinario_No
	{
		Curtain_up,                         //開戦時
		First_half_boss_before,             //前半ボス前
		First_falf_boss_after,              //前半ボス後
		Moai,								//モアイ
		Second_half_boss_before,            //後半ボス前
		Second_half_boss_after              //後半ボス後
	}
	public int No;          //どの無線の状態なのか
							// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText
	{
		get { return Time.time > timeElapsed + timeUntilDisplay; }
	}

	public bool isShowOver = false;     //表示が終了したかどうか（明示的に示すため）
	private float unShowTimer;              //
	[SerializeField] private float unShowTime;

	private int first_start;            //ゲーム開始時からカウントするためのもの
	private Color color;        //文字の色を保存しておくようの変数
	private Color outline;  //テキストの文字のアウトラインを変更する用の変数
	private Outline outline2;

	private int frameMax;

	private bool Is_Start_Wireless;     //無線が始まるまでの判定用
	private bool Is_Finish_Wireless;        //無線が終わったかどうか(）
	public AudioSource audiosource;         //無線受信時の音などを鳴らすよう
	private int soundcnt;
	int Start_cnt;
	void Start()
	{
		Game_Master.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.WIRELESS;
		outline2 = GetComponent<Outline>();
		Is_Display = false;
		frame = 0;
		first_start = 0;
		No = 0;
		uiText.text = "";
		color = uiText.color;
		outline = outline2.effectColor;
		SetNext_sinario();
		frameMax = 180;
		soundcnt = 0;
		Start_cnt = 0;
	}

	void Update()
	{
		//ゲーム内のモードが無線状態の時
		if (Game_Master.Management_In_Stage == Game_Master.CONFIGURATION_IN_STAGE.WIRELESS)
		{
			uiText.color = color;
			outline2.effectColor = outline;
			Worddisplay();
		}
		else
		{
			uiText.color = Color.clear;
			outline2.effectColor = Color.clear;
			//if (outline.IsActive()) outline.enabled = false;
		}
		if (/*Input.GetKeyDown(KeyCode.Space) || */Is_using_wireless)
		{
			Game_Master.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.WIRELESS;
			first_start = 0;
			frame = (int)(frameMax * 0.67f);

			Is_using_wireless = false;
		}

	}
	//文字の表示
	void Worddisplay()
	{
		//プレイヤーのアニメーションの行動が終わるまで飛ばす-----------------
		first_start++;
		//受信時の音(初回のみ)
		if (soundcnt == 0 && first_start > 180 && No == 0)
		{
			Sound_Active();
			soundcnt = 1;
		}
		//受信時の音（２回目以降）
		else if (soundcnt == 0 && first_start > 45 && No != 0)
		{
			Sound_Active();
			soundcnt = 1;
		}
		if (first_start < 120)
		{
			return;
		}
		//-------------------------------------------------------------------------------
		if (isShowOver)
		{
			//既定のシナリオまでだしたら
			if (Time.time >= unShowTimer)
			{
				//無線のモードから通常のモードに治す
				if (currentLine >= 2)
				{
					No++;
					SetNext_sinario();
					currentLine = 0;
					frame = 0;
					Game_Master.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.eNORMAL;
					uiText.text = "";
					Sound_Active();
					soundcnt = 0;
					Start_cnt = 0;
				}
				isShowOver = false;
			}
			//デバック用
			if (Input.GetKeyDown(KeyCode.Alpha0))
			{
				No++;
				SetNext_sinario();
				currentLine = 0;
				frame = 0;
				Voice_Manager.VOICE_Obj.Sinario_Stop();
				Game_Master.Management_In_Stage = Game_Master.CONFIGURATION_IN_STAGE.eNORMAL;
				uiText.text = "";
				Sound_Active();
				soundcnt = 0;
				Start_cnt = 0;
			}
		}
		else
		{
			if (soundcnt == 1)
			{
				Sound_Active();
			}
			frame++;
			Start_cnt++;
			// 文字の表示が完了してるならクリック時に次の行を表示する
			if (IsCompleteDisplayText)
			{
				if (currentLine == 2)
				{
					isShowOver = true;
					unShowTimer = Time.time + unShowTime;
				}
				if (currentLine < scenarios.Length && frame > frameMax || Input.GetKeyDown(KeyCode.Alpha0) /*|| Input.GetButtonDown("P2_Fire1")*/)
				{
					frame = 0;
					SetNextLine();
				}
			}
			else
			{
				if (currentLine > 0)
				{
					if (No == 0)
					{
						//開戦時
						Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.audio_voice[0]);
					}
					//各配列に対応したように鳴らす
					switch (No)
					{
						case 1:
							//前半ボス前
							Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.audio_voice[1]);
							break;
						case 2:
							//前半ボス後
							if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.audio_voice[22]);
							else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.audio_voice[23]);
							break;
						case 3:
							//モアイの音声
							if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.audio_voice[20]);
							else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.audio_voice[21]);
							break;
						case 4:
							//後半ボス前
							if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.audio_voice[4]);
							else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.audio_voice[5]);
							break;
						case 5:
							//後半ボス後
							if (currentLine == 1) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.audio_voice[6]);
							else if (currentLine == 2) Voice_Manager.VOICE_Obj.Sinario_Active(Obj_Storage.Storage_Data.audio_voice[7]);
							break;

					}
				}
				if (Input.GetKeyDown(KeyCode.Alpha0) /*|| Input.GetButtonDown("P2_Fire1")*/)
				{
					// 完了してないなら文字をすべて表示する
					timeUntilDisplay = 0;
				}
			}
		}
		//経過した　時間が想定表示時間の何％か確認し、表示文字数を出す。
		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		//表示文字数が前回の表示文字数と異なるならテキストを更新する。
		if (displayCharacterCount != lastUpdateCharacter)
		{
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}
	//次に表示する文字を確認
	void SetNextLine()
	{
		if (currentLine < scenarios.Length)
		{
			currentText = scenarios[currentLine];
		}
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		currentLine++;
		lastUpdateCharacter = -1;
	}

	//どの行を表示するのかを合わせる関数
	void Linesmatch(int i, int m)
	{
		currentLine = i;
		lastUpdateCharacter = -1;
	}
	//表示するシナリオを変更する
	void SetNext_sinario()
	{
		//ゲーム開始から幾つめの文章を出すのか
		switch (No)
		{
			case 0:
				//開戦時
				scenarios = Curtain_up;
				frameMax = 180;
				break;
			case 1:
				//前半ボス前
				scenarios = First_half_boss_before;
				frameMax = 240;
				unShowTime = 4f;
				break;
			case 2:
				//前半ボス後
				scenarios = First_falf_boss_after;
				frameMax = 240;
				unShowTime = 5f;
				break;
			case 3:
				//モアイ
				scenarios = Moai_before;
				frameMax = 240;
				unShowTime = 5.5f;

				break;
			case 4:
				//後半ボス前
				scenarios = Second_half_boss_before;
				frameMax = 240;
				break;
			case 5:
				//後半ボス後
				scenarios = Second_half_boss_after;
				frameMax = 240;
				break;
			default:
				break;
		}
	}
	void Sound_Active()
	{
		switch (soundcnt)
		{
			//無線開始時
			case 0:
				audiosource.PlayOneShot(Obj_Storage.Storage_Data.audio_se[23]);


				//次のサウンドの準備
				audiosource.clip = Obj_Storage.Storage_Data.audio_se[24];
				audiosource.loop = true;
				break;
			//無線中 & 文字表示中
			case 1:
				audiosource.Play();
				soundcnt = 2;
				//一回だけ
				audiosource.PlayOneShot(Obj_Storage.Storage_Data.audio_se[24]);
				break;
			//無線終了時
			case 2:
				audiosource.Stop();
				//一回だけ
				audiosource.PlayOneShot(Obj_Storage.Storage_Data.audio_se[25]);
				break;
		}
	}
}
