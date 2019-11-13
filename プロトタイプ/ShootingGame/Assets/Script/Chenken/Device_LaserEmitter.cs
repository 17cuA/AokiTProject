using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChenkenLaser;
using UnityEngine;

 [DefaultExecutionOrder(599)]
 [RequireComponent(typeof(AudioSource))]
class Device_LaserEmitter : MonoBehaviour
{
	public bool isPlayerUseAudio = false;
	public bool isClose = false;

	[Header("Fireボタン/キー　設定")]
	public KeyCode firekey;
	public string fireButtonName;
	InputManagerObject inputManager1P;
	InputManagerObject inputManager2P;

	//--------------------------- 直線型レーザー（タイプ１） ----------------------------------
	[Header("--------直線型レーザー--------")]
	[SerializeField] [Range(0.5f,1.2f)]   private float straightLaserShotSpeed        = 0.8f;
	[SerializeField] [Range(0.1f,0.025f)] private float straightLaserWidth            = 0.05f;
	[SerializeField] [Range(20,70)]       private int straightLaserNodeMax            = 50;
	[SerializeField] [Range(0.2f,0.6f)]   private float straightLaserOverloadDuration = 0.4f;
	[SerializeField] [Range(0.005f,0.05f)]private float straightLaserLaunchInterval   = 0.01f;
	[SerializeField] private float straightTrailWidth = 0.1f;
	[SerializeField] private Material straightLaserMaterial;
	private GameObject straightLaserGeneratorParent;

	public GameObject parentObj;
	public Bit_Formation_3 bf;
	public string parentname;
	bool isOption;
	private float endTimer;
	private bool isEnd = false;

	/// <summary>
	/// 回転装置
	/// </summary>
	public class EmitterRotateCore
	{
		public float theta;					
		public Vector3 pushPosition;

		public EmitterRotateCore(Vector3 pushPosition)
		{
			this.pushPosition = pushPosition;
			this.theta = 0;
		}

		public void Rotate(float angle)
		{
			this.theta += angle;
			this.pushPosition.x = Mathf.Cos(this.theta);
			this.pushPosition.y = Mathf.Sin(this.theta);
		}

		public void Reset()
		{
			theta = 0;
		}
	}

	/// <summary>
	/// 発射装置
	/// </summary>
	public class EmitterLaunchCore
	{
		public ILaunchDevice currentLaunchDevice;

		public EmitterLaunchCore(ILaunchDevice defaultLaunchDevice)
		{
			this.currentLaunchDevice = defaultLaunchDevice;
		}

		public void SetDevice(ILaunchDevice newLaunchDevice)
		{
			this.currentLaunchDevice = newLaunchDevice;
		}

		public void GenerateLine(float laserShotSpeed, float laserWidth, Material laserMaterial, int pointMax)
		{
			currentLaunchDevice.GenerateLine(laserShotSpeed, laserWidth, laserMaterial, pointMax);
		}

		public void LaunchNode(float trailWidth)
		{
			currentLaunchDevice.LaunchNode(trailWidth);
		}

	}

	/// <summary>
	/// 発射専用部品インタフェース
	/// </summary>
	public interface ILaunchDevice
	{
		Instance_Laser_Node_Generator CurrentGenerator { get; set; }
		List<Instance_Laser_Node_Generator> generators { get; set; }

		float OverloadDuration          { get; set; }
		float LaunchInterval            { get; set; }
		float CanLaunchTime             { get; set; }
		GameObject EmitterInstance      { get; set; }

		void GenerateLine(float laserShotSpeed, float laserWidth, Material laserMaterial,int pointMax);
		void LaunchNode(float trailWidth);
	}

	/// <summary>
	/// 直線発射部品
	/// </summary>
	public class StraightLaunchDevice : ILaunchDevice
	{
		public float OverloadDuration          { get; set; }
		public float LaunchInterval            { get; set; }
		public float CanLaunchTime             { get; set; }
		public GameObject EmitterInstance      { get; set; }

		public Instance_Laser_Node_Generator CurrentGenerator { get; set; }
		public List<Instance_Laser_Node_Generator> generators { get; set; }

