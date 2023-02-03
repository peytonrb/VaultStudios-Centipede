using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeController : MonoBehaviour
{
    public CentipedeSegment segmentPrefab;
    private List<CentipedeSegment> segments = new List<CentipedeSegment>();
    public Sprite head;
    public Sprite body;
    private int size = 11;
    public float speed = 12f;
    public LayerMask collisionMask;

    void Start() {
        respawn();
    }

    void respawn() {
        foreach (CentipedeSegment segment in segments) {
            Destroy(segment.gameObject);
        }

        segments.Clear();

        for (int i = 0; i < size; i++) {
            // offset segments
            Vector2 position = GridPosition(transform.position) + (Vector2.left * i);
            CentipedeSegment segment = Instantiate(segmentPrefab, position, Quaternion.identity);

            if (i == 0) {
                segment.spriteRenderer.sprite = head;
            } else {
                segment.spriteRenderer.sprite = body;
            }

            segment.centipede = this;

            segments.Add(segment);
        }

        // assigning CentipedeSegment properties
        for (int i = 0; i < segments.Count; i++) {
            CentipedeSegment segment = segments[i];
            segment.ahead = getSegment(i - 1);
            segment.behind = getSegment(i + 1);
        }
    }

    private CentipedeSegment getSegment(int index) {
        if (index >= 0 && index < segments.Count) {
            return segments[index];
        } else {
            return null;
        }
    }

    // rounds the position to the nearest grid line
    public Vector2 GridPosition(Vector2 position) {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        return position;
    }
}
