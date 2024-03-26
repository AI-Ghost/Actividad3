using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 1f;
    [SerializeField] float jumpSpeed = 20f;

    [SerializeField] float levelLoadDelay = 3f;

    [Header ("Attack Properties")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask attackMask;


    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;

    BoxCollider2D myFeetCollider;

    private CinemachineImpulseSource _myImpulseSource;
    private AudioSource audioSource;

    bool isAlive = true;

    //float delay = 0.3f;
    //private bool attackedBlocked;

    //float gravityScaleAtStart;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        _myImpulseSource = GetComponent<CinemachineImpulseSource>();
        audioSource = GetComponent<AudioSource>();
        //gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive){return;}

        Run();
        FlipSprite();
        Death();
    }

        private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }



    void OnMove(InputValue value){

        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);

    }

    void Run(){
        if(!isAlive){return;}
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

    }

    void Death(){

        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask(/*"Enemies",*/ "Hazards","Fall Death"))){
            isAlive = false;
            StartCoroutine(PlayerDeathLoad());
           // myAnimator.SetTrigger("Dying");
            //_myImpulseSource.GenerateImpulse(1);
            //FindObjectOfType<GameSession>().ResetGameSession();
             //FindObjectOfType<GameSession>().ProcessPlayerDeath();
             
        }

    }

    public void EnemyDeath(){
        
            isAlive = true;
            myAnimator.SetTrigger("Hurt");

            _myImpulseSource.GenerateImpulse(1);
              //myAnimator.ResetTrigger("Hurt");
              //Debug.Log("Hurt");

    }

    public void ResetAnim(){
        myAnimator.ResetTrigger("Hurt");
         //Debug.Log("Reset Hurt");
    }

    

    

    IEnumerator PlayerDeathLoad(){

        myAnimator.SetTrigger("Dying");
        _myImpulseSource.GenerateImpulse(1);

         yield return new WaitForSecondsRealtime(levelLoadDelay);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();

    Debug.Log("Level Restart");

  }

    void OnJump(InputValue value){

        if(!isAlive){return;}
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground","Platform"))){

            return;
        }

        if(value.isPressed){
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
        
        
    }

    void OnFire(InputValue value){

        if (!isAlive){ return; }
        //myAnimator.SetBool("isAttacking", true);
        myAnimator.SetTrigger("Attack");
        audioSource.Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackMask);

        foreach (Collider2D enemy in hitEnemies) {
            if (enemy.CompareTag("Enemy")) {
                // Assuming your boss script has a TakeDamage method
                enemy.GetComponent<BatEnemy>().TakeDamage();
            }
        }

    }


    void  FlipSprite(){

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
            {
                transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
            }

    }
}
