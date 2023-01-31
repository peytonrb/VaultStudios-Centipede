using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    private float minX = -12.7f;
    private float maxX = 7.5f;
    private float minY = -13.75f;
    private float maxY = 10.2f; // magic numbers bc bounds is not in scope of this method
    public GameObject spider;
    private BoxCollider2D field;
    private Vector2 targetPosition;
    private float speed = 7f;

    void Start() {
        targetPosition = getPosition();
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

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Barrier") {
            transform.position = Vector2.MoveTowards(-(transform.position), targetPosition, speed * Time.deltaTime);
        }
    }
}
