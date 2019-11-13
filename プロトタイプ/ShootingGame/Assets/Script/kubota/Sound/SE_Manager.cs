using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Manager : MonoBehaviour
{
	public static SE_Manager SE_Obj;

	[Header("音を鳴らすかどうか")]
	public bool Is_Active;
	[Header("基本的なSEたち")]
	public AudioSource audiosource1;
	[Header("パワーアップ時になるSE")]
	public AudioSource Power_Up;
	[Header("アイテムを取得するときになるSE")]
	public AudioSource Item_Up;
	[Header("爆発のSE")]
	public AudioSource Explosion;
	[Header("爆発のSE小型")]
	public AudioSource Explosion_Small;
	[Header("登場用のSE")]
	public AudioSource Entry;
	[Header("バキュラヒットSE")]
	public AudioSource Baculor;
	[Header("無線開始時などに使う用")]
	public AudioSource Wireless;
	[Header("プレイヤーの武器変更用")]
	public AudioSource Weapon;
	[Header("ボスの動きの時に鳴らすボスのEffect")]
	public AudioSource Boss_Move;
	[Header("シャウト用")]
	public AudioSource Shout;
	[Header("ボスのコアシールドとコアにあたった時の音")]
	public AudioSource Core;
	[Header("ボスのコア以外のところを攻撃したときになる音")]
	public AudioSource Boss_Body;
	[Header("落としたオプションの回収")]
	public AudioSource Maltiple_Catch;
	private void Awake()
	{
		SE_Obj = GetComponent<SE_Manager>();
	}
	// Start is called before the first frame update
	public void SE_Active(AudioClip se)
	{
		if (audiosource1.isPlaying) audiosource1.Stop();
		if (Is_Active) audiosource1.PlayOneShot(se);
	}
	//先生からもらったもののSE
	public void SE_Active_2(AudioClip se)
	{
		if (Is_Active) Power_Up.PlayOneShot(se);
	}
	//アイテム取得用
	public void SE_Item_Catch()
	{
		if (Item_Up.isPlaying) Item_Up.Stop();
		if (Is_Active) Item_Up.Play();
	}
	//爆発用
	public void SE_Explosion(AudioClip se)
	{
		if (Explosion.isPlaying) Explosion.Stop();
		if (Is_Active) Explosion.PlayOneShot(se);
	}
	//雑魚敵用爆発
	public void SE_Explosion_small(AudioClip se)
	{
		if (Explosion_Small.isPlaying) Explosion_Small.Stop();
		if (Is_Active) Explosion_Small.PlayOneShot(se);
	}
	//登場＆リスポーン用
	public void SE_Entry(AudioClip se)
	{
		if (Entry.isPlaying) Entry.Stop();
		if (Is_Active) Entry.PlayOneShot(se);
	}
	//バキュラヒット用
	public void SE_Baculor(AudioClip se)
	{
		if (Baculor.isPlaying) Baculor.Stop();
		if (Is_Active) Baculor.PlayOneShot(se);
	}
	//プレイヤーのバレット変更用
	public void weapon_Change(AudioClip se)
	{
		if (Weapon.isPlaying) Weapon.Stop();
		if (Is_Active) Weapon.PlayOneShot(se);
	}	
	//敵を倒した際に叫ぶよう
	public void Enemy_Scleem(AudioClip se)
	{
		if (Shout.isPlaying) Shout.Stop();
		if (Is_Active) Shout.PlayOneShot(se);
	}

	//ボスの動きの時に使用する
	public void Boss_SE(AudioClip se)
	{
		if (Boss_Move.isPlaying) Boss_Move.Stop();
		if (Is_Active) Boss_Move.PlayOneShot(se);
	}
	  //ボスのコアとコアシールドに当たった時
	 public void Boss_Core(AudioClip se)
	{
		  if(Core.isPlaying) Core.Stop();
		  if(Is_Active)Core.PlayOneShot(se);
	}
	//ボスのボディに当たった時の音
	public void Boss_Body_SE(AudioClip se)
	{
		  if(Boss_Body.isPlaying) Boss_Body.Stop();
		  if(Is_Active)Boss_Body.PlayOneShot(se);
	}
	//落としたマルチプルの回収
	public void Maltiple_Catch_SE(AudioClip se)
	{
		if (Maltiple_Catch.isPlaying) Maltiple_Catch.Stop();
		if (Is_Active) Maltiple_Catch.PlayOneShot(se);
	}
}
