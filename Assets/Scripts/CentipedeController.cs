using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeController : MonoBehaviour
{
    public CentipedeSegment segmentPrefab;
    private List<CentipedeSegment> segments = new List<CentipedeSegment>();
    public Sprite head;
    public Sprite body;
    private int size = 12;

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
            segments.Add(segment);
        }
    }

    // rounds the position to the nearest grid line
    private Vector2 GridPosition(Vector2 position) {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        return position;
    }
}
