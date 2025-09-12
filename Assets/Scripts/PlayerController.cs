using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsBase
{
    public float moveSpeed = 3f;
    public float jumpHeight = 6.5f;
    public float direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");

        if (direction != 0)
        {
            desiredX = moveSpeed * direction;
        }
        else
        {
            desiredX = 0;
        }

        if (Input.GetButton("Jump") && grounded) velocity.y = jumpHeight;
        else if (grounded) velocity.y = 0f;
    }
}
