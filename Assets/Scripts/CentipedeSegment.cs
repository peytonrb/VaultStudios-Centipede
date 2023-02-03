using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSegment : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public CentipedeSegment ahead { get; set; } // this ensures it won't show in editor
    public CentipedeSegment behind { get; set; }
    public bool isHead => ahead == null;
    public CentipedeController centipede { get; set; }
    private Vector2 targetPosition;
    private Vector2 direction = Vector2.right + Vector2.down;
    
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
    }

    void Update() {
        if (isHead && Vector2.Distance(transform.position, targetPosition) < 0.1f) {
            updateHeadSegment();
        }

        Vector2 currentPosition = transform.position;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, centipede.speed * Time.deltaTime);
        Vector2 movementDirection = (targetPosition - currentPosition).normalized;

        // make sure segment is always rotated in the direction you're moving
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void updateHeadSegment() {
        Vector2 gridPosition = centipede.GridPosition(transform.position);
        targetPosition = gridPosition;
        targetPosition.x += direction.x;
        
        if (Physics2D.OverlapBox(targetPosition, Vector2.zero, 0f, centipede.collisionMask)) {
            direction.x = -direction.x;
            targetPosition.x = gridPosition.x;
            targetPosition.y = gridPosition.y + direction.y;

            Bounds homeBounds = centipede.homeArea.bounds;

            // guarantees centipede stays within home once it enters
            if ((direction.y == 1f && targetPosition.y > homeBounds.max.y) || 
                (direction.y == -1f && targetPosition.y < homeBounds.min.y)) {
                direction.y = -direction.y;
                targetPosition.y = gridPosition.y + direction.y;
            }
        }

        if (behind != null) {
            behind.updateBodySegment(); // check for tail
        }
    }

    public void updateBodySegment() {
        targetPosition = centipede.GridPosition(ahead.transform.position);
        direction = ahead.direction;

        if (behind != null) {
            behind.updateBodySegment(); // check for tail
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet")) {
            centipede.Remove(this);
        }
    }
}
