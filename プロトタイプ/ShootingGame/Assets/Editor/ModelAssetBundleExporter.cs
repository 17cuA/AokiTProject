using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR
public static class ModelAssetBundleExporter
{
	[MenuItem("AssetBundle/ExportAssetBundle")]
	private static void Create()
	{
		// 出力先
		string exportPath = "AssetBundle";
		// ディレクトリが存在しなければ作成する
		if (!Directory.Exists(exportPath))
		{
			Directory.CreateDirectory(exportPath);
		}
		// ビルド
		BuildPipeline.BuildAssetBundles(exportPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
		EditorUtility.DisplayDialog("Dialog", "アセットバンドルビルド終了", "OK");
	}
}
#endif