		public StraightLaunchDevice(float overloadDuration,float launchInterval, GameObject emitterInstance)
		{
			this.OverloadDuration = overloadDuration;
			this.LaunchInterval   = launchInterval;
			this.CanLaunchTime    = 0f;
			this.EmitterInstance  = emitterInstance;

			this.CurrentGenerator     = null;
			this.generators           = new List<Instance_Laser_Node_Generator>();
		}

		public void GenerateLine(float laserShotSpeed, float laserWidth, Material laserMaterial, int pointMax)
		{
			for(var i = 0; i < this.generators.Count; ++i)
			{
				if(!this.generators[i].gameObject.activeSelf)
				{
					this.CurrentGenerator = this.generators[i];
					this.CurrentGenerator.ResetLineRenderer();
					this.CurrentGenerator.IsFixed = true;
					this.CurrentGenerator.gameObject.SetActive(true);
					return;
				}
			}

			var generatorGo = new GameObject("Generator");
			var generator = generatorGo.AddComponent<Instance_Laser_Node_Generator>();

			generator.Setting(laserShotSpeed, laserWidth, laserMaterial,pointMax);
			generator.IsFixed = true;

			generatorGo.transform.SetParent(EmitterInstance.transform);
			generatorGo.transform.localPosition = Vector3.zero;

			this.CurrentGenerator = generator;
			this.generators.Add(generator);
		}

		public void LaunchNode(float trailWidth)
		{
			this.CurrentGenerator.LaunchNode(trailWidth,false);
			this.CanLaunchTime = Time.time + LaunchInterval;
		}
	}



	private EmitterLaunchCore emitterLaunchCore;
	private AudioSource audioSource;
	[SerializeField] private AudioClip laserBegin;
	[SerializeField] private AudioClip laserContinuing;
	[SerializeField] private AudioClip laserEnd;
	private float firePressTime;

	private void OnEnable()
	{
		this.isClose = false;
	}

	private void Awake()
	{
		var straightLaserParent = new GameObject("Device_StrightLaserParent");
		straightLaserParent.transform.SetParent(this.transform);
		straightLaserParent.transform.localPosition = Vector3.zero;
		this.straightLaserGeneratorParent = straightLaserParent;


		this.emitterLaunchCore = new EmitterLaunchCore(new StraightLaunchDevice(this.straightLaserOverloadDuration, this.straightLaserLaunchInterval, this.straightLaserGeneratorParent));

		inputManager1P = GameObject.Find("InputManager_1P").GetComponent<InputManagerObject>();
		inputManager2P = GameObject.Find("InputManager_2P").GetComponent<InputManagerObject>();

		if (isPlayerUseAudio)
		{
			this.audioSource = GetComponent<AudioSource>();
		}
		parentObj = transform.parent.gameObject;
		parentname = parentObj.name;
		if (parentObj.name == "Option(Clone)")
		{
			bf = parentObj.GetComponent<Bit_Formation_3>();
			isOption = true;
		}
		else if (parentObj.name == "Player(Clone)")
		{

			fireButtonName = inputManager1P.Manager.Button["Shot"];
			isOption = false;
		}
		else if (parentObj.name == "Player2(Clone)")
		{
			fireButtonName = inputManager2P.Manager.Button["Shot"];
			isOption = false;
		}

	}

