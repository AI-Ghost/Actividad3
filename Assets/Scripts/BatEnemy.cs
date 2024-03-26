using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    public float speed = 1.19f;
    Vector3 pointA;
    Vector3 pointB;
    public int attackDamage = 10;
    public Transform attackPoint;
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    public GameObject Bullet;
    public float minTime = 5f;
    public float maxTime = 15f;


    void Start()
    {
        StartCoroutine(SpawnCoRutine(0));
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    IEnumerator SpawnCoRutine(float waitTime)
    {
        yield return new WaitForSeconds(minTime);
        Instantiate(Bullet, transform.position, Quaternion.identity);
        StartCoroutine(SpawnCoRutine(Random.Range(minTime, maxTime)));
    }

    public void TakeDamage()
    {

        Destroy(gameObject);

    }
    void OnTriggerExit2D(Collider2D other)
    {

        moveSpeed = -moveSpeed;
        FlipEnemyFacing();

    }

    void FlipEnemyFacing()
    {

        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(transform.position, transform.position, time);
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
        GetDirection();
    }

    public float GetDirection()
    {
        float direction = Mathf.Sign(myRigidbody.velocity.x);
        transform.localScale = new Vector2(direction, 1f);
        return direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameSession>().TakeEnemyHit(attackDamage);
        }
    }

}