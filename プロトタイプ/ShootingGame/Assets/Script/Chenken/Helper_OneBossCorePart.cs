using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_OneBossCorePart : MonoBehaviour
{
	private One_Boss_Parts partScript;
	[SerializeField] private GameObject partInstance;
	[SerializeField] private Texture2D red_baseColorTexture;
	[SerializeField] private Texture2D red_emissiveTexture;
	[SerializeField] private ParticleSystem particleSystem;
	[SerializeField] private float transitionLimitHpPercent = 0.3f;
	[SerializeField] private int coreHpMax;
	private MeshRenderer renderer;
	private bool change = false;
	private bool hasChanged = false;
	private Material redMaterial;


	private void Start()
	{
		partScript = GetComponent<One_Boss_Parts>();
		renderer = partInstance.GetComponent<MeshRenderer>();
		coreHpMax = partScript.hp;
		change = false;
		hasChanged = false;
	}


	private void Update()
    {
        if(partScript.hp <= transitionLimitHpPercent * coreHpMax)
		{
			change = true;
		}

		if (!change)
			return;

		renderer.material.SetTexture("_BaseColorMap", red_baseColorTexture);
		renderer.material.SetTexture("_EmissiveColorMap", red_emissiveTexture);

		//Hp > limitHp || isCHanged  return 
		if (hasChanged)
			return;

		if (particleSystem != null)
		{
			particleSystem.transform.position = transform.position;
			particleSystem?.Stop();
			particleSystem?.Play();
			hasChanged = true;
		}
    }
}
