using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MoaiGroup : MonoBehaviour
{
    public float rotaY;
    public float rotaY_Value;
    public float rollCnt;
    public float saveRotaY;

    public bool isRoll;

    void Start()
    {
        saveRotaY = rotaY;
    }

    void Update()
    {
        if (isRoll)
        {
            rotaY += rotaY_Value;
            if (rotaY > saveRotaY + 90)
            {
                saveRotaY += 90;
                rotaY = saveRotaY;
                isRoll = false;
            }
            transform.rotation = Quaternion.Euler(0, rotaY, 0);
        }
    }
}
