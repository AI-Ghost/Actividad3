using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;


public class EnemyMovement : MonoBehaviour
{
   //[SerializeField] float moveSpeed = 1f;
   Rigidbody2D myRigidbody;

   Animator myAnimator;

   private System.Action[] enemyStates ;

    void Start()
    {
        enemyStates = new System.Action[]
        {

            Idle,
            Attack,
        };
        
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        StartCoroutine(UpdateState()); 
        
    }

    
    void Update()
    {
        //Attack();
        //StartCoroutine(UpdateState()); 
        //UpdateState();
           //myRigidbody.velocity = new Vector2(moveSpeed,0f);
    }
    

    void Idle(){

         myAnimator.SetBool("isIdle", true);
         myAnimator.SetBool("isAttack", false);
          Debug.Log("Idle Call");
    }

    void Attack()
    {
        
        //myRigidbody.velocity = new Vector2(moveSpeed,0f);
        myAnimator.SetBool("isAttack", true);
        Debug.Log("Attack Call");
        myAnimator.SetBool("isIdle", false);
        
         
    }

    IEnumerator UpdateState()
    {
        while(true){
            yield return new WaitForSeconds(Random.Range(1, 5)); 
           int index = Random.Range(0, enemyStates.Length);
            if( enemyStates[index] != null ) enemyStates[index]();
            
            Debug.Log("UpdateState Call");
    }
        }

         


    /*void OnTriggerExit2D(Collider2D other) {

        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
        
    }*/

    void FlipEnemyFacing()
    {

        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

    
}
