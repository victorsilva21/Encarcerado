using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAMP1 : MonoBehaviour
{
    Rigidbody2D rb;

    public float moveSpeed;
    public float esquerda;
    public float direita;


    bool movimento;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movimento = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void ChangeMoveDir()
    {
        if (transform.rotation.z > direita)
        {
            movimento = false;
        }
        if (transform.rotation.z < esquerda)
        {
            movimento = true;
        }
    }


    public void Move()
    {
        ChangeMoveDir();

        if (movimento)
        {
            rb.angularVelocity = moveSpeed;
        }

        if (!movimento)
        {
            rb.angularVelocity = -1 * moveSpeed;
        }
    }
}
