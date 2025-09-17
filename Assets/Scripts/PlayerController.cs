using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject deathParticlesPrefab;

    void Start()
    {
        sr = sprite.GetComponent<SpriteRenderer>();
        targetRotation = Quaternion.identity;
        UpdateSprite();
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

        UpdateSprite();
    }

    void UpdateSprite()
    {

        Color32 lightBlue = new Color32(0, 28, 255, 255);
        Color32 lightRed = new Color32(255, 0, 4, 255);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes"))
        {;
            GameObject particles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);

            var main = particles.GetComponent<ParticleSystem>().main;
            main.startColor = sr.color;

            Destroy(gameObject);
            FindObjectOfType<LevelTransition>().LoadNextLevel(SceneManager.GetActiveScene().name);
        }
    }

    public void DisablePlayer()
    {
        GetComponent<PlayerController>().enabled = false;
    }

}
