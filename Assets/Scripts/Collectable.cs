using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

  [SerializeField] int pointsForCollectables = 5;

  bool wasCollected = false;
  private void OnTriggerEnter2D(Collider2D other) {

    if(other.tag == "Player" && !wasCollected){

        wasCollected = true;

        FindObjectOfType<GameSession>().AddToScore(pointsForCollectables);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
    
  }
}
