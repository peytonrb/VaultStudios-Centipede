using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private int health = 3;
    private SpriteRenderer spriteRenderer;
    public Sprite[] healthState;
    private PlayerController player;

    void Start() {    
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<PlayerController>();
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Bullet")) {
            this.health--;

            if (health == 2) {
                spriteRenderer.sprite = (Sprite) Resources.Load<Sprite>("Sprites/shell_2") as Sprite;
            } else if (health == 1) {
                spriteRenderer.sprite = (Sprite) Resources.Load<Sprite>("Sprites/shell_3") as Sprite;
            } else {
                if (player != null) {
                    player.changeScore(1);
                }
            }
        } 

        // if spider collides with mushroom, there is a 30% chance it will destroy
        if (collision.gameObject.CompareTag("Spider")) {
            this.health--;
        }

        if (this.health <= 0) { 
            Destroy(gameObject);        
        }   
    }
}
