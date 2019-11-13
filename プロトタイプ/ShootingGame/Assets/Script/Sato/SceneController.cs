// メニューから選択でのシーン移動
// 作成者:佐藤翼
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ボタンを使用するためUIとSceneManagerを使用ためSceneManagementを追加
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	// ボタンをクリックするとBattleSceneに移動します
	public void ButtonClicked_Tittle()
	{
		SceneManager.LoadScene("TITLE");
	}
	public void ButtonClicked_Stage()
	{
		//FadeManager.Instance.LoadScene("Stage", 0.3f);
		SceneManager.LoadScene("Stage_01");
	}
}