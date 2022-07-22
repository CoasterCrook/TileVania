using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] AudioClip enemyDeathSound;
    Rigidbody2D myRigidBody;
    PlayerMovement player;
    float xSpeed;
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
        transform.localScale = new Vector2((Mathf.Sign(xSpeed)) * transform.localScale.x, transform.localScale.y);
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2 (xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Enemy")
        {
            AudioSource.PlayClipAtPoint(enemyDeathSound,Camera.main.transform.position, volume: .5f);
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
}
