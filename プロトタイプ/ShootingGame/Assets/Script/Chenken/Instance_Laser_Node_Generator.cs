using System;
using System.Collections.Generic;
using UnityEngine;

 [DefaultExecutionOrder(590)]
[RequireComponent(typeof(LineRenderer))]
public class Instance_Laser_Node_Generator : MonoBehaviour
{
	private float shotSpeed;
	private GameObject emitter;

	private bool isFixed;
	public bool IsFixed { get { return isFixed; } set { isFixed = value;} }

	private LineRenderer lineRenderer;

	public List<GameObject> nodes;

	private int pointMax;
	public int pointCount;
	public bool IsOverLoad
	{
		get
		{
			return this.pointCount > this.pointMax;
		}
	}

	private float s_width;


	private void Awake()
	{
		this.isFixed      = true;
		this.lineRenderer = GetComponent<LineRenderer>();
		this.nodes        = new List<GameObject>();
		this.emitter      = GameObject.Find("LaserEmitter");
	}


	private void Update()
	{

		//------------------------------------------------------------------------------------------------------------------------------


		//----------------------------------------レーザー　線　レンダリング--------------------------------------------------------------------------

		//if (this.nodes.Count >= 2)
		//{
		//	this.SetLineRenderer(new Vector3(this.nodes[0].transform.position.x, this.nodes[0].transform.position.y, 0)
		//						, new Vector3(this.nodes[nodes.Count - 1].transform.position.x, this.nodes[nodes.Count - 1].transform.position.y, 0));
		//}

        //node　2　以上

        if( Wireless_sinario.Is_using_wireless)
        {
             for (int i = 29; i < Obj_Storage.Storage_Data.Laser_Line.Get_Obj().Count; i++)
             {
                if(!Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i].gameObject.activeSelf)
                { 
                    if(this.nodes.Contains(Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i]))
                        this.nodes.Remove(Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i]);
                    Destroy(Obj_Storage.Storage_Data.Laser_Line.Get_Obj()[i]);
                    Obj_Storage.Storage_Data.Laser_Line.Get_Obj().RemoveAt(i);
                }
             }
        }
        else
        {
            for (var i = 0; i < this.nodes.Count; ++i)
		    {
                if(nodes[i] == null)
                {
                    nodes.Remove(nodes[i]);
                    i--;
                    continue;
                }
                

			    //現在検索オブジェクトは非アクティブの場合
			    if(!this.nodes[i].gameObject.activeSelf)
			    {
                    if(i == 0)
                    {
                        this.nodes[i].transform.position = new Vector3(0,-20,0);
                    }
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
				    if (this.isFixed)
					    this.nodes[i].transform.position = new Vector3(this.nodes[i].transform.position.x, this.transform.position.y, 0);
			    }

			    if (this.nodes.Count == 0)
			    {
				    this.ResetGenerator();
				    this.ResetLineRenderer();

				    this.gameObject.SetActive(false);                 //当オブジェクトを非アクティブ状態に
			    }
		    }

        }
        if (this.nodes.Count > 2)
		  {
		      this.lineRenderer.SetPosition(0, this.nodes[0].transform.position);
              this.lineRenderer.SetPosition(1, this.nodes[nodes.Count - 1].transform.position);

		  }
          //node 1
		  else if(this.nodes.Count > 0)
		  {

              this.lineRenderer.SetPosition(0,this.nodes[0].transform.position - Vector3.right);
              this.lineRenderer.SetPosition(1,this.nodes[0].transform.position);

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
	public void Setting(float shotSpeed, float lineWidth,Material material,int pointMax)
	{
		this.shotSpeed                = shotSpeed;
		this.lineRenderer.startWidth  = lineWidth;
		this.lineRenderer.endWidth    = lineWidth;
		this.lineRenderer.material    = material;
		this.pointMax                 = pointMax;
	}

	/// <summary>
	/// レーザー連結点発射（生成）する
	/// </summary>
	public void LaunchNode(float trailWidth, bool isRotateLaser)
	{
		GameObject node = null;
		node = CreateNode(transform.position, this.emitter.transform.localEulerAngles, trailWidth, isRotateLaser);

		//管理するように
		this.nodes.Add(node);
		this.pointCount++;

		if (nodes.Count > 1 && isRotateLaser)
		{
			var last = this.nodes[this.nodes.Count - 1];
			var lastlast = this.nodes[this.nodes.Count - 2];
			var pos = lastlast.transform.position + (last.transform.position - lastlast.transform.position) * 0.33f;
			var rotation = lastlast.transform.localEulerAngles + (last.transform.localEulerAngles - lastlast.transform.localEulerAngles) * 0.33f;
			node = CreateNode(pos, rotation, trailWidth,isRotateLaser);

			last = this.nodes[this.nodes.Count - 1];
			lastlast = this.nodes[this.nodes.Count - 2];
			pos = lastlast.transform.position + (last.transform.position - lastlast.transform.position) * 0.67f;
			rotation = lastlast.transform.localEulerAngles + (last.transform.localEulerAngles - lastlast.transform.localEulerAngles) * 0.67f;
			node = CreateNode(pos, rotation, trailWidth, isRotateLaser);
		}
	}

	/// <summary>
	/// レンダリング設定
	/// </summary>
	/// <param name="pos"> 一番目はレーザー頭位置、一番後ろはレーザー尾位置 </param>
	private void SetLineRenderer(params Vector3[] pos)
	{
		for(var i = 0; i < pos.Length; ++i)
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

	private GameObject CreateNode(Vector3 pos, Vector3 rotation, float trailWidth, bool isRotateLaser)
	{
		var node = StorageReference.Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_LASER, pos, Quaternion.identity);


		if(transform.parent.parent.parent.name == "Player")
		{
			node.name = "Player_Laser";
		}
		else if(transform.parent.parent.parent.name == "Player_2")
		{
			node.name = "Player2_Laser";
		}
		else if(transform.parent.parent.parent.name == "Option")
		{
			if(transform.parent.parent.parent.GetComponent<Bit_Formation_3>().bState == Bit_Formation_3.BitState.Player1)	
				node.name = "Option_Player1_Laser";

			if(transform.parent.parent.parent.GetComponent<Bit_Formation_3>().bState == Bit_Formation_3.BitState.Player2)
				node.name = "Option_Player2_Laser";
		}

		//if (transform.parent.parent.parent.name == "Player" || (transform.parent.parent.parent.GetComponent<Bit_Formation_3>() != null && transform.parent.parent.parent.GetComponent<Bit_Formation_3>().bState == Bit_Formation_3.BitState.Player1))
		//{
		//	node.GetComponent<LaserLine>().IsPlayer1Laser = true;
		//	node.GetComponent<LaserLine>().IsPlayer2Laser = false;
		//}
		//if (transform.parent.parent.parent.name == "Player_2" || (transform.parent.parent.parent.GetComponent<Bit_Formation_3>() != null && transform.parent.parent.parent.GetComponent<Bit_Formation_3>().bState == Bit_Formation_3.BitState.Player2))
		//{
		//	node.GetComponent<LaserLine>().IsPlayer2Laser = true;
		//	node.GetComponent<LaserLine>().IsPlayer1Laser = false;
		//}

		//node.GetComponent<bullet_status>().shot_speed = this.shotSpeed;
		//node.transform.localEulerAngles = rotation;
		//node.GetComponent<bullet_status>().Travelling_Direction = node.transform.right;
		//node.GetComponent<LaserLine>().TrailRenderer.Clear();
		//node.GetComponent<LaserLine>().TrailRenderer.endWidth = trailWidth;
		//node.GetComponent<LaserLine>().TrailRenderer.startWidth = trailWidth;

		//if(node.GetComponent<PauseComponent>() == null)
		//	node.AddComponent<PauseComponent>();

		return node;
	}


}

