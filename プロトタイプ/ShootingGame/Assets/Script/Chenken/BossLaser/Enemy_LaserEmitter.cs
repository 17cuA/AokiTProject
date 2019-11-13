using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChenkenLaser;
using UnityEngine;
using UnityEngine.Events;
using StorageReference;

[DefaultExecutionOrder(599)]
public class Enemy_LaserEmitter : MonoBehaviour
{
	public bool isClose = false;
	//--------------------------- 直線型レーザー（タイプ１） ----------------------------------
	[Header("--------直線型レーザー--------")]
	[SerializeField]  private float straightLaserShotSpeed = 0.8f;
	[SerializeField]  private float straightLaserWidth = 0.05f;
	[SerializeField]  private int straightLaserNodeMax = 50;
	[SerializeField]  private float straightLaserOverloadDuration = 0.4f;
	[SerializeField]  private float straightLaserLaunchInterval = 0.01f;
	[SerializeField]  private float straightTrailWidth = 0.1f;
	[SerializeField]  private Material straightLaserMaterial;
	[SerializeField]  private Game_Master.OBJECT_NAME laserName;
    public GameObject laserLinePrefab;

	[SerializeField] private bool isFire;
	public bool IsFire
	{
		get
		{
			return isFire;
		}
		set
		{
			isFire = value;
		}
	}
	private GameObject straightLaserGeneratorParent;


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

		public void GenerateLine(GameObject laserLinePrefab, Game_Master.OBJECT_NAME laserName, float laserShotSpeed, float laserWidth, Material laserMaterial, int pointMax)
		{
			currentLaunchDevice.GenerateLine(laserLinePrefab, laserName, laserShotSpeed, laserWidth, laserMaterial, pointMax);
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
		Enemy_LaserGenerator CurrentGenerator { get; set; }
		List<Enemy_LaserGenerator> generators { get; set; }

		float OverloadDuration { get; set; }
		float LaunchInterval { get; set; }
		float CanLaunchTime { get; set; }
		GameObject EmitterInstance { get; set; }

		void GenerateLine(GameObject laserLinePrefab, Game_Master.OBJECT_NAME laserName, float laserShotSpeed, float laserWidth, Material laserMaterial, int pointMax);
		void LaunchNode(float trailWidth);
	}

	/// <summary>
	/// 直線発射部品
	/// </summary>
	public class StraightLaunchDevice : ILaunchDevice
	{
		public float OverloadDuration { get; set; }
		public float LaunchInterval { get; set; }
		public float CanLaunchTime { get; set; }
		public GameObject EmitterInstance { get; set; }

		public Enemy_LaserGenerator CurrentGenerator { get; set; }
		public List<Enemy_LaserGenerator> generators { get; set; }

		public StraightLaunchDevice(float overloadDuration, float launchInterval, GameObject emitterInstance)
		{
			this.OverloadDuration = overloadDuration;
			this.LaunchInterval = launchInterval;
			this.CanLaunchTime = 0f;
			this.EmitterInstance = emitterInstance;

			this.CurrentGenerator = null;
			this.generators = new List<Enemy_LaserGenerator>();
		}

		public void GenerateLine(GameObject laserLinePrefab, Game_Master.OBJECT_NAME laserName, float laserShotSpeed, float laserWidth, Material laserMaterial, int pointMax)
		{
			Debug.Log("StraightLaserGeneratorアクティブ");
			for (var i = 0; i < this.generators.Count; ++i)
			{
				if (!this.generators[i].gameObject.activeSelf)
				{
					this.CurrentGenerator = this.generators[i];
					this.CurrentGenerator.ResetLineRenderer();
					this.CurrentGenerator.IsFixed = true;
					this.CurrentGenerator.gameObject.SetActive(true);
					return;
				}
			}

			var generatorGo = new GameObject("Generator");
			var generator = generatorGo.AddComponent<Enemy_LaserGenerator>();

			generator.Setting(laserLinePrefab,laserName, laserShotSpeed, laserWidth, laserMaterial, pointMax);
			generator.IsFixed = true;

			generatorGo.transform.SetParent(EmitterInstance.transform);
			generatorGo.transform.localPosition = Vector3.zero;

			this.CurrentGenerator = generator;
			this.generators.Add(generator);
		}

		public void LaunchNode(float trailWidth)
		{
			this.CurrentGenerator.LaunchNode(trailWidth, false);
			this.CanLaunchTime = Time.time + LaunchInterval;
		}
	}


	private EmitterRotateCore emitterRotateCore;
	private EmitterLaunchCore emitterLaunchCore;


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
		this.emitterRotateCore = new EmitterRotateCore(this.transform.parent.position);
		this.emitterLaunchCore = new EmitterLaunchCore(new StraightLaunchDevice(this.straightLaserOverloadDuration, this.straightLaserLaunchInterval, this.straightLaserGeneratorParent));
	}

	private void Start()
	{
	
	}

	private void Update()
	{
		var launchDevice = emitterLaunchCore.currentLaunchDevice;
		if (this.isClose)
		{
			if (launchDevice is StraightLaunchDevice)
			{
				var num = transform.GetChild(0).childCount;
				for (var i = 0; i < num; ++i)
				{
					if (transform.GetChild(0).GetChild(i).gameObject.activeSelf)
					{
						return;
					}
				}
				this.gameObject.SetActive(false);
			}
		}
		else
		{
			if (launchDevice != null)
			{
				if (isFire)
				{
					if (launchDevice.CurrentGenerator == null)
					{
						this.emitterLaunchCore.GenerateLine(laserLinePrefab, laserName, straightLaserShotSpeed, straightLaserWidth, straightLaserMaterial, straightLaserNodeMax);
					}

					if (Time.time >= launchDevice.CanLaunchTime && launchDevice.CurrentGenerator != null)
					{
						this.emitterLaunchCore.LaunchNode(straightTrailWidth);
					}
				}

				if (!isFire && launchDevice.CurrentGenerator != null)
				{
					launchDevice.CurrentGenerator.IsFixed = false;
					launchDevice.CurrentGenerator = null;
				}
			}
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------


		//------------------------------------------------------------レーザー発射装置使いすぎ------------------------------------------------------
		if (launchDevice.CurrentGenerator != null && launchDevice.CurrentGenerator.IsOverLoad)
		{
			launchDevice.CurrentGenerator.IsFixed = false;
			launchDevice.CurrentGenerator = null;
			this.emitterLaunchCore.GenerateLine(laserLinePrefab, laserName, straightLaserShotSpeed, straightLaserWidth, straightLaserMaterial, straightLaserNodeMax);

			launchDevice.CanLaunchTime = Time.time + launchDevice.OverloadDuration;
		}
		//------------------------------------------------------------------------------------------------------------------------------------------------

		if (!this.transform.parent.gameObject.activeSelf && launchDevice.CurrentGenerator != null)
		{
			launchDevice.CurrentGenerator.IsFixed = false;
			launchDevice.CurrentGenerator = null;
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			IsFire = !IsFire;
		}
	}
}

