using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_MaterialBlight : MonoBehaviour
{
	private Renderer render;
	private Color startMaterialMaterial;
	public Color settingMaterialColor;

	private Color currentMaterialColor;
	private Color targetMaterialColor;

	[SerializeField] private float changeTime;
	[Header("0から~までの範囲でランダム")]
	[SerializeField] [Range(0,3)] private float randomStartDelayRange = 2;
	[SerializeField] private float startDelay;
	private float startTimer;
	private float changeTimer;

	private bool isBlightToDark;
	private void Awake()
	{
		render = GetComponent<Renderer>();
	}
	// Start is called before the first frame update
	private void Start()
    {
		startMaterialMaterial = render.material.GetColor("_EmissiveColor");

		currentMaterialColor = startMaterialMaterial;
		targetMaterialColor = settingMaterialColor;
		isBlightToDark = true;

		startDelay = UnityEngine.Random.Range(0, randomStartDelayRange);
    }

	// Update is called once per frame
	private void Update()
	{
		if(startTimer >= startDelay)
		{
			if(startTimer != startDelay) startTimer = startDelay;
			changeTimer += Time.deltaTime;
		}
		else
		{
			startTimer += Time.deltaTime;
		}

		if (changeTimer >= changeTime)
		{
			isBlightToDark = !isBlightToDark;
			changeTimer = 0;
			return;
		}

		if(isBlightToDark)
		{
			if (currentMaterialColor != startMaterialMaterial) currentMaterialColor = startMaterialMaterial;
			if (targetMaterialColor != settingMaterialColor) targetMaterialColor = settingMaterialColor;
		}
		else
		{
			if (currentMaterialColor != settingMaterialColor) currentMaterialColor = settingMaterialColor;
			if (targetMaterialColor != startMaterialMaterial) targetMaterialColor = startMaterialMaterial;
		}

		var color = Color.Lerp(currentMaterialColor, targetMaterialColor, changeTimer / changeTime);
		render.material.SetColor("_EmissiveColor", color);

	}
}
