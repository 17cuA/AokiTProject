using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChenkenLaser;

 [DefaultExecutionOrder(599)]
public class LaserEmitter : MonoBehaviour
{
	public Material lineMaterial;
    private List<ChenkenLaser.Laser> lasers;
    private ChenkenLaser.Laser currentLaser;
	[Header("--------生成するレーザー設定-------")]
    public float lineWidth = 0.2f;
    public float lineLength = 0.5f;
    public float shotSpeed = 0.8f;
	public int laserMaxNum;
	public float overLoadDutarion;

	[Header("--------回転プロパティ--------")]
	public bool canRotateControl;
	public float rotateDivisor = 12;

	[Header("-------レーザーポイントオブジェクト--------")]
    public GameObject LaserNode;

    private float fireInterval;
    private int laserCurrentNum;
    private float laserCanShotTime  = 0f;

    private void Awake()
    {
        this.lasers         = new List<ChenkenLaser.Laser>();
        this.currentLaser   = null;
		this.fireInterval   = lineLength / (shotSpeed * 60);
	}

    public void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            this.LaunchLaserInstance();
        }

        if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
        {
			if(this.currentLaser == null)
			{
				this.LaunchLaserInstance();
			}

            if(Time.time > this.laserCanShotTime && currentLaser != null)
            {
                this.LaunchLaserContinous();
            }
        }

        if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space)  && this.currentLaser != null)
        {
            this.currentLaser.IsFixedPos = false;
			this.currentLaser = null;
        }

        if(this.laserCurrentNum >= this.laserMaxNum && this.currentLaser != null)
        {
            this.currentLaser.IsFixedPos = false;
			this.currentLaser = null;

			this.laserCanShotTime = Time.time + this.overLoadDutarion;
            this.laserCurrentNum  = 0;
        }

        if(!this.transform.parent.gameObject.activeSelf && this.currentLaser != null)
        {
            this.currentLaser.IsFixedPos = false;
			this.currentLaser = null;
        }
    }

    private void LaunchLaserInstance()
    {
		Debug.Log("レーザーライン生成する。");

		for (var i = 0; i < this.lasers.Count; ++i)
        {
            if(!this.lasers[i].gameObject.activeSelf)
            {
				this.currentLaser = this.lasers[i];
                this.currentLaser.ResetLineRenderer(); 
                this.currentLaser.IsFixedPos = true;
                this.currentLaser.gameObject.SetActive(true);
                return;
            }
        }

        var laserGo            = new GameObject("Laser");
        var newLaser           = laserGo.AddComponent<ChenkenLaser.Laser>();
        newLaser.LineWidth     = lineWidth;
        newLaser.LaserMaterial = lineMaterial;
        newLaser.LaserNode     = LaserNode;
        newLaser.IsFixedPos = true;
        newLaser.ShotSpeed     = this.shotSpeed;
        laserGo.transform.SetParent(this.transform);
        laserGo.transform.localPosition = Vector3.zero;
        this.currentLaser =  newLaser;
        this.lasers.Add(currentLaser); 
    }

    private void LaunchLaserContinous()
    {
		Debug.Log("レーザーポイント生成し続ける。");
        this.currentLaser.Launch();
        this.laserCurrentNum++;
        this.laserCanShotTime = Time.time + fireInterval;
        this.currentLaser.IsFixedPos = true;
    }
}
