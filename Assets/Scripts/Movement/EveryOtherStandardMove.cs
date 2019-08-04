using UnityEngine;

public class EveryOtherStandardMove : MonoBehaviour, IMovementBehavior {
    bool doMove = false;

    public Sprite awakeSprite;
    public Sprite sleepingSprite;
    private SpriteRenderer _spriteRenderer;

    private void Start() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public MovementIntent GetMovementIntent(Vector2 direction) {
        if (doMove) {
            doMove = false;
            SetSprite();
            return new MovementIntent(direction);
        } else {
            doMove = true;
            SetSprite();
            return new MovementIntent(Directions.None);
        }
    }

    private void SetSprite() {
        if (doMove) {
            _spriteRenderer.sprite = awakeSprite;
        } else {
            _spriteRenderer.sprite = sleepingSprite;
        }
    }
}