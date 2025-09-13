using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBase : MonoBehaviour
{

    public Vector2 velocity;
    public float gravityFactor;
    public float desiredX;
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Movement(Vector2 velocity, bool horizontal)
    {
        if (velocity.magnitude < 0.000001f) return;
        grounded = false;

        RaycastHit2D[] collisions = new RaycastHit2D[16];
        int colCount = GetComponent<Rigidbody2D>().Cast(velocity, collisions, velocity.magnitude + 0.001f);

        for (int i = 0; i < colCount; ++i)
        {
            if (Mathf.Abs(collisions[i].normal.x) > 0.3f && horizontal)
            {
                return;
            }
            if (Mathf.Abs(collisions[i].normal.y) > 0.3f && !horizontal)
            {
                if (collisions[i].normal.y > 0.3f)
                {
                    grounded = true;
                }
                return;
            }
        }

        transform.position += (Vector3)velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 gravity = Vector2.zero;
        if (!grounded) gravity = 9.81f * Vector2.down * gravityFactor;

        velocity += gravity * Time.fixedDeltaTime;
        velocity.x = desiredX;

        Vector2 movement = velocity * Time.fixedDeltaTime;
        Movement(new Vector2(movement.x, 0), true);
        Movement(new Vector2(0, movement.y), false);
    }
}
