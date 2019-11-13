using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

[RequireComponent(typeof(Rigidbody))]
[DefaultExecutionOrder(500)]
public class Enemy_MiddleBoss : character_status
{
	public StateType type;
	[Header("移動パラメータ（単位：Unit/秒）")]
	public float advanceBackSpeed;
	public float escapeSpeed;

	public float escapeSpeedIncrease = 25;

	[Header("何回目行動時　突撃行動　（単位：回目）")]
	public List<int> advanceActionSlot = new List<int>();
	[Header("何回目行動時　逃走行動　（単位：回目）")]
	public int excapeActionSlot;
	private int currentSlot;
	private int backActionSlot;
	[Header("行動持続時間　（単位：秒）")]
	public float waitDuration = 5;
	public float debutDuration = 2f;
	public float moveDuration = 1;
	public float advanceBackDuration = 2f;
	public float stopDuration = 1f;
	public float escapeDuration = 2f;

	[Header("移動限界Y座標（単位：Unit）")]
	public float limitYUp = -5f;
	public float limitYDown = -12f;

	[Header("弾発射位置調整")]
	public float frontBulletOffetY;
	public float backBulletOffetY;
	public float bulletsDistance;
	public Vector2 bulletCreatLocalPos;

	[Header("微調整")]
	public bool canFirstShot = false;

	private Vector3 moveDirection;
	private StateManager<StateType> stateManager;

	private CapsuleCollider _capsuleCollider;   
    [SerializeField] private List<BoxCollider> childsBoxColliders = new List<BoxCollider>();
    [SerializeField] private List<BoxCollider> bodyColliders = new List<BoxCollider>();
    private Enemy_DestroyParts[] partsInstances = new Enemy_DestroyParts[4];
	private Rigidbody rb;
	private Transform player;
	private Animator animator;
	private bool canAdvanceAttack;

	public ParticleSystem explosionEffect;
	public ParticleSystem blackSmokeEffect;
	private int rotateCount = 0;

	[SerializeField]private int currentDestroyPartIndex = 0;

	private void Awake()
	{
		stateManager = new StateManager<StateType>();
		rb = GetComponent<Rigidbody>() ;
		animator = GetComponentInChildren<Animator>();
		_capsuleCollider = GetComponent<CapsuleCollider>();

		rb.useGravity = false;

		var state = new StateBase<StateType>(moveDuration, StateType.MOVE);
		state.EnterCallBack = Move_Enter;
		state.UpdateCallBack = Move_Update;
		state.ExitCallBack = Move_Exit;
		stateManager.Add(state);

		state = new StateBase<StateType>(waitDuration, StateType.WAIT);
        state.EnterCallBack = Wait_Enter;
		state.UpdateCallBack = Wait_Update;
		stateManager.Add(state);

		state = new StateBase<StateType>(debutDuration, StateType.DEBUT);
		state.EnterCallBack = Debut_Enter;
		state.UpdateCallBack = Debut_Update;
		state.ExitCallBack = Debut_Exit;
		stateManager.Add(state);

		state = new StateBase<StateType>(advanceBackDuration, StateType.ADVANCE_AND_BACK);
		state.EnterCallBack = AdvanceBack_Enter;
		state.UpdateCallBack = AdvanceBack_Update;
		stateManager.Add(state);


		state = new StateBase<StateType>(stopDuration, StateType.STOP);
		state.EnterCallBack = Stop_Enter;
		state.UpdateCallBack = Stop_Update;
		state.ExitCallBack = Stop_Exit;
		stateManager.Add(state);

		state = new StateBase<StateType>(escapeDuration, StateType.ESCAPE);
		state.EnterCallBack = Escape_Enter;
		state.UpdateCallBack = Escape_Update;
		stateManager.Add(state);

		state = new StateBase<StateType>(5f, StateType.DEATH);
		state.EnterCallBack = Death_Enter;
		state.UpdateCallBack = Death_Update;
		state.ExitCallBack = Death_Exit;
		stateManager.Add(state);

		currentDestroyPartIndex = childsBoxColliders.Count - 1;
	}

