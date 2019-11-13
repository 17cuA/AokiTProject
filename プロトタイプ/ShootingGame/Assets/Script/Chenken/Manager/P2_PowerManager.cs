using System;
using System.Collections.Generic;
using UnityEngine;

namespace Power
{
	public class P2_PowerManager : Singleton<P2_PowerManager>
	{
		/// <summary>
		/// パワークラス
		/// </summary>
		public class Power
		{
			/// <summary>
			/// パワータイプ列挙型
			/// </summary>
			public enum PowerType
			{
				SPEEDUP,
				MISSILE,
				DOUBLE,
				LASER,
				OPTION,
				SHIELD,
				INITSPEED,
				KILLALL
			}

			private int upgradeCount;                                                       //現在強化回数
			private int maxUpgradeCount;                                                    //最大強化可能回数
			private int tempMaxUpgradeCount;
			private PowerType type;                                                         //パワータイプ
			private List<ExcutePowerUpgradeCallBack> onUpgradeCallBacks;//強化時コールバック
			private List<ResetPowerCallBack> onResetCallBacks;                  //リセット時コールバック
			private CheckPowerResetCallBack onCheckResetCallBack;           //条件判断コールバック
			private bool resetCheck;                                                            //現在リセット状態

			public PowerType Type { get { return type; } }                          //取得要パワータイプ
			public bool CanUpgrade                                                          //強化可能かどうか
			{
				get
				{
					return upgradeCount < maxUpgradeCount;
				}
			}

			/// <summary>
			/// コンストラクタ➀
			/// </summary>
			/// <param name="type">　　　　			　パワータイプ　　</param>
			/// <param name="maxUpgradeCount">　最大強化回数　</param>
			public Power(PowerType type, int maxUpgradeCount)
			{
				this.upgradeCount = 0;
				this.maxUpgradeCount = maxUpgradeCount;
				this.tempMaxUpgradeCount = this.maxUpgradeCount;
				this.type = type;
				this.resetCheck = false;
				this.onUpgradeCallBacks = new List<ExcutePowerUpgradeCallBack>();
				this.onResetCallBacks = new List<ResetPowerCallBack>();
			}

			/// <summary>
			/// コンストラクタ②
			/// </summary>
			/// <param name="type">　　パワータイプ　</param>
			public Power(PowerType type)
			{
				this.type = type;
				this.resetCheck = false;
				this.upgradeCount = 0;
				this.maxUpgradeCount = 999999999;
				this.tempMaxUpgradeCount = this.maxUpgradeCount;
				this.onUpgradeCallBacks = new List<ExcutePowerUpgradeCallBack>();
				this.onResetCallBacks = new List<ResetPowerCallBack>();
			}

			/// <summary>
			/// 強化
			/// </summary>
			public void Upgrade()
			{
				//強化コンストラクタ関数実行
				for (var i = 0; i < onUpgradeCallBacks.Count; ++i)
				{
					if (onUpgradeCallBacks != null)
						onUpgradeCallBacks[i]();
				}

				//強化回数＋１
				upgradeCount++;
				DebugManager.OperationDebug("強化成功、パワーアップ名：" + type.ToString(), "Player2");
			}

			/// <summary>
			/// 更新
			/// </summary>
			public void Update()
			{
				//　条件判断コールバック関数が存在し、　リセット状態はFalse　-> Trueに変更する時　　、条件判断コールバック関数でTrueが返された時、　　　
				if (onCheckResetCallBack != null && resetCheck != onCheckResetCallBack() && onCheckResetCallBack() == true)
				{
					//強化回数リセット
					SetUpgradeCount(0);
					//リセット処理実行する
					for (var i = 0; i < onResetCallBacks.Count; ++i)
					{
						if (onResetCallBacks != null)
							onResetCallBacks[i]();
					}
					//リセット状態Trueにする
					resetCheck = true;
					return;
				}

				if (onCheckResetCallBack != null && resetCheck != onCheckResetCallBack() && onCheckResetCallBack() == false)
				{
					resetCheck = false;
					return;
				}
			}

			/// <summary>
			/// 強化回数設定メソッド
			/// </summary>
			/// <param name="count">　　設定したい回数　</param>
			public void SetUpgradeCount(int count)
			{
				if (upgradeCount == count)
					return;

				if (count > maxUpgradeCount)
					return;

				upgradeCount = count;
			}

			/// <summary>
			/// 強化処理を監視するメソッド
			/// </summary>
			/// <param name="callBack">　void()　</param>
			public void AddUpgradeFunction(ExcutePowerUpgradeCallBack callBack)
			{
				if (callBack == null)
					return;

				if (onUpgradeCallBacks.Contains(callBack))
					return;

				onUpgradeCallBacks.Add(callBack);
			}

