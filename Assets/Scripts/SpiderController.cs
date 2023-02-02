using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    private float minX = -10.25f;
    private float maxX = 7.5f;
    private float minY = -14f;
    private float maxY = 10.2f; // magic numbers bc bounds are not in scope of this method
    public GameObject spider;
    private BoxCollider2D field;
    private Vector2 targetPosition;
    private float speed = 7f;
    private PlayerController player;

    void Start() {
        targetPosition = getPosition();
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<PlayerController>();
    }

    void Update() {
        if ((Vector2) transform.position != targetPosition) {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        } else {
            targetPosition = getPosition();
        }
    }

    private Vector2 getPosition() {

        float x = Mathf.Round(Random.Range(minX, maxX));
        float y = Mathf.Round(Random.Range(minY, maxY));
        Vector2 newPosition = new Vector2(x, y);

        return newPosition;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        float currentLocation = gameObject.transform.position.y;

        if (collision.collider.tag == "Barrier") {
            transform.position = Vector2.MoveTowards(-(transform.position), targetPosition, speed * Time.deltaTime);
        } else if (collision.gameObject.CompareTag("Bullet")) {
      
            if (player != null) {
                if (currentLocation > 6.5f) {
                    player.changeScore(900);
                } else if (currentLocation <= 4.5f && currentLocation > -3.5f) {
                    player.changeScore(600);
                } else {
                    player.changeScore(300);
                }

            }

            Destroy(gameObject);
        }
    }
}
