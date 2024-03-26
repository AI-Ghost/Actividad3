using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingPlatform : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.name.Equals("Zack")){
            PlatformManager.Instance.StartCoroutine("SpawnPlatform", new Vector2(transform.position.x, transform.position.y));
            Invoke("DropPlatform",0.5f);
            Destroy(gameObject, 1f);
        }
        
    }

    void DropPlatform(){
        rb.isKinematic = false;
    }
}
