//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/20
//----------------------------------------------------------------------------------------------
// 文字の表示
//----------------------------------------------------------------------------------------------
// 2019/05/20：0～9,A～Zのフォントを使わない字の表示
// 2019/05/23：文字数の固定、固定された文字数以上、未満はエラー
// 2019/05/24：文字の大きさ変更可能
// 2019/05/24：文字の色変え
// 2019/05/24：文字の完全削除
// 2019/05/24：オブジェクトの親子関係の修正
// 2019/05/27：表示非表示の選択追加
// 2019/07/19：文字のポジション中央揃え化
// 2019/08/13：文字の位置変更
//----------------------------------------------------------------------------------------------
//
// 私を読んで
//
// 画像準備 編
// 1.デザイナーにこの形で画像データを並べて作ってもらって！
//	 ┏━┳━┳━┳━┳━┳━┳━┳━┳━┳━┓
//	 ┃０┃１┃２┃３┃４┃５┃６┃７┃８┃９┃
//	 ┣━╋━╋━╋━╋━╋━╋━╋━╋━╋━┫
//	 ┃Ａ┃Ｂ┃Ｃ┃Ｄ┃Ｅ┃Ｆ┃Ｇ┃Ｈ┃Ｉ┃Ｊ┃
//	 ┣━╋━╋━╋━╋━╋━╋━╋━╋━╋━┫
//	 ┃Ｋ┃Ｌ┃Ｍ┃Ｎ┃Ｏ┃Ｐ┃Ｑ┃Ｒ┃Ｓ┃Ｔ┃
//	 ┣━╋━╋━╋━╋━╋━╋━╋━╋━╋━┫
//	 ┃Ｕ┃Ｖ┃Ｗ┃Ｘ┃Ｙ┃Ｚ┃  ┃  ┃  ┃  ┃
//	 ┗━┻━┻━┻━┻━┻━┻━┻━┻━┻━┛
// 
// 2.読み込みたい画像の Inspector にある Texture Type を Sprite(2D and UI)に変更
// 
// 3.いつも通り Sprite Editor で任意の大きさに画像を切る
//
// 3.5.画像データは Asset の中の Resource の中に保存
//
// プログラム 編
// 4.Characters_Count に文字数指定
//
// 5.Look_Load に文字ファイルのパス入力
//
// 6.Character_Preference に表示したい文字の入力
//	 (Characters_Count に入力した文字数と同じにしないとエラー)
//
// 以上使いかた

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TextDisplay
{
	public class Character_Display
	{
        private GameObject controler_obj;		// まとめようオブジェクト

        public int Word_Count { set; get; }									// 文字数の保存場所
		public string Resource_Path { private set; get; }					// 画像のパス
        public Color Font_Color {private set; get;}                         // 文字の色
		public List<GameObject> Character_Object { private set; get; }		// 表示用の一文字分の object
		public List<Image> Display_Characters { private set; get; }			// 表示用の一文字分の Image コンポーネント
        public bool Enable {private set; get;}                              // 表示するかしないか
		private Sprite[] Look { set; get; }                                 // PNGでできたフォントデータの保存場所

		/// <summary>
		/// コンストラクタコンストラクタ
		/// </summary>
		/// <param name="n"> 文字数 </param>
		/// <param name="s"> 文字用画像の保存場所 </param>
		/// <param name="v"> 文字の表示場所 </param>
		public Character_Display(int n, string s, GameObject t, Vector3 v)
		{
			Word_Count = n;
			SetControler(t);
            controler_obj.transform.localPosition = v;
            Character_Object = new List<GameObject>();
			Display_Characters = new List<Image>();
            Font_Color = new Color(1.0f,1.0f,1.0f);
			if (s != "")
			{
				Look_Load(s);
			}
		}

		/// <summary>
		/// 見た目のロード
		/// </summary>
		/// <param name="resource_path"> リソース内の Sprite のパス </param>
		public void Look_Load(string path)
		{
			Resource_Path = path;
			Look = Resources.LoadAll<Sprite>(Resource_Path);
		}

		/// <summary>
		/// 文字の設定
		/// </summary>
		/// <param name="s"> 表示する文字列 </param>
		public void Character_Preference(string s)
		{
			if (s.Length == Word_Count)
			{
				controler_obj.name = s;
				// 初期のとき
				if (Character_Object.Count == 0)
				{
                    Vector2 posTemp = controler_obj.transform.position;

                    for (int i = 0; i < Word_Count; i++)
					{
						Character_Object.Add(new GameObject());
                        Character_Object[i].AddComponent<Image>();
						Display_Characters.Add(Character_Object[i].GetComponent<Image>());
                        Character_Object[i].transform.position = posTemp;
                        posTemp.x += 100.0f;
                        Character_Object[i].transform.SetParent(controler_obj.transform);
                    }
                }

				for (int i = 0; i < s.Length; i++)
				{
					Character_Object[i].name = "Character_" + s[i];
					Display_Characters[i].sprite = Look[character_search(s[i])];
				}
			}
			else if (s.Length > Word_Count)
			{
				Debug.LogError("設定文字数以上");
			}
			else if (s.Length < Word_Count)
			{
				Debug.LogError("設定文字数未満");
			}
		}

		/// <summary>
		/// 文字検索
		/// </summary>
		/// <param name="search_c"> 1文字 </param>
		/// <returns> 文字のあるフォントの番号 </returns>
		private byte character_search(char search_c)
		{
			switch (search_c)
			{
				case '0':
					return 0;
				case '1':
					return 1;
				case '2':
					return 2;
				case '3':
					return 3;
				case '4':
					return 4;
				case '5':
					return 5;
				case '6':
					return 6;
				case '7':
					return 7;
				case '8':
					return 8;
				case '9':
					return 9;
				case 'A':
					return 10;
				case 'B':
					return 11;
				case 'C':
					return 12;
				case 'D':
					return 13;
				case 'E':
					return 14;
				case 'F':
					return 15;
				case 'G':
					return 16;
				case 'H':
					return 17;
				case 'I':
					return 18;
				case 'J':
					return 19;
				case 'K':
					return 20;
				case 'L':
					return 21;
				case 'M':
					return 22;
				case 'N':
					return 23;
				case 'O':
					return 24;
				case 'P':
					return 25;
				case 'Q':
					return 26;
				case 'R':
					return 27;
				case 'S':
					return 28;
				case 'T':
					return 29;
				case 'U':
					return 30;
				case 'V':
					return 31;
				case 'W':
					return 32;
				case 'X':
					return 33;
				case 'Y':
					return 34;
				case 'Z':
					return 35;
				case '?':
					return 36;
				case '!':
					return 37;
				default:
					return 39;
			}
		}

		/// <summary>
		/// 文字数、フォントのリセット
		/// </summary>
		/// <param name="n"> 文字数 </param>
		/// <param name="s"> フォントのパス </param>
		public void Character_Reset(int n, string s)
		{
			Initialization();
			Word_Count = n;
			Look_Load(s);
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialization()
		{
			// Image コンポーネントの情報削除
			Display_Characters.Clear();

			// object の削除
			foreach (GameObject obj1 in Character_Object)
			{
                MonoBehaviour.Destroy(obj1);
			}
			Character_Object.Clear();
		}

		/// <summary>
		/// 表示したいキャンバスの設定
		/// </summary>
		/// <param name="t"> キャンバスのアタッチされた object の </param>
		public void SetControler(object t)
		{
            controler_obj = (GameObject)t;
        }

        /// <summary>
        /// 文字の大きさ替え
        /// </summary>
        /// <param name="size"> 各軸のサイズ </param>
        public void Size_Change(Vector3 size)
        {
			controler_obj.transform.localScale = size;
		}

        /// <summary>
        /// 文字の色替え
        /// </summary>
        /// <param name="color"> 変更したい色 </param>
        public void Color_Change(Color color)
        {
            Font_Color = color;
            foreach (Image image in Display_Characters)
            {
                image.color = color;
            }
        }

        /// <summary>
        /// 文字の表示、非表示の設定
        /// </summary>
        /// <param name="_enable"> コンポーネントの SetActive と同じ使い方 </param>
        public void Set_Enable(bool _enable)
        {
            Enable = _enable;
            foreach(Image image in Display_Characters)
            {
                image.enabled = _enable;
            }
        }

		/// <summary>
		/// 中央揃え
		/// </summary>
		public void Centering()
		{
			float MovingDistance = (100.0f * Word_Count) / 2.0f;
			for(int i = 0; i < Character_Object.Count;i++)
			{
				Vector3 temp = Character_Object[i].transform.localPosition;
				temp.x -= MovingDistance;

				Character_Object[i].transform.localPosition = temp;
			}
		}

		/// <summary>
		/// 位置変更
		/// </summary>
		public void Position_Change(Vector3 next_pos)
		{
			controler_obj.transform.localPosition = next_pos;
		}
	}
}