using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Helper_SceneLoadAsync : MonoBehaviour
{
	private static bool isAutoLoading = false;
	AsyncOperation async;   
	[SerializeField] private string SceneName;
	[SerializeField] private float loadingSpeed = 1;

	private float targetValue;
	private float progress;

	private void Start()
	{
		progress = 0f;
		StartCoroutine("LoadScene");
	}

	private void Update()
	{
		if (async == null) return;

		targetValue = async.progress;


		if (async.progress >= 0.9f)
		{
			targetValue = 1.0f;
		}

		if (targetValue != progress)
		{
			progress = Mathf.Lerp(progress, targetValue, Time.deltaTime * loadingSpeed);

			if (Mathf.Abs(progress - targetValue) < 0.01f)
			{

				progress = targetValue;
			}
		}


		if ((int)(progress * 100) == 100)
		{
			async.allowSceneActivation = true;
			isAutoLoading = false;
		}
	}

	private IEnumerator LoadScene()
	{
		yield return new WaitForEndOfFrame();

		async = SceneManager.LoadSceneAsync(SceneName);

		async.allowSceneActivation = false;

		print("Loading:" + async);

		yield return async;
	}

	public static void TranslateToScene()
	{
		isAutoLoading = true;
	}
}


