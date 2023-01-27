using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public int health = 3;

    void Update() {
        if (this.health <= 0) { 
            Destroy(gameObject);           
        }        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Bullet")){
            this.health--;
        }        
    }
}
