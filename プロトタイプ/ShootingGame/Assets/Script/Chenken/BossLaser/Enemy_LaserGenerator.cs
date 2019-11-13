using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(590)]
[RequireComponent(typeof(LineRenderer))]
public class Enemy_LaserGenerator : MonoBehaviour
{
	private float shotSpeed;
	private GameObject emitter;

	private bool isFixed;
	public bool IsFixed { get { return isFixed; } set { isFixed = value; } }

	private LineRenderer lineRenderer;
	private Game_Master.OBJECT_NAME laserName;
	private GameObject laserLinePrefab;
	public List<Enemy_LaserLine> nodes;

	private int pointMax;
	private int pointCount;
	public bool IsOverLoad
	{
		get
		{
			return this.pointCount > this.pointMax;
		}
	}

	private float s_width;
	private Vector3 shotDirection;

	private void Awake()
	{
		this.isFixed = true;
		this.lineRenderer = GetComponent<LineRenderer>();
		this.nodes = new List<Enemy_LaserLine>();
		this.emitter = GameObject.Find("Boss_LaserEmitter");
	}

	private void Update()
	{
		//--------------------------------------------------ループチェック-------------------------------------------------------------
		//管理しているレーザー構成オブジェクト個数回実行する
		for (var i = 0; i < this.nodes.Count; ++i)
		{
			//現在検索オブジェクトは非アクティブの場合
			if (!this.nodes[i].gameObject.activeSelf)
			{
				//管理しないようにする
				this.nodes.Remove(nodes[i]);
				//検索位置調整する
				i--;
				//管理オブジェクト個数を調べ、管理個数は0の場合
			}
			//アクティブ状態の場合
			else
			{
				//位置合わせTrue場合、強制的に管理オブジェクトの位置を修正する

					this.nodes[i].FixedDirection = shotDirection;
					this.nodes[i].transform.position = shotDirection * nodes[i].Frame * nodes[i].shot_speed;
				
			}

			if (this.nodes.Count == 0)
			{
				this.nodes.Clear();                               //念のため、管理リストクリアする
				this.ResetGenerator();
				this.ResetLineRenderer();

				this.gameObject.SetActive(false);                 //当オブジェクトを非アクティブ状態に
			}
		}
		//------------------------------------------------------------------------------------------------------------------------------


		//----------------------------------------レーザー　線　レンダリング--------------------------------------------------------------------------

		//if (this.nodes.Count >= 2)
		//{
		//	this.SetLineRenderer(new Vector3(this.nodes[0].transform.position.x, this.nodes[0].transform.position.y, 0)
		//						, new Vector3(this.nodes[nodes.Count - 1].transform.position.x, this.nodes[nodes.Count - 1].transform.position.y, 0));
		//}

		if (this.nodes.Count > 2)
		{

			this.lineRenderer.positionCount = this.nodes.Count;

			for (var i = 0; i < this.nodes.Count; ++i)
			{
				this.lineRenderer.SetPosition(i, this.nodes[i].transform.position);
			}
		}
		else
		{
			ResetLineRenderer();
		}

		//if (this.lineRenderer.positionCount == 2 && this.lineRenderer.GetPosition(0) == Vector3.zero && this.lineRenderer.GetPosition(1) == Vector3.zero)
		//{
		//	if(this.nodes.Count == 0)
		//		this.gameObject.SetActive(false);

		//}


		//---------------------------------------------------------------------------------------------------------------------------------------------	
	}

	/// <summary>
	/// 設定方法
	/// </summary>
	/// <param name="shotSpeed"> レーザー連結点発射速度  </param>
	/// <param name="lineWidth"> レーザー線太さ          </param>
	/// <param name="nodePrefab">レーザー連結点プレハブ  </param>
	/// <param name="material">  レーザー線マテリアル    </param>
	public void Setting(GameObject laserLinePrefab, Game_Master.OBJECT_NAME laserName, float shotSpeed, float lineWidth, Material material, int pointMax)
	{
		this.shotSpeed = shotSpeed;
		this.laserLinePrefab = laserLinePrefab;
		this.laserName = laserName;
		this.lineRenderer.startWidth = lineWidth;
		this.lineRenderer.endWidth = lineWidth;
		this.lineRenderer.material = material;
		this.pointMax = pointMax;
	}

	/// <summary>
	/// レーザー連結点発射（生成）する
	/// </summary>
	public void LaunchNode( float trailWidth, bool isFixed)
	{
		Enemy_LaserLine node = null;
		node = CreateNode(transform.position, this.emitter.transform.rotation, trailWidth, isFixed);

		//管理するように
		this.nodes.Add(node);
		this.pointCount++;

		//if (nodes.Count != 0)
		//{
		//	var last = this.nodes[this.nodes.Count - 1];
		//	var pos = (last.transform.position + transform.position) / 2;
		//	var rotation = Average(new Quaternion[] { last.transform.localRotation, transform.localRotation });
		//	node = CreateNode(pos, rotation, trailWidth);
		//	this.nodes.Add(node);
		//	this.pointCount++;
		//}
	}

	/// <summary>
	/// レンダリング設定
	/// </summary>
	/// <param name="pos"> 一番目はレーザー頭位置、一番後ろはレーザー尾位置 </param>
	private void SetLineRenderer(params Vector3[] pos)
	{
		for (var i = 0; i < pos.Length; ++i)
		{
			this.lineRenderer.SetPosition(i, pos[i]);
		}
	}

	public void ResetLineRenderer()
	{
		this.lineRenderer.positionCount = 2;
		this.lineRenderer.SetPosition(0, Vector3.zero);
		this.lineRenderer.SetPosition(1, Vector3.zero);
	}

	public void ResetGenerator()
	{
		pointCount = 0;
	}

	private Enemy_LaserLine CreateNode(Vector3 pos, Quaternion rotation, float trailWidth, bool isFixed)
	{
		// node.GetComponent<Enemy_LaserLine>() は最初にローカル変数にとっておいたほうがいいよ
		// 毎回 GetComponent すると重いよ
		var nodeGo = Instantiate(laserLinePrefab, pos, Quaternion.identity);
		if(nodeGo == null)
		{
			nodeGo = StorageReference.Object_Instantiation.Object_Reboot(laserName, pos, Quaternion.identity);
		}
		var node = nodeGo.GetComponent<Enemy_LaserLine>();
		node.shot_speed = this.shotSpeed;

		var t_x = Mathf.Cos((transform.parent.parent.parent).localEulerAngles.z * Mathf.Deg2Rad);
		var t_y = Mathf.Sin((transform.parent.parent.parent).localEulerAngles.z * Mathf.Deg2Rad);

		var direction = new Vector3(t_x, t_y, 0);
		shotDirection = direction;

		Debug.Log(direction);
		node.FixedDirection = direction;
		node.IsFixed = isFixed;
		node.Travelling_Direction = direction;
		
		node.TrailRenderer.Clear();
		node.TrailRenderer.endWidth = trailWidth;
		node.TrailRenderer.startWidth = trailWidth;
		return node;
	}

}

