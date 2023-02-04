using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeController : MonoBehaviour
{
    public CentipedeSegment segmentPrefab;
    public Mushroom mushroomPrefab;
    private List<CentipedeSegment> segments = new List<CentipedeSegment>();
    public Sprite head;
    public Sprite body;
    private int size = 11;
    public float speed = 12f;
    public LayerMask collisionMask;
    public BoxCollider2D homeArea; // prevents centipede from leaving the space
    private PlayerController player;
    private SpiderController spider;
    private AudioSource source;
    public AudioClip hurt;

    void Start() {
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<PlayerController>();
        source = GetComponent<AudioSource>();

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

    public void Remove(CentipedeSegment segment) {
        player.changeScore(100);
        source.PlayOneShot(hurt);
        Vector3 position = GridPosition(segment.transform.position);
        Instantiate(mushroomPrefab, position, Quaternion.identity);

        // if segment is destroyed in the middle of the centipede
        if (segment.ahead != null) {
            segment.ahead.behind = null;
        }

        if (segment.behind != null) {
            segment.behind.ahead = null;
            segment.behind.spriteRenderer.sprite = head;
            segment.behind.updateHeadSegment();
        }

        segments.Remove(segment);
        Destroy(segment.gameObject);

        // instantiates a new level
        if (segments.Count == 0) {
            speed *= 1.1f;
            respawn();
            // spider.speed *= 1.1f;
        }
    }
}
