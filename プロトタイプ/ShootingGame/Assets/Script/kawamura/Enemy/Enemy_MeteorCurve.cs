//作成者：川村良太
//右上や右下から来て左側に行くとカーブしてまた右に戻っていく挙動

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeteorCurve : character_status
{
    public enum State
    {
        TurnUp,
        TurnDown,
    }

    State eState;

    Vector3 velocity;

    public Vector3 startMarker;
    public Vector3 endMarker;
    public Vector3 defaultPos;
    float startTime;
    float present_Location;
    public float testSpeed = 1.0f;

    private float distance_two;


    public float speedX;
    public float SpeedY;
    public float rotaZ;
    public float rotaZ_ChangeValue;

    public bool isSlerp;
    public bool isTurn;
    public bool isSpeedXInc;
    public bool isSpeedXDec;
    public bool isSpeedYInc;
    public bool isSpeedYDec;

    new void Start()
    {
        if (transform.position.y > 0)
        {
            eState = State.TurnDown;
        }
        else
        {
            eState = State.TurnUp;
        }

        startMarker = new Vector3(-10.0f, transform.position.y, 0);
        endMarker = new Vector3(-10.0f, -transform.position.y, 0);

        distance_two = Vector3.Distance(startMarker, endMarker);
        defaultPos = transform.position;

        rotaZ_ChangeValue = 1.63f;
        base.Start();
    }

    new void Update()
    {

        velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
        gameObject.transform.position += velocity * Time.deltaTime;

        //if (isSlerp)
        //{
        //    present_Location = (Time.time * testSpeed) / distance_two;
        //    transform.position = Vector3.Slerp(startMarker, endMarker, startTime);
        //    startTime += Time.deltaTime;

        //    if (transform.position == endMarker)
        //    {
        //        isSlerp = false;
        //        speedX *= -1;
        //    }

        //}
        if (isTurn)
        {
            if (eState == State.TurnDown)
            {
                rotaZ += rotaZ_ChangeValue;
                if (rotaZ > 180)
                {
                    rotaZ = 180;
                    isTurn = false;
                    //isTurn = false;
                }
            }
            else if (eState == State.TurnUp)
            {
                rotaZ -= rotaZ_ChangeValue;
                if (rotaZ < -180)
                {
                    rotaZ = -180;
                    isTurn = false;
                    //isTurn = false;
                }

            }
            transform.rotation = Quaternion.Euler(0, 0, rotaZ);
        }
        else if (transform.position.x < -10)
        {
            isSlerp = true;
            isTurn = true;
        }
        //else
        //{
        //    velocity = gameObject.transform.rotation * new Vector3(speedX, 0, 0);
        //    gameObject.transform.position += velocity * Time.deltaTime;

        //}
    }

}
