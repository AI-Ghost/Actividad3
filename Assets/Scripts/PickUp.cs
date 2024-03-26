using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] AudioClip pickUpSFX;
    [SerializeField] int pointsPickUp = 15;

    bool wasCollected = false;


    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && !wasCollected){

            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsPickUp);
            AudioSource.PlayClipAtPoint(pickUpSFX,Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
