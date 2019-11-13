using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CharaRotation : MonoBehaviour
{
	public float rotation_speed;
	private int rotation_cnt;
	private bool Is_return;
	private PlayableDirector anim;
	public PlayableAsset animation_1;
	public PlayableAsset animation_2;
	public ParticleSystem jet;
	private int frame;
	private bool is_anim;
	public int frame_max;
	// Start is called before the first frame update
	void Start()
    {
		frame = 0;
		rotation_cnt = 0;
		Is_return = false;
		anim = GetComponent<PlayableDirector>();
		anim.Play(animation_1);
	}

	// Update is called once per frame
	void Update()
    {
		if(is_anim) frame++;

		if (Input.GetButtonDown("Fire1"))
		{
			is_anim = true;
			transform.position = new Vector3(-50, 0, 0);
		}

		if(frame > frame_max)
		{
			anim.Play(animation_1);
			jet.Play();
			frame = 0;
			is_anim = false;
		}

		//self_rotation();
	}
	private void self_rotation()
	{
		transform.localRotation = Quaternion.Euler(rotation_speed, 0, 0);
		if (Is_return)
		{
			rotation_speed -= 10;
		}
		else
		{
			rotation_speed += 10;
		}
		if (rotation_speed > 360)
		{
			rotation_speed = 0;
			rotation_cnt++;
		}
	}
	private void Restore_Rotation()
	{
		if (transform.localRotation.x != 0)
		{
			if (transform.localRotation.x > 0)
			{
				transform.localRotation = Quaternion.Euler(rotation_speed, 0, 0);
				rotation_speed -= 10;
			}
			else if (transform.localRotation.x < 0)
			{
				transform.localRotation = Quaternion.Euler(rotation_speed, 0, 0);
				rotation_speed += 10;
			}
		}
	}
}
