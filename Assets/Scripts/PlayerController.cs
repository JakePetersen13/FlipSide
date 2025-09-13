using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsBase
{
    public float moveSpeed = 3f;
    public float flipBoost = 5f;
    public float direction;

    public float updatespriteWaitTime;
    private Quaternion targetRotation;
    public float flipRotateSpeed = 10f;

    public Transform sprite;
    private SpriteRenderer sr;

    void Start()
    {
        sr = sprite.GetComponent<SpriteRenderer>();
        targetRotation = Quaternion.identity;
        Updatesprite();
    }

    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        desiredX = direction != 0 ? moveSpeed * direction : 0;

        // Gravity flip on Space
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            FlipGravity();
        }
        else if (grounded)
        {
            velocity.y = 0f;
        }

        sprite.localRotation = Quaternion.Lerp(sprite.localRotation, targetRotation, Time.deltaTime * flipRotateSpeed);
    }

    void FlipGravity()
    {
        gravityDirection *= -1; 
        velocity.y = flipBoost * -gravityDirection;

        StartCoroutine(waitToUpdatesprite(updatespriteWaitTime)); //give player time to move away from colliders
    }

    void Updatesprite()
    {

        Color32 lightBlue = new Color32(0, 128, 255, 255);
        Color32 lightRed = new Color32(255, 51, 51, 255);

        if (gravityDirection == 1)
        {
            sr.color = lightBlue;
            targetRotation = Quaternion.identity;
            sprite.localPosition = new Vector3(0f, 0f, 0f);
        }
        else
        {
            sr.color = lightRed;
            targetRotation = Quaternion.Euler(0, 0, 180);
            sprite.localPosition = new Vector3(0f, 0.3f, 0f); //rotation axis of triangle means it must shift up to line up with collider
        }
    }

    public int GetGravityDirection()
    {
        return gravityDirection;
    }

    IEnumerator waitToUpdatesprite(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Updatesprite();
    }
}
