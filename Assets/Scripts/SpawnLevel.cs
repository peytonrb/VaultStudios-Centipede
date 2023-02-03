using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    private static BoxCollider2D field;
    public GameObject mushroom;
    public int amount = 60;
    public GameObject spider;

    void Awake() {
        field = GetComponent<BoxCollider2D>();
    }

    void Start() {
        Bounds bounds = field.bounds;
        Vector2 position = Vector2.zero;

        for (int i = 0; i < amount; i++) {
            position.x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            position.y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));

            Instantiate(mushroom, position, Quaternion.identity, transform);
        }

        Instantiate(spider, position, Quaternion.identity, transform);
    }

    public void ClearField()
    {
        GameObject[] mushrooms = GameObject.FindGameObjectsWithTag("Mushroom");

        for (int i = 0; i < mushrooms.Length; i++) {
            Destroy(mushrooms[i].gameObject);
        }
    }
}
