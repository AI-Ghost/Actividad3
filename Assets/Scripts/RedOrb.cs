using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrb : MonoBehaviour
{
     [SerializeField] int pointsForCollectables = 35;

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
