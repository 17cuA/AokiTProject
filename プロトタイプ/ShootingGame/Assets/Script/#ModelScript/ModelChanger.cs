/*
 * 20191111 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一つのモデルのメッシュやテクスチャをAssetBundleから切り替える
/// アタッチするときは変えたいモデルの一番上の階層にアタッチする
/// </summary>
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class ModelChanger : MonoBehaviour
{
	[SerializeField] string assetBundlePath = "AssetBundle/player";	// 読み込むAssetBundleのパス
	const string kTextureName = "Albedo";							// 取得したAssetBundleから読み込むテクスチャの名前
	const string kShaderTextureName = "_BaseColorMap";				// テクスチャを反映させるシェーダー側のプロパティの名前
	const string kModelName = "Model";								// 取得したAssetBundleから読み込むモデルの名前
	void Awake()
	{
		// AssetBundleを取得する
		AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
		// AssetBundleが取得できなかったら終わる
		if (!assetBundle)
		{
			return;
		}
		Texture2D importTexture = assetBundle.LoadAsset<Texture2D>(kTextureName);
		GameObject importModel = assetBundle.LoadAsset<GameObject>(kModelName);
		assetBundle.Unload(false);
		// テクスチャが読み込めなかったら差し替えずに終わる
		if (!importTexture)
		{
			Debug.LogError("Could not load Texture named " + kTextureName);
			return;
		}
		// モデルが読み込めなかったら差し替えずに終わる
		if (!importModel)
		{
			Debug.LogError("Could not load Model named " + kModelName);
			return;
		}
		// 元からあるレンダラーをoffにする
		MeshRenderer[] defaultModelRenderers = GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer mr in defaultModelRenderers)
		{
			mr.enabled = false;
		}
		// 自身の情報を取得する
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		// AssetBundleから情報を取得する
		MeshRenderer importMeshRenderer = importModel.GetComponent<MeshRenderer>();
		MeshFilter importMeshFilter = importModel.GetComponent<MeshFilter>();
		// 取得した情報を反映させる
		meshRenderer.material = importMeshRenderer.sharedMaterial;
		meshRenderer.material.SetTexture(kShaderTextureName, importTexture);
		// メッシュの反映
		meshFilter.mesh = importMeshFilter.sharedMesh;
		meshRenderer.enabled = true;
		// 角度を0にする
		transform.rotation = Quaternion.identity;
	}
}