    private void OnDisable()
    {

        if(Obj_Storage.Storage_Data.Laser_Line != null)
        { 
             for (int i = 29; i < Obj_Storage.Storage_Data.Laser_Line.Get_Obj().Count; i++)
             {
                 if(!Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i].gameObject.activeSelf)
                 { 
                     Destroy(Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i]);
                     Obj_Storage.Storage_Data.Laser_Line.Get_Obj().RemoveAt(i);
                 }
            }
        }
    }
    private void Update()
	{

		var launchDevice = emitterLaunchCore.currentLaunchDevice;
		if (isOption)
		{
			if (bf.bState == Bit_Formation_3.BitState.Player1)
			{
				fireButtonName = inputManager1P.Manager.Button["Shot"];
			}
			else if (bf.bState == Bit_Formation_3.BitState.Player2)
			{
				fireButtonName = inputManager2P.Manager.Button["Shot"];
			}
		}

		if (this.isClose)
		{
			if(launchDevice is StraightLaunchDevice)
			{
				var num = transform.GetChild(0).childCount;
				for(var i = 0; i < num; ++i)
				{
					if(transform.GetChild(0).GetChild(i).gameObject.activeSelf)
					{
						return;
					}
				}
				this.gameObject.SetActive(false);
			}
		}
		else
		{
			//-----------------------------------------------------------------入力 検索----------------------------------------------------------------------
			if (Input.GetButtonDown(fireButtonName) || Input.GetKeyDown(firekey))
			{
			    this.emitterLaunchCore.GenerateLine(straightLaserShotSpeed, straightLaserWidth, straightLaserMaterial, straightLaserNodeMax);

				if (audioSource.isPlaying) audioSource.Stop();
				audioSource.clip = laserBegin;
				audioSource.Play();

				isEnd = false;
			}

			if (Input.GetButton(fireButtonName) || Input.GetKey(firekey))
			{
				if (launchDevice.CurrentGenerator == null)
				{
					this.emitterLaunchCore.GenerateLine(straightLaserShotSpeed, straightLaserWidth, straightLaserMaterial, straightLaserNodeMax);			
				}
                else
                { 
				    if (Time.time >= launchDevice.CanLaunchTime && launchDevice.CurrentGenerator != null)
				    {
					
					    if (isPlayerUseAudio)
					    {
						
						    //記念すべきAセット by Johnny Yamazaki
						    // ドトールのミラノサンドはおいしいよ
						    if (audioSource.time >= laserBegin.length * 0.6f && audioSource.clip == laserBegin && audioSource.clip != laserEnd)
						    {
							    audioSource.clip = laserContinuing;
							    audioSource.loop = true;
							    audioSource.Play();
						    }
						    if(audioSource.clip == laserEnd && audioSource.time >= laserEnd.length * 0.6f)
						    {
							    if (audioSource.isPlaying) audioSource.Stop();
							    audioSource.clip = laserBegin;
							    audioSource.Play();
						    }

					    }

						 this.emitterLaunchCore.LaunchNode(straightTrailWidth);
				    }
                }
			}

			if (Input.GetButtonUp(fireButtonName) || Input.GetKeyUp(firekey))
			{
				if (launchDevice.CurrentGenerator != null)
				{
					launchDevice.CurrentGenerator.IsFixed = false;
					launchDevice.CurrentGenerator = null;
				}

				if (isPlayerUseAudio)
				{
					if (audioSource.isPlaying) audioSource.Stop();
					audioSource.clip = laserEnd;
                    audioSource.loop = false;
					audioSource.Play();
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------
		

		//------------------------------------------------------------レーザー発射装置使いすぎ------------------------------------------------------
		if(launchDevice.CurrentGenerator != null && launchDevice.CurrentGenerator.IsOverLoad)
		{
			launchDevice.CurrentGenerator.IsFixed = false;
			launchDevice.CurrentGenerator = null;

			launchDevice.CanLaunchTime = Time.time + launchDevice.OverloadDuration;
			//if (isPlayerUseAudio)
			//{
			//	if (audioSource.loop) audioSource.loop = false;
			//	if (audioSource.isPlaying) audioSource.Stop();
			//	audioSource.PlayOneShot(laserEnd);

			//}
		}

		if(launchDevice.CurrentGenerator != null && launchDevice.CurrentGenerator.pointCount ==  straightLaserNodeMax / 3 * 2)
		{
			if (isPlayerUseAudio)
			{
				if (audioSource.isPlaying) audioSource.Stop();
				audioSource.clip = laserEnd;
				audioSource.Play();
			}
		}
		//------------------------------------------------------------------------------------------------------------------------------------------------


		if(!this.transform.parent.gameObject.activeSelf && launchDevice.CurrentGenerator != null)
		{
			launchDevice.CurrentGenerator.IsFixed = false;
			launchDevice.CurrentGenerator = null;
		}


		if (emitterLaunchCore.currentLaunchDevice is StraightLaunchDevice)
		{
			this.transform.gameObject.transform.localEulerAngles = Vector3.zero;
		}

		if(Input.GetKeyDown(KeyCode.Q))
		{
			this.isClose = !this.isClose;
		}
    }
	
}