			/// <summary>
			/// 強化処理の監視を削除するメソッド
			/// </summary>
			/// <param name="callBack"> void() </param>
			public void RemoveUpgradeFunction(ExcutePowerUpgradeCallBack callBack)
			{
				if (callBack == null)
					return;

				if (!onUpgradeCallBacks.Contains(callBack))
					return;

				onUpgradeCallBacks.Remove(callBack);
			}

			/// <summary>
			/// 強化リセット条件判断処理を監視するメソッド
			/// </summary>
			/// <param name="callBack">　　　　bool()　</param>
			/// <param name="resetCallBack">　void()　</param>
			public void AddCheckFunction(CheckPowerResetCallBack callBack, ResetPowerCallBack resetCallBack)
			{
				if (callBack == null)
					return;

				onCheckResetCallBack = callBack;

				if (onResetCallBacks.Contains(resetCallBack))
					return;

				onResetCallBacks.Add(resetCallBack);
			}

			/// <summary>
			/// 強化リセット条件判断処理の監視を削除するメソッド
			/// </summary>
			/// <param name="resetCallBack">　void()　</param>


			public void RemoveCheckFunction()
			{
				onCheckResetCallBack = null;
				onResetCallBacks.Clear();
			}

             public void RemoveUpdateFunction()
            {
                onUpgradeCallBacks.Clear();
            }


			public void ResetUpgradeCount()
			{
				upgradeCount = 0;
			}

			public void ReduceMaxUpgradeTime(int time)
			{
				this.maxUpgradeCount -= time;
			}

			public void ResetMaxUpgradeTime()
			{
				this.maxUpgradeCount = this.tempMaxUpgradeCount;
			}
		}

		//--------------------------------------------------
		//パワーマネジメント
		//プロパティ
		//-------------------------------------------------

		/// <summary>
		/// パワーデータ
		/// </summary>
		private Dictionary<Power.PowerType, Power> powers = new Dictionary<Power.PowerType, Power>
		{
			{ Power.PowerType.SPEEDUP, new Power(Power.PowerType.SPEEDUP, 5) },
			{ Power.PowerType.MISSILE,  new Power(Power.PowerType.MISSILE, 1) },
			{ Power.PowerType.DOUBLE,  new Power(Power.PowerType.DOUBLE,  1) },
			{ Power.PowerType.LASER,     new Power(Power.PowerType.LASER,   1) },
			{ Power.PowerType.OPTION,   new Power(Power.PowerType.OPTION,  4) },
			{ Power.PowerType.SHIELD,    new Power(Power.PowerType.SHIELD,  1) },
			{Power.PowerType.INITSPEED, new Power(Power.PowerType.INITSPEED , 1) },
		};

		/// <summary>
		/// 敵全滅させるパワー
		/// </summary>
		private Power annihilate = new Power(Power.PowerType.KILLALL);

		/// <summary>
		/// 現在選択されたパワーはデータにある位置
		/// </summary>
		private int position = -1;
		public int Position
		{
			get
			{
				return position;
			}
		}

		/// <summary>
		/// 現在選択されたパワー
		/// </summary>
		public Power CurrentPower
		{
			get
			{
				if (0 <= position && position < powers.Count)
					return powers[(Power.PowerType)position];

				return null;
			}
		}

		/// <summary>
		/// 現在選択されたパワーは強化可能かどうか
		/// </summary>
		public bool CurrentPowerCanUpgrade
		{
			get
			{
				return CurrentPower != null && CurrentPower.CanUpgrade;
			}
		}

		/// <summary>
		/// 強化処理を監視するメソッド
		/// </summary>
		/// <param name="type">　パワータイプ　</param>
		/// <param name="callBack">　void()　</param>
		public void AddFunction(Power.PowerType type, ExcutePowerUpgradeCallBack callBack)
		{
			if (callBack == null)
				return;

			if (type == Power.PowerType.KILLALL)
			{
				annihilate.AddUpgradeFunction(callBack);
				return;
			}

			if (!powers.ContainsKey(type))
				return;

			powers[type].AddUpgradeFunction(callBack);
		}

		/// <summary>
		/// 強化処理の監視を削除するメソッド
		/// </summary>
		/// <param name="type">　パワータイプ　</param>
		/// <param name="callBack">　void() </param>
		public void RemoveFunction(Power.PowerType type, ExcutePowerUpgradeCallBack callBack)
		{
			if (callBack == null)
				return;

			if (type == Power.PowerType.KILLALL)
			{
				annihilate.RemoveUpgradeFunction(callBack);
				return;
			}

			if (!powers.ContainsKey(type))
				return;

			powers[type].RemoveUpgradeFunction(callBack);
		}

