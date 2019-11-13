/*
 * 久保田達己
 * 
 * ボタン押されたときに音がなるように
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test_Sound_Check : MonoBehaviour
{
	private int sound_cnt;
	private bool is_explosion;
	private void Start()
	{
		sound_cnt = 12;
		is_explosion = false;
	}
	public void Item_Catch_Test()
	{
		SE_Manager.SE_Obj.SE_Item_Catch();
	}
	public void Explision_Test()
	{
		if (is_explosion)
		{
			SE_Manager.SE_Obj.SE_Explosion(Obj_Storage.Storage_Data.audio_se[9]);
			is_explosion = !is_explosion;
		}
		else
		{
			SE_Manager.SE_Obj.SE_Explosion_small(Obj_Storage.Storage_Data.audio_se[8]);
			is_explosion = !is_explosion;

		}

	}
	public void Power_Up_Test()
	{
		SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);
	}
	public void Voice_Test()
	{
		switch (sound_cnt)
		{
			case 12:
				Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[sound_cnt]);
				SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);
				sound_cnt++;
				break;
			case 13:
				Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[sound_cnt]);
				SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);
				sound_cnt++;
				break;
			case 14:
				Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[sound_cnt]);
				SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);
				sound_cnt++;
				break;
			case 15:
				Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[sound_cnt]);
				SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);
				sound_cnt++;
				break;
			case 16:
				Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[sound_cnt]);
				SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);
				sound_cnt = 25;
				break;
			case 25:
				Voice_Manager.VOICE_Obj.Voice_Active(Obj_Storage.Storage_Data.audio_voice[sound_cnt]);
				SE_Manager.SE_Obj.SE_Active_2(Obj_Storage.Storage_Data.audio_se[16]);
				sound_cnt = 12;

				break;
		}
	}
	public void Shot_Test()
	{
		SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
	}
}
