using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public int health = 4;

    void Update() {      
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bullet")){
            this.health--;
        } 

        if (this.health <= 0) { 
            Destroy(gameObject);        
        }   
    }
}
