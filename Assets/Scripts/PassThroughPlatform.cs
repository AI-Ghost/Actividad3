using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{

    private Collider2D _collider;
    private bool _playerOnPlatform;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    void Update() {

        if(_playerOnPlatform && Input.GetAxisRaw("Vertical") < 0 )
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());

        }
        
    }

    private IEnumerator EnableCollider()
    {

        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {

        var player = other.gameObject.GetComponent<PlayerMovement>();
        if(player != null)
        {
            _playerOnPlatform = value;
        }
    }

     private void OnCollisionEnter2D(Collision2D other) {

        SetPlayerOnPlatform(other,true);
        
    }

    private void OnCollisionExit2D(Collision2D other) {

        SetPlayerOnPlatform(other,true);
        
    }
   
}
