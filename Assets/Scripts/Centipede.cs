using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    private float speed = 5f;
    private int segmentLength = 10;
    private int pathIndex = 0;
    private Vector2 centipedeDirection = Vector2.right;
    private List<Transform> segments = new List<Transform>();
    public GameObject head;
    public GameObject body;
    public GameObject[] path;

    void Start() {
        // transform.position = path[pathIndex].transform.position;

        for (int i = 0; i <= segmentLength; i++) {

            // stores current position and moves to make room for new segment
            Vector2 currentPosition = transform.position;
            transform.Translate(centipedeDirection * speed * Time.deltaTime);

            GameObject segment = Instantiate(body, currentPosition, Quaternion.identity, transform);
            segments.Insert(segments.Count, segment.transform);
        }
    }

    void Update() {
        split();
        move();

        transform.position = Vector2.MoveTowards(transform.position, path[pathIndex].transform.position, speed * Time.deltaTime);

        if (transform.position == path[pathIndex].transform.position) {
            pathIndex += 1;
        }

        if (pathIndex == path.Length - 1) {
            pathIndex = 0;
        }
    }

    private void move() {
        Transform currentLocation = transform;
        // transform.right = path[pathIndex].transform.position - transform.position;

        for (int i = 0; i < segments.Count; i++) {
            if (i == 0) {
                segments[i].transform.position = Vector2.MoveTowards(segments[i].transform.position,
                    currentLocation.position, speed * Time.deltaTime);
                segments[i].transform.right = currentLocation.position - segments[i].transform.position;
            } else {
                segments[i].transform.position = Vector2.MoveTowards(segments[i].transform.position,
                    segments[i - 1].transform.position, speed * Time.deltaTime);
                segments[i].transform.right = segments[i].transform.position - segments[i - 1].transform.position;
            }
        }
    }

    private void split() {
        int count = 0;

        for (int i = 0; i < segments.Count; i++) {
            if (!segments[i].gameObject.activeSelf) {
                Destroy(segments[i].gameObject);
                segments.RemoveAt(i); 
            }

            // remove disconnected centipede parts to respawn as new centipede
            for (int j = i; j < segments.Count; j++) {
                Destroy(segments[j].gameObject);
                segments.RemoveAt(j);
                count++;
            }

            GameObject newCentipede = Instantiate(gameObject, segments[i - 1].transform.position, Quaternion.identity) as GameObject;
            newCentipede.GetComponent<Centipede>().segmentLength = count;
            newCentipede.GetComponent<Centipede>().pathIndex = pathIndex - 1;
        }
    }
}
