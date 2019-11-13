using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerUpComponent : MonoBehaviour
{
    [SerializeField] private float enlargeScale = 1.3f;
    [SerializeField] private float targetScale;
	[SerializeField] private float currentScale;

	public float changeTime = 1f;
	[SerializeField]private float changeTimer;
	[SerializeField]private bool isResize = false;
    [SerializeField]private bool isEnlarge = false;

	private Image image;

	private void Awake()
	{
		currentScale = targetScale = 1f;
		image = GetComponent<Image>();
	}

	public Sprite Sprite
	{
		get
		{
			return image.sprite;
		}
		set
		{
			image.sprite = value;
		}
	}



	// Update is called once per frame
    void Update()
    {
        if(targetScale > currentScale)
        {
            if(isResize)
            {
                isResize = false;
                changeTimer = 0;
            }
            isEnlarge = true;
        }
        else if(targetScale < currentScale)
        {
             if(isEnlarge)
            {
                isEnlarge = false;
                changeTimer = 0;
            }
            isResize = true;
        }
        else
        {
            isEnlarge = false;
            isResize = false;
            changeTimer = 0;
        }

        if(isResize || isEnlarge)
		{
			var scale =  0f;
            if(isEnlarge)
                scale = Mathf.Lerp(currentScale, targetScale - 0.01f, changeTimer / changeTime);
            if(isResize)
                scale = Mathf.Lerp(currentScale, targetScale + 0.01f, changeTimer / changeTime);
			transform.localScale = new Vector3(scale, scale, scale);
			changeTimer += Time.deltaTime;
			if(changeTimer > changeTime)
			{
				changeTimer = changeTime;
                 if(isEnlarge)
                    currentScale = enlargeScale;
                 if(isResize)
                    currentScale = 1f;
				transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                changeTimer = 0f;
                targetScale = currentScale;
			}
		}
    }

	public void Enlarge()
	{
  
        currentScale = 1f;
		targetScale = enlargeScale + 0.01f;


	}

	public void Resize()
	{
		if(isResize)
		{
			return;
		}

        currentScale = transform.localScale.x;
		targetScale = 1f - 0.01f;
	}

    private void Reset()
    {
        isEnlarge = false;
        isResize = false;
        changeTimer = 0;
    }

	public void Black()
	{
		image.material = null;
		image.color = new Color(0.65f, 0.65f, 0.65f, 1);
	}

	public void Blight(Material material)
	{
		image.material = material;
		image.color = new Color(1, 1, 1, 1);
	}
}
