using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class GameSession : MonoBehaviour
{
    //[SerializeField] float levelLoadDelay = 3f;

    [SerializeField] int PlayerHealth = 100;
    [SerializeField] int score = 0;

     [SerializeField] int damage = 5;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI scoreText;

    public int CollectableCounter = 0;

    //public GameObject otherObject;
   // Animator playerAnimator;
    

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {

            DontDestroyOnLoad(gameObject);
        }

        //playerAnimator = otherObject.GetComponent<Animator>();
    }
    void Start()
    {
        healthText.text = PlayerHealth.ToString();
        scoreText.text = score.ToString();

    }

    public void ProcessPlayerDeath(){

        if(PlayerHealth > 1){
            TakeLife();
        }
        else{

            ResetGameSession();
        }
    }

    void TakeLife(){


        PlayerHealth-=damage;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        healthText.text = PlayerHealth.ToString();

        if(PlayerHealth < 1){

           ResetGameSession();
        }
        
        
    }

   /* public void ProcessPlayerEnemyHit(int hit){

        if(PlayerHealth > 1){
             TakeEnemyHit(hit);

        }
        else{

            ResetGameSession();
        }
    }*/

    public void TakeEnemyHit(int hitPoints){


        PlayerHealth-=hitPoints;
        FindObjectOfType<PlayerMovement>().EnemyDeath();
        healthText.text = PlayerHealth.ToString();
        //playerAnimator.ResetTrigger("Hurt");


        if(PlayerHealth < 1){

           ResetGameSession();
        }
    }

    public void AddToScore(int pointsToAdd){
        

        score += pointsToAdd;
        CollectableCounter +=1;
        scoreText.text = score.ToString();

    }

    /*void OnTriggerEnter2D(Collider2D other)
    {

        StartCoroutine(ResetGameSession());

    }*/


   /* IEnumerator ResetGameSession(){

    yield return new WaitForSecondsRealtime(levelLoadDelay);

    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    SceneManager.LoadScene(currentSceneIndex);

    Debug.Log("Level Restart");
    Destroy(gameObject);

  }*/

    public void ResetGameSession()
    {


        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(currentSceneIndex);
        Debug.Log("Level Restart");
        Destroy(gameObject);

    }


}


