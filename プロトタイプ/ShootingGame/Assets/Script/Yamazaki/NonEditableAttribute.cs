// 20190930
// 山嵜
// 概要：private 変数をInspectorに表示し編集不可にする
// URL：https://kandycodings.jp/2019/03/24/unidev-noneditable/

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class NonEditableAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(NonEditableAttribute))]
public sealed class NonEditableAttributeDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return EditorGUI.GetPropertyHeight(property, label, true);
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		GUI.enabled = false;
		EditorGUI.PropertyField(position, property, label, true);
		GUI.enabled = true;
	}
}
#endif