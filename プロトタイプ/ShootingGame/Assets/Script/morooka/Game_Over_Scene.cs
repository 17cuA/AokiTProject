using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextDisplay;
using UnityEngine.SceneManagement;

public class Game_Over_Scene : MonoBehaviour
{
	private Character_Display game_over;
	private GameObject game_over_parent;
	public Vector3 game_over_pos;
	public float game_over_size;

	private Character_Display please_push_button;
	private GameObject please_push_button_parent;
	public Vector3 please_push_button_pos;
	public float please_push_button_size;

	private int d;
	private Color mu;
	private Color ari;
	// Start is called before the first frame update
	void Start()
    {
		game_over_parent = new GameObject();
		game_over_parent.transform.parent = transform;
		game_over = new Character_Display(9, "morooka/SS", game_over_parent, game_over_pos);
		game_over.Character_Preference("GAME_OVER");
		game_over.Size_Change(new Vector3(game_over_size, game_over_size, game_over_size));

		please_push_button_parent = new GameObject();
		please_push_button_parent.transform.parent = transform;
		please_push_button = new Character_Display(16, "morooka/SS", please_push_button_parent, please_push_button_pos);
		please_push_button.Character_Preference("PRESS_ANY_BUTTON");
		please_push_button.Size_Change(new Vector3(please_push_button_size, please_push_button_size, please_push_button_size));

		mu = new Color(1.0f,1.0f,1.0f,0.0f);
		ari = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}

	// Update is called once per frame
	void Update()
    {
		d++;
        if(d == 30)
		{
			please_push_button.Color_Change(mu);
		}
		else if(d == 60)
		{
			please_push_button.Color_Change(ari);
			d = 0;
		}

		if(Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
		{
			Scene_Manager.Manager.Screen_Transition_To_Title();
		}
	}
}
