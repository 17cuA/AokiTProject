//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/14
//----------------------------------------------------------------------------------------------
// CSVで作ったデータベースの読み込み関数集
//----------------------------------------------------------------------------------------------
// 2019/05/14：CSV を string型 の二次配列化して読み込み
// 2019/05/14：レコード、カラムの配列を検索して読み込み
// 2019/05/16：レコード保管用のクラス作成
// 2019/05/20：コンストラクタの作成
// 2019/06/17：レコード、カラムから各軸で取り出せるようにする
//----------------------------------------------------------------------------------------------
//
// 理想のデータベースの形（例）
// ┏━┳━━━┳━┳━━━┳━・・・━┳━━━━┓
// ┃ID┃NAME  ┃HP┃Speed ┃  ・・・  ┃remarks ┃
// ┣━╋━━━╋━╋━━━╋━・・・━╋━━━━┫
// ┃01┃tomoya┃02┃ 8.6  ┃  ・・・  ┃CLANNAD ┃
// ┣━╋━━━╋━╋━━━╋━・・・━╋━━━━┫
// ・・・  ・  ・・・  ・  ・    ・    ・   ・   ・
// ・・・  ・  ・・・  ・  ・    ・    ・   ・   ・
// ・・・  ・  ・・・  ・  ・    ・    ・   ・   ・
// ┣━╋━━━╋━╋━━━╋━・・・━╋━━━━┫
// ┃99┃nagisa┃02┃ 8.6  ┃  ・・・  ┃CLANNAD ┃
// ┗━┻━━━┻━┻━━━┻━・・・━┻━━━━┛
// 
// 一番左に ID(番号)
// 左から2番目に名前
// の形にしてもらえると使いやすいはず

