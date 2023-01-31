using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