	// Start is called before the first frame update
	private new void Start()
    {
		base.HP_Setting();

		player = GameObject.Find("Player").transform.GetChild(0).transform;
		if(player.gameObject.activeSelf)
		{
			player = GameObject.Find("Player_2").transform.GetChild(0).transform;
		}
		base.Start();
		stateManager.Start(StateType.WAIT);
	}

	// Update is called once per frame
	private new void Update()
    {

		if (transform.position.x < -30.0f)
		{
			this.gameObject.SetActive(false);
		}

		if (player.gameObject.activeSelf)
		{
			stateManager.Update();
			type = stateManager.Current.StateType;
		}
		else
		{
			
			player = GameObject.Find("Player").transform.GetChild(0).transform;
			if(!player.gameObject.activeSelf)
			{
				player = GameObject.Find("Player_2").transform.GetChild(0).transform;
			}		
		}

		base.Update();
		if (hp >= 1)
		{			
			if (stateManager.Current.StateType != StateType.DEBUT)
			{
				if (currentDestroyPartIndex >= 0)
				{
					if (capsuleCollider.enabled)
						capsuleCollider.enabled = false;

					for (var i = 0; i < childsBoxColliders.Count; ++i)
					{
						if (i == currentDestroyPartIndex)
						{
							childsBoxColliders[i].enabled = true;
							if (!childsBoxColliders[i].gameObject.activeSelf)
							{
								currentDestroyPartIndex--;
							}
						}
						else
						{
							if (childsBoxColliders[i].gameObject.activeSelf)
								childsBoxColliders[i].enabled = false;
						}
					}
				}
				else
				{
					if (!capsuleCollider.enabled)
						capsuleCollider.enabled = true;
				}
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		
	}

	private void Wait_Enter()
    {
        for(var i = 0; i < childsBoxColliders.Count; ++i)
        {
            childsBoxColliders[i].enabled = false;
        }
		for (var i = 0; i < bodyColliders.Count; ++i)
		{
			bodyColliders[i].enabled = false;
		}
		_capsuleCollider.enabled = false;
    }

	private void Wait_Update()
	{
		if (stateManager.Current.IsDone)
		{
			stateManager.ChangeState(StateType.DEBUT);
			return;
		}
	}

	private void Debut_Enter()
	{
		DebugManager.OperationDebug("中ボス出場!", "中ボス");
		animator.Play("Debut");
        for (var i = 0; i < bodyColliders.Count; ++i)
        {
            bodyColliders[i].enabled = false;
        }
          for (var i = 0; i < childsBoxColliders.Count; ++i)
        {
            childsBoxColliders[i].enabled = false;
        }
		_capsuleCollider.enabled = false;
	}

	private void Debut_Update()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
		{
			stateManager.ChangeState(StateType.STOP);
			return;
		}
	}

	private void Debut_Exit()
	{
		var temp = transform.GetChild(0).position;
		transform.position = temp;
		transform.GetChild(0).localPosition = Vector3.zero;
		animator.enabled = false;

		for (var i = 0; i < bodyColliders.Count; ++i)
        {
            bodyColliders[i].enabled = true;
        }
        for (var i = 0; i < childsBoxColliders.Count; ++i)
        {
            childsBoxColliders[i].enabled = true;
        }
		_capsuleCollider.enabled = true;
	}

	private void Move_Enter()
	{
		currentSlot++;
		rotateCount++;

		if (transform.position.y > limitYUp)
		{
			moveDirection = Vector3.down;
			DebugManager.OperationDebug("中ボス移動!" + " 移動方向：" + moveDirection.ToString(), "中ボス");
			return;
		}

		if (transform.position.y < limitYDown)
		{
			moveDirection = Vector3.up;
			DebugManager.OperationDebug("中ボス移動!" + " 移動方向：" + moveDirection.ToString(), "中ボス");
			return;
		}

		if (player.transform.position.y >= transform.position.y)
		{
			moveDirection = Vector3.up;
		}
		else
		{
			moveDirection = Vector3.down;
		}
		DebugManager.OperationDebug("中ボス移動!" + " 移動方向：" + moveDirection.ToString(), "中ボス");
	}

	private void Move_Update()
	{
		var rotatePreFrame = 180.0f / (moveDuration / Time.deltaTime);
		var smoothStepRotate = Mathf.SmoothStep(rotatePreFrame * 0.5f, rotatePreFrame * 1.5f, (moveDuration - stateManager.Current.Timer) / (moveDuration));
		if (moveDirection == Vector3.down)
			transform.Rotate(Vector3.left * smoothStepRotate);
		if (moveDirection == Vector3.up)
			transform.Rotate(Vector3.right * smoothStepRotate);


		if (stateManager.Current.IsDone && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
		{
			stateManager.ChangeState(StateType.STOP);
			return;
		}




		//if (moveDirection == Vector3.down)
		//      transform.localEulerAngles = Vector3.Slerp(new Vector3(180, 0 ,0), new Vector3(0, 0, 0), (moveDuration -  stateManager.Current.Timer)/ moveDuration);

		//if (moveDirection == Vector3.up)
		//      transform.localEulerAngles = Vector3.Slerp(new Vector3(0, 0 ,0), new Vector3(180, 0, 0), (moveDuration -  stateManager.Current.Timer) / moveDuration);


		var movePreFrame = speed * moveDirection * Time.deltaTime / moveDuration;
		var SmoothStepMove = Mathf.SmoothStep(0, 2 * movePreFrame.y, (moveDuration - stateManager.Current.Timer) / (moveDuration));

		transform.position += new Vector3(0, SmoothStepMove, 0);
	}

	private void Move_Exit()
	{
		
	}

	private void AdvanceBack_Enter()
	{
		DebugManager.OperationDebug("中ボス前に出る", "中ボス");
		canAdvanceAttack = true;
		currentSlot += 2;
	}

	private void AdvanceBack_Update()
	{
		if (hp < 1)
		{
			stateManager.ChangeState(StateType.DEATH);
			return;
		}

		if (stateManager.Current.IsDone)
		{
			stateManager.ChangeState(StateType.STOP);
			return;
		}

		if (stateManager.Current.Timer >= 0.625 * stateManager.Current.Duration)
		{
			moveDirection = Vector3.left;
		}
		else if (stateManager.Current.Timer >= 0.375 * stateManager.Current.Duration)
		{
			if (canAdvanceAttack)
			{
				DebugManager.OperationDebug("中ボス前に出て弾を出す", "中ボス");
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(bulletCreatLocalPos.x, backBulletOffetY + bulletCreatLocalPos.y, 0), Vector3.left);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(bulletCreatLocalPos.x - bulletsDistance, frontBulletOffetY + bulletCreatLocalPos.y, 0), Vector3.left);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(bulletCreatLocalPos.x - bulletsDistance, -frontBulletOffetY + bulletCreatLocalPos.y, 0), Vector3.left);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(bulletCreatLocalPos.x, -backBulletOffetY + bulletCreatLocalPos.y, 0), Vector3.left);
				canAdvanceAttack = false;
			}
			moveDirection = Vector3.zero;
		}
		else 
		{
			moveDirection = Vector3.right;
		}

		transform.position += advanceBackSpeed * moveDirection * Time.deltaTime;
	}


	private void Escape_Enter()
	{
		DebugManager.OperationDebug("中ボス退場", "中ボス");
		//-------------------------7/20修正--------------------------
		//逃亡時コライダー修正
		//実はコアの部分を大きくする
		//細かく調整が必要あるが、今はこれで
		//-----------------------------------------------------------
		//capsuleCollider.height = 8;
		//capsuleCollider.center = new Vector2(1.5f,0.09f);

		animator.enabled = true;
		animator.Play("Escape");

		moveDirection = Vector3.left;
		base.hp = 10000;
	}

	private void Escape_Update()
	{
		if (hp < 1)
		{
			stateManager.ChangeState(StateType.DEATH);
			return;
		}

		transform.position += escapeSpeed * moveDirection * Time.deltaTime;

		if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.1f)
		{
			escapeSpeed += escapeSpeedIncrease * Time.deltaTime;
		}
	}

	private void Stop_Enter()
	{
		DebugManager.OperationDebug("中ボス弾出す", "中ボス");
		moveDirection = Vector3.zero;
		if(!canFirstShot && currentSlot == 0)
			return;

		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(bulletCreatLocalPos.x, backBulletOffetY + bulletCreatLocalPos.y, 0), Vector3.left);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(bulletCreatLocalPos.x - bulletsDistance, frontBulletOffetY + bulletCreatLocalPos.y, 0), Vector3.left);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(bulletCreatLocalPos.x - bulletsDistance, -frontBulletOffetY + bulletCreatLocalPos.y, 0), Vector3.left);
		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, transform.position + new Vector3(bulletCreatLocalPos.x, -backBulletOffetY + bulletCreatLocalPos.y, 0), Vector3.left);
	}

	private void Stop_Update()
	{
		if (hp < 1)
		{
			stateManager.ChangeState(StateType.DEATH);
			return;
		}

		if (stateManager.Current.IsDone)
		{
			if ((advanceActionSlot.Count > 0 && currentSlot != advanceActionSlot[0] || advanceActionSlot.Count == 0) && currentSlot != excapeActionSlot && currentSlot != backActionSlot || (backActionSlot == 0 && currentSlot == backActionSlot))
			{
				stateManager.ChangeState(StateType.MOVE);
				return;
			}

			if (advanceActionSlot.Count > 0 && currentSlot == advanceActionSlot[0])
			{
				stateManager.ChangeState(StateType.ADVANCE_AND_BACK);
				backActionSlot = advanceActionSlot[0] + 1;
				advanceActionSlot.RemoveAt(0);
				return;
			}

			if (currentSlot == excapeActionSlot)
			{
				stateManager.ChangeState(StateType.ESCAPE);
				return;
			}
		}
	}

	private void Stop_Exit()
	{
		
	}

	private void Death_Enter()
	{
		var changeTransfrom = transform.GetChild(0).GetChild(6);
		for (var i = 0; i < changeTransfrom.childCount; ++i)
		{
			if(i < changeTransfrom.childCount - 4)
				changeTransfrom.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Explosion"); 
			else
				changeTransfrom.GetChild(i).GetChild(0).gameObject.layer = LayerMask.NameToLayer("Explosion");
		}
		DebugManager.OperationDebug("中ボス破壊", "中ボス");
		animator.enabled = true;
		animator.Play("Death");
		capsuleCollider.enabled = false;
		explosionEffect.gameObject.SetActive(true);
		blackSmokeEffect.gameObject.SetActive(true);

		//
		Game_Master.MY.Score_Addition(score, Opponent);
		SE_Manager.SE_Obj.SE_Explosion(Obj_Storage.Storage_Data.audio_se[22]);

		Debug.Log(transform.localEulerAngles);
		//爆発位置調整
		if (rotateCount % 2 == 1)
		{
			explosionEffect.transform.localPosition = new Vector3(explosionEffect.transform.localPosition.x, explosionEffect.transform.localPosition.y, 3);
		}
		else
		{
			explosionEffect.transform.localPosition = new Vector3(explosionEffect.transform.localPosition.x, explosionEffect.transform.localPosition.y, -3);
		}		

        base.BossRemainingBouns(2);
	}

	private void Death_Update()
	{
		if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.65f)
		{
			transform.position += speed * 0.1f * Time.deltaTime * Vector3.down;
		}

		if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
		{	
			Is_Dead = true;
			Reset_Status();
			this.gameObject.SetActive(false);
			explosionEffect.gameObject.SetActive(false);
		}
	}

	private void Death_Exit()
	{

	}
}
