using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDragAndFollowTarget : MonoBehaviour, IDragHandler
{
	RectTransform rectTransform = null;
	[SerializeField] Transform target = null;

	[SerializeField]
	Canvas canvas;

	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponent<Graphic>().canvas;
	}

	void IDragHandler.OnDrag(PointerEventData ev)
	{
		Vector3 worldPos;
		var ray = RectTransformUtility.ScreenPointToRay(Camera.main, ev.position);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			target.position = hit.point + hit.normal * 0.5f;
		}
	}

	void Update()
	{
		var pos = Vector2.zero;
		var uiCamera = Camera.main;
		var worldCamera = Camera.main;
		var canvasRect = canvas.GetComponent<RectTransform>();

		var screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, target.position);
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera, out pos);
		rectTransform.localPosition = pos;
	}
}