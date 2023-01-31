using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private int health = 4;
    private Animator animator;

    void Start() {    
        animator = GetComponent<Animator>();
    }

    void Update() {
        // if (health == 4) {
        //     animator.SetFloat("name", 0);
        // } else if (health == 3) {
        //     animator.SetFloat("name", 1);
        // } else if (health == 2) {
        //     animator.SetFloat("name", 2);
        // } else {
        //     animator.SetFloat("name", 3);
        // }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bullet")){
            this.health--;
        } 

        // if spider collides with mushroom, there is a 30% chance it will destroy
        if (collision.gameObject.CompareTag("Spider")) {
            float roll = Mathf.Round(Random.Range(0, 10));

            if (roll <= 7 && roll >= 10) {
                Destroy(gameObject);
            }
        }

        if (this.health <= 0) { 
            Destroy(gameObject);        
        }   
    }
}
