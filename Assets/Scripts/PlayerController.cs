using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsBase
{
    public float moveSpeed = 3f;
    public float flipBoost = 5f;
    public float direction;

    public float updateVisualsWaitTime;
    private Quaternion targetRotation;
    public float flipRotateSpeed = 10f;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        targetRotation = Quaternion.identity;
    UpdateVisuals();
    }

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

        // Gravity flip on Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipGravity();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * flipRotateSpeed);
    }

    void FlipGravity()
    {
        gravityDirection *= -1; 
        velocity.y = flipBoost * -gravityDirection;

        StartCoroutine(waitToUpdateVisuals(updateVisualsWaitTime)); //give player time to move away from colliders
    }

    void UpdateVisuals()
    {
        // Color
        sr.color = (gravityDirection == 1) ? Color.blue : Color.red;

        // Rotation target (don’t apply immediately, lerp in Update)
        targetRotation = (gravityDirection == 1) ? Quaternion.identity : Quaternion.Euler(0, 0, 180);
    }

    public int GetGravityDirection()
    {
        return gravityDirection;
    }

    IEnumerator waitToUpdateVisuals(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        UpdateVisuals();
    }
}
