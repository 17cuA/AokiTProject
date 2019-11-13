using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Board_Quater_Parent : MonoBehaviour
{

	public GameObject quarterObj;

	GameObject saveQuaterObj;

	public Quaternion randRota_TopLeft;
	public Quaternion randRota_TopRight;
	public Quaternion randRota_BottomLeft;
	public Quaternion randRota_BottomRight;

	public float speedX;

	Vector3 velocity;

	public bool isDead = false;
    void Start()
    {
        
    }

    void Update()
    {
		velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
		gameObject.transform.position += velocity * Time.deltaTime;
		speedX -= 1.0f;
		if (speedX < 0)
		{
			speedX = 0;
		}

		randRota_TopLeft = new Quaternion(0, 0, Random.Range(180, 270), 0);
		randRota_TopRight = new Quaternion(0, 0, Random.Range(270, 360), 0);
		randRota_BottomLeft = new Quaternion(0, 0, Random.Range(0, 90), 0);
		randRota_BottomRight = new Quaternion(0, 0, Random.Range(90, 180), 0);
		if (isDead)
		{
			//randRota_TopLeft = new Quaternion(0, 0, Random.Range(0, 360), 0);
			//Instantiate(quarterObj, transform.position, randRota_TopLeft);
			//randRota_TopLeft = new Quaternion(0, 0, Random.Range(0, 360), 0);
			//Instantiate(quarterObj, transform.position, randRota_TopRight);
			//randRota_TopLeft = new Quaternion(0, 0, Random.Range(0, 360), 0);
			//Instantiate(quarterObj, transform.position, randRota_BottomLeft);
			//randRota_TopLeft = new Quaternion(0, 0, Random.Range(0, 360), 0);
			//Instantiate(quarterObj, transform.position, randRota_BottomRight);

			saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(180, 270));
			//saveQuaterObj.transform.localScale = new Vector3(1, 0.5f, 0.5f);

			saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(270, 360));
			//saveQuaterObj.transform.localScale = new Vector3(1, 0.5f, 0.5f);

			saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
			//saveQuaterObj.transform.localScale = new Vector3(1, 0.5f, 0.5f);

			saveQuaterObj = Instantiate(quarterObj, transform.position, transform.rotation);
			saveQuaterObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(90, 180));
			//saveQuaterObj.transform.localScale = new Vector3(1, 0.5f, 0.5f);

			isDead = false;

			gameObject.SetActive(false);
		}


	}
}
