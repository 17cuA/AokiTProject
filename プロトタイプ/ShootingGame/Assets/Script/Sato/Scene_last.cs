// エンディングシーンからの移動
// 作成者:佐藤翼
// 変更者:諸岡
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextDisplay;
using UnityEngine.SceneManagement;

public class Scene_last : MonoBehaviour
{
	[SerializeField,Header("表示文字")]public string string_to_display;

	private Character_Display game_last;
	private GameObject game_last_parent;
	public Vector3 game_last_pos;
	public float game_last_size;
	public string Game_Last_String { get; set; }

	private Character_Display score;
	private GameObject score_parent;
	public Vector3 score_pos;
	public float score_size;
	public string Score_String { get; set; }

	private Character_Display please_push_button;
	private GameObject please_push_button_parent;
	public Vector3 please_push_button_pos;
	public float please_push_button_size;
	public string Please_Push_Button_String { get; set; }

	private int d;
	private Color mu;
	private Color ari;

	//追加--------------------------
	private int frame;		//自動でタイトルに行くように
	//------------------------------
	private void Start()
	{
		Game_Last_String = string_to_display;
		Score_String = "TOTALSCORE__" + Game_Master.display_score_1P.ToString("D10");
		Please_Push_Button_String = "PRESS__BUTTON";

		game_last_parent = new GameObject();
		game_last_parent.transform.parent = transform;
		game_last = new Character_Display(Game_Last_String.Length, "morooka/SS", game_last_parent, game_last_pos);
		game_last.Character_Preference(string_to_display);
		game_last.Size_Change(new Vector3(game_last_size, game_last_size, game_last_size));
		game_last.Centering();

		score_parent = new GameObject();
		score_parent.transform.parent = transform;
		score = new Character_Display(Score_String.Length, "morooka/SS", score_parent, score_pos);
		score.Character_Preference(Score_String);
		score.Size_Change(new Vector3(score_size, score_size, score_size));
		score.Centering();

		please_push_button_parent = new GameObject();
		please_push_button_parent.transform.parent = transform;
		please_push_button = new Character_Display(Please_Push_Button_String.Length, "morooka/SS", please_push_button_parent, please_push_button_pos);
		please_push_button.Character_Preference(Please_Push_Button_String);
		please_push_button.Size_Change(new Vector3(please_push_button_size, please_push_button_size, please_push_button_size));
		please_push_button.Centering();

		mu = new Color(1.0f,1.0f,1.0f,0.0f);
		ari = new Color(1.0f, 1.0f, 1.0f, 1.0f);

	}

	void Update()
	{
		d++;
		frame++;
        if(d == 30)
		{
			please_push_button.Color_Change(mu);
		}
		else if(d == 60)
		{
			please_push_button.Color_Change(ari);
			d = 0;
		}

		if (Input.anyKeyDown && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.F4) && !Input.GetKey(KeyCode.LeftControl) || frame > 1200)
		{
			Scene_Manager.Manager.Screen_Transition_To_Title();
		}
	}
}