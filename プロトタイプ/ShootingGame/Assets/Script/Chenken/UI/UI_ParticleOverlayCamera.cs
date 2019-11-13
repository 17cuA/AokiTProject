using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_2018_3_OR_NEWER && UNITY_EDITOR
using PrefabStageUtility = UnityEditor.Experimental.SceneManagement.PrefabStageUtility;
#endif

[ExecuteInEditMode]
	public class UI_ParticleOverlayCamera : MonoBehaviour
	{
		//################################
		// Public/Protected Members.
		//################################
		/// <summary>
		/// Get instance object.
		/// If instance does not exist, Find instance in scene, or create new one.
		/// </summary>
		public static UI_ParticleOverlayCamera instance
		{
			get
			{
#if UNITY_2018_3_OR_NEWER && UNITY_EDITOR
				// If current scene is prefab mode, create OverlayCamera for editor.
				var prefabStage = PrefabStageUtility.GetCurrentPrefabStage ();
				if (prefabStage != null && prefabStage.scene.isLoaded)
				{
					if (!s_InstanceForPrefabMode)
					{
						// This GameObject is not saved in prefab.
						// This GameObject is not shown in the hierarchy view.
						// When you exit prefab mode, this GameObject is destroyed automatically.
						var go = new GameObject (typeof (UI_ParticleOverlayCamera).Name + "_ForEditor")
						{
							hideFlags = HideFlags.HideAndDontSave,
							tag = "EditorOnly",
						};
						UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene (go, prefabStage.scene);
						s_InstanceForPrefabMode = go.AddComponent<UI_ParticleOverlayCamera> ();
					}
					return s_InstanceForPrefabMode;
				}
#endif

				// Find instance in scene, or create new one.
				if (object.ReferenceEquals (s_Instance, null))
				{
					s_Instance = FindObjectOfType<UI_ParticleOverlayCamera> () ?? new GameObject (typeof (UI_ParticleOverlayCamera).Name, typeof (UI_ParticleOverlayCamera)).GetComponent<UI_ParticleOverlayCamera> ();
					s_Instance.gameObject.SetActive (true);
					s_Instance.enabled = true;
				}
				return s_Instance;
			}
		}

		public static Camera GetCameraForOvrelay (Canvas canvas)
		{
			var i = instance;
			var rt = canvas.rootCanvas.transform as RectTransform;
			var cam = i.cameraForOvrelay;
			var trans = i.transform;
			cam.enabled = false;

			var pos = rt.localPosition;
			cam.orthographic = true;
			cam.orthographicSize = Mathf.Max (pos.x, pos.y);
			cam.nearClipPlane = 0.3f;
			cam.farClipPlane = 1000f;
			pos.z -= 100;
			trans.localPosition = pos;

			return cam;
		}

		//################################
		// Private Members.
		//################################
		Camera cameraForOvrelay { get { return m_Camera ? m_Camera : (m_Camera = GetComponent<Camera> ()) ? m_Camera : (m_Camera = gameObject.AddComponent<Camera> ()); } }
		Camera m_Camera;
		static UI_ParticleOverlayCamera s_Instance;
#if UNITY_2018_3_OR_NEWER && UNITY_EDITOR
		static UI_ParticleOverlayCamera s_InstanceForPrefabMode;
#endif

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		void Awake ()
		{
#if UNITY_2018_3_OR_NEWER && UNITY_EDITOR
			// OverlayCamera for editor.
			if (hideFlags == HideFlags.HideAndDontSave || s_InstanceForPrefabMode == this)
			{
				s_InstanceForPrefabMode = GetComponent<UI_ParticleOverlayCamera> ();
				return;
			}
#endif

			// Hold the instance.
			if (s_Instance == null)
			{
				s_Instance = GetComponent<UI_ParticleOverlayCamera> ();
			}
			// If the instance is duplicated, destroy itself.
			else if (s_Instance != this)
			{
				UnityEngine.Debug.LogWarning ("Multiple " + typeof (UI_ParticleOverlayCamera).Name + " in scene.", this.gameObject);
				enabled = false;
#if UNITY_EDITOR

				if (!Application.isPlaying)
				{
					DestroyImmediate (gameObject);
				}
				else
#endif
				{
					Destroy (gameObject);
				}
				return;
			}

			cameraForOvrelay.enabled = false;

			// Singleton has DontDestroy flag.
			if (Application.isPlaying)
			{
				DontDestroyOnLoad (gameObject);
			}
		}

		/// <summary>
		/// This function is called when the MonoBehaviour will be destroyed.
		/// </summary>
		void OnDestroy ()
		{
#if UNITY_2018_3_OR_NEWER && UNITY_EDITOR
			if (s_InstanceForPrefabMode == this)
			{
				s_InstanceForPrefabMode = null;
			}
#endif

			// Clear instance on destroy.
			if (s_Instance == this)
			{
				s_Instance = null;
			}
		}
	}
