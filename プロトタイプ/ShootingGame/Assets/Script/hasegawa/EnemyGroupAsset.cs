using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "EnemyGroupAsset", menuName = "ScriptableObject/EnemyGroup", order = 0)]
public class EnemyGroupAsset : ScriptableObject
{
	public List<EnemyCreate.EnemyGroup> enemyGroups = new List<EnemyCreate.EnemyGroup>();
}

#if UNITY_EDITOR
namespace ScriptableCreater
{
	public class CreaterEnemyGroupAsset
	{
		[MenuItem("ScriptableObject/EnemyGroup")]
		public static void Create()
		{
			EnemyGroupAsset enemyGroupAsset = ScriptableObject.CreateInstance<EnemyGroupAsset>();
			AssetDatabase.CreateAsset(enemyGroupAsset, "Assets/EnemyGroupAsset.asset");
		}
	}
}
#endif
