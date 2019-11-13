using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_s : MonoBehaviour
{
    public Rigidbody rigidbodyaa;
    public float up;
    public float down;
    public float speed;
    public bool UPOK;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyaa = GetComponent<Rigidbody>();
        rigidbodyaa.velocity = transform.forward.normalized * speed;
        UPOK = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector = rigidbodyaa.velocity;

        if (UPOK)
        {
            vector.y += up;

            if(transform.position.y >= 3.0f)
            {
                UPOK = false;
            }
        }
        else if(!UPOK)
        {
            vector.y += down;

            if (transform.position.y <= -3.0f)
            {
                UPOK = true;
            }
        }

        rigidbodyaa.velocity = vector;
        transform.forward = rigidbodyaa.velocity;
    }
}
