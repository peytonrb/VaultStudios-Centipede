using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Vector2 moveDirection;
    public float speed = 10f;

    void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {

        
    }

    void FixedUpdate() {
        // accessing Input Manager to allow player controls
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x = position.x + speed * moveDirection.x * Time.deltaTime; // frame independent
        position.y = position.y + speed * moveDirection.y * Time.deltaTime;
        transform.position = position;

        rigidbody.MovePosition(position);
    }
}