		/// <summary>
		/// パワーリセット条件判断処理を監視するメソッド及びリセット後の処理を監視するメソッド
		/// </summary>
		/// <param name="type">　　パワータイプ　</param>
		/// <param name="callBack">　bool()　</param>
		/// <param name="resetCallBack"> void() </param>
		public void AddCheckFunction(Power.PowerType type, CheckPowerResetCallBack callBack, ResetPowerCallBack resetCallBack)
		{
			if (callBack == null)
				return;

			if (!powers.ContainsKey(type))
				return;

			powers[type].AddCheckFunction(callBack, resetCallBack);
		}

		/// <summary>
		/// パワーリセット条件判断処理の監視を削除するメソッド及びリセット後の処理の監視を削除するメソッド
		/// </summary>
		/// <param name="type">　パワータイプ　</param>
		/// <param name="callBack">　bool()　</param>
		/// <param name="resetCallBack"> void() </param>
		public void RemoveCheckFunction(Power.PowerType type, CheckPowerResetCallBack callBack, ResetPowerCallBack resetCallBack)
		{
			if (callBack == null)
				return;

			if (!powers.ContainsKey(type))
				return;

			powers[type].RemoveCheckFunction();
		}

		/// <summary>
		/// 強化処理
		/// </summary>
		public void Upgrade()
		{
			if (!CurrentPowerCanUpgrade)
				return;

			CurrentPower.Upgrade();
			if (CurrentPower.Type == Power.PowerType.SPEEDUP && !CurrentPower.CanUpgrade)
			{
				CurrentPower.ResetUpgradeCount();
				var power = powers[Power.PowerType.SPEEDUP];
				powers[Power.PowerType.SPEEDUP] = powers[Power.PowerType.INITSPEED];
				powers[Power.PowerType.INITSPEED] = power;
			}

			if (CurrentPower.Type == Power.PowerType.INITSPEED && !CurrentPower.CanUpgrade)
			{
				CurrentPower.ResetUpgradeCount();
				var power = powers[Power.PowerType.SPEEDUP];
				powers[Power.PowerType.SPEEDUP] = powers[Power.PowerType.INITSPEED];
				powers[Power.PowerType.INITSPEED] = power;
			}

			position = -1;
		}

		/// <summary>
		/// 敵全滅処理
		/// </summary>
		public void Annihilate()
		{
			annihilate.Upgrade();
		}

		/// <summary>
		/// アイテム取得処理
		/// </summary>
		public void Pick()
		{
			position++;

			position %= powers.Count - 1;

			DebugManager.OperationDebug("現在パワーアップ位置：" + ((Power.PowerType)position).ToString(), "Player2");
		}

		/// <summary>
		/// パワー取得処理
		/// </summary>
		/// <param name="type">　パワータイプ　</param>
		/// <returns></returns>
		public Power GetPower(Power.PowerType type)
		{
			if (type == Power.PowerType.KILLALL)
				return annihilate;

			if (!powers.ContainsKey(type))
				return null;

			return powers[type];
		}

		/// <summary>
		/// ビットン数更新処理、　ビットン数 = 現在オプションパワー強化数
		/// </summary>
		/// <param name="count"></param>
		public void UpdateBit(int count)
		{
			powers[Power.PowerType.OPTION].SetUpgradeCount(count);
		}

		/// <summary>
		/// マネージャー更新
		/// </summary>
		public void Update()
		{
			// パワーアップ数に注意
			for (var i = 0; i < powers.Count - 1; ++i)
			{
				var power = powers[(Power.PowerType)i];
				power.Update();
			}
		}

		public void ClearPosition()
		{
			position = -1;
		}

		//後消す
		public void ResetShieldPower() { }
		public void ResetAllPowerUpgradeCount()
		{
			for (var i = 0; i < powers.Count; ++i)
			{
				powers[(Power.PowerType)i].ResetUpgradeCount();
			}

             if (powers[Power.PowerType.SPEEDUP].Type == Power.PowerType.INITSPEED)
			{
				var power = powers[Power.PowerType.SPEEDUP];
				powers[Power.PowerType.SPEEDUP] = powers[Power.PowerType.INITSPEED];
				powers[Power.PowerType.INITSPEED] = power;
			}
		}

		public void RemoveAllCheckCallBack()
		{
			foreach (var power in powers.Values)
			{
				power.RemoveCheckFunction();
			}
		}

        public void RemoveAllUpdateCallBack()
		{
			foreach(var power in powers.Values)
			{
				power.RemoveUpdateFunction();
			}
		}

		/// <summary>
		/// 07/20追加　原因：プレイヤーが死んだら選択がリセットされない
		/// 選択だけリセット
		/// </summary>
		public void ResetSelect()
		{
			position = -1;
		}
	}
}