using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace CSV_Management
{
	public class Record
	{
		public string[,] Database_Array { private set; get; }		// CSV の入る二次元配列

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="resourcesName"> CSVの名前か空白でOK </param>
        public Record(string resourcesName = "")
        {
            if (resourcesName == "")
            {
            }
            else
            {
                CSVArrangement(resourcesName);
            }
        }

        /// <summary>
        /// CSVデータの読み込み
        /// </summary>
        /// <param name="resourcesName"> リソース内のファイル名（パス） </param>
        public void CSVArrangement(string resourcesName)
        {
			Reset();
			//csvファイルの中身を入れる List
			List<string[]> CsvData = new List<string[]>();
			//csvファイルを入れる変数
			TextAsset Word = Resources.Load(resourcesName) as TextAsset;
            StringReader csv = new StringReader(Word.text);
            while (csv.Peek() > -1)
            {
                string line = csv.ReadLine();       // 1行分の読み込み
                CsvData.Add(line.Split(','));       // 1行を列に分ける
            }

			Database_Array = new string[CsvData.Count, CsvData[0].Length];

			// 二次配列への変更
			for (int i = 0; i < CsvData.Count; i++)
            {
                for (int j = 0; j < CsvData[0].Length; j++)
                {
					Database_Array[i, j] = CsvData[i][j];
                }
            }
        }

        /// <summary>
        /// レコードの検索
        /// </summary>
        /// <param name="search_Content"> 検索したい内容 </param>
        /// <returns> 検索した内容があったレコードの二次元配列 </returns>
        public string[,] Record_Search(string search_Content)
		{
			List<string[]> conclusion_list = new List<string[]>();		// 仮置き用
			string[,] returning_string;												// 返す文字列の配列

			// 列の繰り返し
			for (int i = 0; i < Database_Array.Length; i++)
			{
				// 行の繰り返し
				for (int j = 0; j < Database_Array.GetLength(i); j++)
				{
					// 検索の繰り返し
					if (Database_Array[i, j] == search_Content)
					{
						string[] temp = new string[Database_Array.GetLength(1)];
						// 配列のコピー
						for (int l = 0; l < Database_Array.GetLength(1); l++)
						{
							temp[l] = Database_Array[i, l];
						}
						conclusion_list.Add(temp);

						break;
					}
				}
			}

			returning_string = new string[conclusion_list.Count, conclusion_list[0].Length];

			// 列の繰り返し
			for (int i = 0; i < conclusion_list.Count; i++)
			{
				// 行の繰り返し
				for(int j = 0; j < conclusion_list[i].Length; j++)
				{
					returning_string[i, j] = conclusion_list[i][j]; 
				}
			}

			return returning_string;
		}

        /// <summary>
        /// カラムの検索
        /// </summary>
        /// <param name="search_Content"> 検索したい内容 </param>
        /// <returns> 検索した内容があったカラムの配列 </returns>
        public string[,] Column_Search(string search_Content)
		{
			List<string[]> conclusion_list = new List<string[]>();		// 仮置き用
			string[,] returning_string;                                             // 返す文字列の配列

			// 列の繰り返し
			for (int i = 0; i < Database_Array.Length; i++)
			{
				// 行の繰り返し
				for (int j = 0; j < Database_Array.GetLength(i); j++)
				{
					// 検索の繰り返し
					if (Database_Array[i, j] == search_Content)
					{
						string[] temp = new string[Database_Array.GetLength(0)];
						// 配列のコピー
						for (int l = 0; l < Database_Array.GetLength(0); l++)
						{
							temp[l] = Database_Array[i, l];
						}
						conclusion_list.Add(temp);
						break;
					}
				}
			}

			returning_string = new string[conclusion_list.Count, conclusion_list[1].Length];

			// 列の繰り返し
			for (int i = 0; i < conclusion_list.Count; i++)
			{
				// 行の繰り返し
				for (int j = 0; j < conclusion_list[i].Length; j++)
				{
					returning_string[i, j] = conclusion_list[i][j];
				}
			}

			return returning_string;
		}

		/// <summary>
		/// 指定の番号レコードを全て取得
		/// </summary>
		/// <param name="num"> 番号 </param>
		/// <returns> 指定の番号レコード </returns>
		public string[] GetSpecifiedRecord(int num)
		{
			string[] rString = new string[Database_Array.GetLength(0)];

			for(int i = 0; i < Database_Array.GetLength(0); i++)
			{
				rString[i] = Database_Array[num, i];
			}
			return rString;
		}

		/// <summary>
		/// 指定の番号カラムを全て取得
		/// </summary>
		/// <param name="num"> 番号 </param>
		/// <returns>指定の番号カラム</returns>
		public string[] GetSpecifiedColumn(int num)
		{
			string[] rString = new string[Database_Array.GetLength(1)];

			for(int i = 0; i < Database_Array.GetLength(1); i++)
			{
				rString[i] = Database_Array[i, num];
			}
			return rString;
		}

		/// <summary>
		/// IDからレコードの検索
		/// </summary>
		/// <param name="ID"> 検索したいID </param>
		/// <returns> IDのあるレコード </returns>
		public string[] SearchAt_ID(int ID)
        {
            // 返す文字列の配列
            string[] returning_string = new string[Database_Array.GetLength(1)];

			// 検索の繰り返し
            for (int i = 0; i < Database_Array.GetLength(1); i++)
            {
				// ID が一致するとき
                if(Database_Array[i,0] == ID.ToString())
                {
                    // 配列のコピー
                    for (int l = 0; l < Database_Array.GetLength(1); l++)
                    {
                        returning_string[l] = Database_Array[i, l];
                    }
					break;
				}
			}

            return returning_string;
        }

        /// <summary>
        /// 名前からレコードの検索
        /// </summary>
        /// <param name="name"> 検索したい名前 </param>
        /// <returns> 名前のあるレコード </returns>
        public string[] SearchAt_Name(string name)
        {
            // 返す文字列の配列
            string[] returning_string = new string[Database_Array.GetLength(1)];

			// 検索の繰り返し
            for (int i = 0; i < Database_Array.GetLength(1); i++)
            {
				// 名前が一致するとき
                if (Database_Array[i, 1] == name)
                {
                    // 配列のコピー
                    for (int l = 0; l < Database_Array.GetLength(1); l++)
                    {
                        returning_string[l] = Database_Array[i, l];
                    }
					break;
                }
            }
            return returning_string;
        }

		/// <summary>
		/// 変数の初期化
		/// </summary>
		public void Reset()
		{
			Database_Array = new string[0,0];
		}
	}

	public class Record_Container
	{
		public string[] Own_Record { private set; get; }		// レコード入れ

		/// <summary>
		/// 自分のデータの保存
		/// </summary>
		/// <param name="ar"> ストリングの配列 </param>
		public void Set_Data(string[] ar)
		{
			Own_Record = new string[ar.Length];
			Own_Record = ar;
		}

		/// <summary>
		/// 文字列のイント型化
		/// </summary>
		/// <param name="elements_number"> 要素番号 </param>
		/// <returns> int型 の数値 </returns>
		public int ToInt(int elements_number)
		{
			return int.Parse(Own_Record[elements_number]);
		}
	}
}