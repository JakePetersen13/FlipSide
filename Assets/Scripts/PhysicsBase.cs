using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsBase : MonoBehaviour
{
    public Vector2 velocity;
    public float gravityFactor = 1f;
    public int gravityDirection = 1; //1 = normal (down), -1 = reversed (up)
    public float desiredX;
    public bool grounded;

    public GameObject playerObject;

    void Start()
    {
    }

    void Movement(Vector2 velocity, bool horizontal)
    {
        if (velocity.magnitude < 0.000001f) return;
        grounded = false;

        RaycastHit2D[] collisions = new RaycastHit2D[16];
        int colCount = GetComponent<Rigidbody2D>().Cast(velocity, collisions, velocity.magnitude + 0.0001f);

        for (int i = 0; i < colCount; ++i)
        {
            Collider2D col = collisions[i].collider;

            if (!CanCollideWith(col))
                continue;

            if (Mathf.Abs(collisions[i].normal.x) > 0.3f && horizontal)
            {
                return;
            }
            if (Mathf.Abs(collisions[i].normal.y) > 0.3f && !horizontal)
            {
                // Check ground depending on gravity direction
                if (gravityDirection == 1 && collisions[i].normal.y > 0.3f)
                {
                    grounded = true;
                    velocity.y = 0f;
                }
                else if (gravityDirection == -1 && collisions[i].normal.y < -0.3f)
                {
                    grounded = true;
                    velocity.y = 0f;
                }
                return;
            }
        }

        transform.position += (Vector3)velocity;
    }

    void FixedUpdate()
    {
        Vector2 gravity = Vector2.zero;

        if (!grounded)
        {
            gravity = 9.81f * gravityDirection * Vector2.down * gravityFactor;
        }

            velocity += gravity * Time.fixedDeltaTime;
        velocity.x = desiredX;

        Vector2 movement = velocity * Time.fixedDeltaTime;
        Movement(new Vector2(movement.x, 0), true);
        Movement(new Vector2(0, movement.y), false);
    }

    bool CanCollideWith(Collider2D col)
    {
        string tag = col.tag;

        if (tag == "WhitePlatform") return true;
        if (gravityDirection == 1 && tag == "BluePlatform") return true;
        if (gravityDirection == -1 && tag == "RedPlatform") return true;

        if (tag == "Spikes")
        {
            Destroy(playerObject);
        }

        return false;
    }
}
