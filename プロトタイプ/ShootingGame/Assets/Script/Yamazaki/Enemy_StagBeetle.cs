// 作成者：山嵜ジョニー
// クワガタの挙動
// 2019/10/24
using UnityEngine;

public class Enemy_StagBeetle : character_status
{
	
	   
	public int sinAngleFrame;
	public int sinAngleFrameMax = 30;
	public float yRange = 8.0f;
	public bool isUp;

    // Start is called before the first frame update
    private void Start()
    {
		isUp = true;

		sinAngleFrame = 0;

		base.Start();
	}

    // Update is called once per frame
    private void Update()
    {
		transform.position = new Vector3(0.0f, Mathf.Sin(((float)sinAngleFrame / (float)sinAngleFrameMax) * Mathf.PI * 2.0f) * yRange / 2.0f, 0.0f);
		sinAngleFrame++;

		if (sinAngleFrame / sinAngleFrameMax > 0.25f && isUp && sinAngleFrame / sinAngleFrameMax < 0.5f)
		{
			isUp = false;
		}
		else if (sinAngleFrame / sinAngleFrameMax > 0.75f && !isUp)
		{
			isUp = true;
		}
		else if (sinAngleFrame / sinAngleFrameMax >= 1.0f)
		{
			sinAngleFrame -= sinAngleFrameMax;
		}

		HSV_Change();
		if (hp < 1)
		{
			Died_Process();
		}
		if (transform.localPosition.x < -35)
		{
			Destroy(this.gameObject);
		}
		base.Update();
	}

}
