using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using UnityEngine;

public class BackgroundActivation : MonoBehaviour
{
	//アルファ値
	public float alphaValue = 0.0f;
	//マテリアルリスト
	public Material[] materialList = new Material[10];

	//変化トリガー
	public bool changeTrigger = false;
	//完了フラグ
	public bool completeFlag = false;
	//変化にかかる時間
	public float changeTime = 3.0f;
	//経過時間
	public float elapsedTime = 0.0f;

	//アルファ値を上げる
	public bool make255 = false;

	void Start()
	{
		//アルファ値の変化量
		alphaValue = 0.0f;
		//経過時間
		elapsedTime = 0.0f;
		//make255 = false;

		BoxCollider boxCollider = this.gameObject.GetComponent<BoxCollider>();

		for (int i = 0; i < 10; i++)
		{
			//gamemode:stage1_Nomal
			materialList[i]  = transform.GetChild(i).gameObject.GetComponent<ParticleSystemRenderer>().material;
		}

		for (int i = 0; i < 9; i++)
		{
			//色を黒に変更
			materialList[i].SetColor("_TintColor", new Color(255 / 255, 255 / 255, 255 / 255, 0 / 255));
			//subColor / harf
			//Color(128 / 255, 128 / 255, 128 / 255, 0 / 255)
		}
		materialList[9].SetColor("_TintColor", new Color(0 / 255, 0 / 255, 0 / 255, 0 / 255));
	}

	void Update()
	{
		//色変更
		if (changeTrigger)
		{
			elapsedTime += Time.deltaTime;				//経過時間
			alphaValue = (elapsedTime / changeTime);	//経過時間割合

			//経過時間判定
			if (elapsedTime < changeTime)
			{
				if (make255 == true) TransparencyChange(alphaValue);
				else TransparencyChange(1.0f - alphaValue);
			}
			else
			{
				//値リセット
				changeTrigger = false;
				elapsedTime = 0.0f;

				/*
				//hi koroppari4 / friend siva3
				//main kokuyari
				//kototue okkezakkusu sivaken
				//yonzozyu anubisutue ikusaba
				//omegayari syuumatuono mikaeruken

				//mizu europa3/varuna4
				//main kokutue
				//sugitama sugitama sugitama
				//ropatue huxinburu huxinburu
				//syumatutue omegatue gabutue 

				//tuti yugu4/yugu4
				//main omagaken
				//buroono buroono buroono
				//burokatana burokatana burokatana
				//kokuyari syumatukoto urikobushi

				//kaze texia4/texia4
				//main etalove
				//gurimukoto gurimukoto gurimukoto
				//texiazyu texiazyu texiazyu
				// syumatuyari rahuxakoto
				*/
			}

		}
	}

	//色変更トリガー
	public void TransparencyChangeTrigger()
	{
		make255 = !make255;		//反転
		changeTrigger = true;   //変更トリガー
	}

	//色変更
	private void TransparencyChange(float _alphaValue)
	{
		//経過時間判定
		if (elapsedTime < changeTime)
		{
			for (int i = 0; i < 9; i++)
			{
				materialList[i].SetColor("_TintColor", new Color(1,1,1, _alphaValue ));
			}
			materialList[9].SetColor("_TintColor", new Color(0,0,0, _alphaValue ));
		}
	}
}
