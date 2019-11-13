using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
	AudioSource audiosource1;//ＢＧＭを流しているAudioSource
	AudioSource audiosource2;//ＢＧＭを流すAudioSource

	public AudioClip First_clip;				//開戦時のＢＧＭ
	public AudioClip First_Boss_clip;			//最初のボスのＢＧＭ
	public AudioClip Second_clip;			//ボス語のＢＧＭ
	public AudioClip Final_Boss_clip;           //最後のボスのＢＧＭ

	public int changecnt;			//交換した回数カウント用

	[Range(0, 1)]
	public float fade_num;             //フェードを行う際に使用
	private bool Is_fadeout;		//フェードアウトを行うかどうか
	private bool Is_fadein;         //フェードインを行うかどうか
	private bool Is_Change;
    // Start is called before the first frame update
    void Start()
    {
		audiosource1 = GetComponent<AudioSource>();
		fade_num = 1;
    }

	// Update is called once per frame
	void Update()
	{
		//if (Input.GetButtonDown("Fire1"))
		//{
		//	audiosource1.Play();
		//}
		if(Input.GetButtonDown("Fire2"))
		{
			Is_Change = true;
		}
		if(Is_Change)
		{
			active_change();
		}
	}

	void fade_out()
	{
		if(!Is_fadeout)
		{
			if (fade_num >= 0)
			{
				audiosource1.volume = fade_num;
				fade_num -= 0.05f;
			}
			else
			{
				Is_fadeout = true;
				Is_fadein = false;
			}
		}
	}

	void fade_in()
	{
		if(!Is_fadein)
		{
			if(fade_num <= 1)
			{
				audiosource1.volume = fade_num;
				fade_num += 0.05f;

			}
			else
			{
				changecnt++;
				Change_BGM();
				Is_fadein = true;
				Is_Change = false;
			}
		}
	}

	void Change_BGM()
	{
		switch(changecnt)
		{
			//ボス戦ように変更
			case 0:
				audiosource1.clip = First_Boss_clip;
				break;
				//ボス戦後に変更
			case 1:
				audiosource1.clip = Second_clip;
				break;
				//最後のボスように変更
			case 2:
				audiosource1.clip = Final_Boss_clip;
				break;
			default:
				changecnt = 0;
				break;
		}
	}
	void active_change()
	{
		if(Is_Change)
		{
			if(!Is_fadeout)
			{
				fade_out();
			}
			if(!Is_fadein)
			{
				fade_in();
			}
		}
	}
}
