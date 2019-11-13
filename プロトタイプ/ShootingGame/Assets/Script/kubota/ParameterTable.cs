using UnityEngine;

[CreateAssetMenu(menuName = "Paramater/ParameterTable", fileName = "ParameterTable")]
public class ParameterTable : ScriptableObject
{
	[SerializeField, Header("初期体力")]
	private int life;
	public int Get_Life { get { return life; } }

	[SerializeField, Header("初期速度")]
	private float Speed;
	public float Get_Speed { get { return Speed; } }

	[SerializeField, Header("残機数")]
	private int Reaming;
	public int Get_Reaming { get { return Reaming; } }

	[SerializeField, Header("シールドの値")]
	private int Shield;
	public int Get_Shield { get { return Shield; } }

	[SerializeField, Header("スコア設定")]
	private uint Score;
	public uint Get_Score { get { return Score; } }


}
