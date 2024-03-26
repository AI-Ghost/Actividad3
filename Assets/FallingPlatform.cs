using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

   [SerializeField] private float fallDelay = 1f;
   [SerializeField] private float respawnTime = 2f;
    [SerializeField] private Rigidbody2D rb;

    Vector2 defaultPos;


    void Start() {

        defaultPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.CompareTag("Player")){
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall(){
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(respawnTime);
        Reset();
        //Destroy(gameObject,destroyDelay);
    }

    private void Reset() {

        rb.bodyType = RigidbodyType2D.Static;
        transform.position = defaultPos;

        
    }

   
}
