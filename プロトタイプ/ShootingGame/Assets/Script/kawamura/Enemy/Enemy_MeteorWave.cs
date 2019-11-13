using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeteorWave : character_status
{
    public GameObject meteor_One;
    public GameObject meteor_Two;
    public GameObject meteor_Three;
    public GameObject meteor_Four;
    public GameObject meteor_Five;

    public float speedX;
    public float speedY;
    public float rotaZ;
    public float rotaZ_ChangeValue;
    public int curveCnt;
	public string myName;

    Vector3 velocity;

    public bool isInc = false;
    public bool isDec = false;

    new void Start()
    {
        meteor_One = GameObject.Find("Enemy_Meteor_One");
        meteor_Two = GameObject.Find("Enemy_Meteor_Two");
        meteor_Three = GameObject.Find("Enemy_Meteor_Three");
        meteor_Four = GameObject.Find("Enemy_Meteor_Four");
        meteor_Five = GameObject.Find("Enemy_Meteor_Five");

		myName = gameObject.name;

        curveCnt = 0;
		rotaZ = 0;
        base.Start();
    }

    new void Update()
    {
		if(myName == "Enemy_MeteorWave")
		{
			velocity = gameObject.transform.rotation * new Vector3(0, 0, speedX);
			gameObject.transform.position += velocity * Time.deltaTime;
		}
		else
		{
			velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
		}

		if (myName == "Enemy_MeteorWave")
		{
			transform.rotation = Quaternion.Euler(rotaZ, -90, 90);

		}
		else
		{
			transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotaZ);

		}

        if (curveCnt == 0 && transform.position.x < meteor_Five.transform.position.x)
        {
            curveCnt++;
            isInc = true;
        }
        else if (curveCnt == 1 && transform.position.x < meteor_Four.transform.position.x + 2.1)
        {
            curveCnt++;
            isInc = false;
            isDec = true;

        }
        else if (curveCnt == 2 && transform.position.x < meteor_Three.transform.position.x)
        {
            curveCnt++;
            isDec = false;
            isInc = true;
        }
        else if (curveCnt == 3 && transform.position.x < meteor_Two.transform.position.x + 0.5)
        {
            curveCnt++;
            isInc = false;
            isDec = true;
        }
		else if (curveCnt == 4 && transform.position.x < meteor_One.transform.position.x )
		{
			curveCnt++;
			isDec = false;
			isInc = true;
		}

		if (isInc)
        {
            rotaZ += rotaZ_ChangeValue;
			if (curveCnt == 1)
			{
				if (rotaZ > 47)
				{
					rotaZ = 47;
					isInc = false;
				}

			}
			else if (curveCnt == 3)
			{
				if (rotaZ > 0)
				{
					rotaZ = 0;
					isInc = false;
				}
			}
			else if (curveCnt == 5)
			{
				if (rotaZ > 0)
				{
					rotaZ = 0;
					isInc = false;
				}
			}
        }
        else if(isDec)
        {
            rotaZ -= rotaZ_ChangeValue;
            if (curveCnt == 2)
            {
                if (rotaZ < -20)
                {
                    rotaZ = -20;
                    isDec = false;
                }
            }
            if (curveCnt == 4)
            {
                if (rotaZ < -32)
                {
                    rotaZ = -32;
                    isDec = false;
                }
            }
        }
    }
}
