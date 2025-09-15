using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 10;
    public GameObject coinParticlesPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(scoreValue);
            GameObject particles = Instantiate(coinParticlesPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
            Destroy(particles.gameObject, 1f);
        }
    }
}
