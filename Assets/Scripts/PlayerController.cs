using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 moveDirection;
    public float speed = 10f;
    public GameObject projectile;
    Vector2 fireDirection = new Vector2(0, 1);

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 500);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
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
