using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 20f;
    public int attackDamage = 5;
    private Rigidbody2D myRigidbody2D;
    private GameSession gameSession;

    private BatEnemy batScript;
    private float direction;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        gameSession = FindObjectOfType<GameSession>();
        //batScript = GetComponent<BatEnemy>();
        Destroy(gameObject, 2f);
        //direction = batScript.GetDirection();

        // Find the BatEnemy GameObject and get its direction
        GameObject batEnemyObject = GameObject.FindGameObjectWithTag("Enemy");
        if (batEnemyObject != null)
        {
            BatEnemy batScript = batEnemyObject.GetComponent<BatEnemy>();
            if (batScript != null)
            {
                direction = batScript.GetDirection();
            }
        }
    }


    void Update()
    {
        myRigidbody2D.velocity = new Vector2(direction * speed, myRigidbody2D.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the bullet, not the player
            gameSession.TakeEnemyHit(attackDamage);
        }
    }
}
