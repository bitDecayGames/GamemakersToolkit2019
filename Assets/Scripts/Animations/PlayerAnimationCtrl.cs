using UnityEngine;

public class PlayerAnimationCtrl : MyAnimationCtrl {
    private int walkUpIndex;
    private int walkDownIndex;
    private int walkRightIndex;
    private int walkLeftIndex;

    void Start() {
        init();
    }

    public override void Animate(Vector2 dir) {
        dir.Normalize();
        if (isNorth(dir)) {
            anim.Play("WalkUp" + (walkUpIndex++ % 2 + 1));
        } else if (isSouth(dir)) {
            anim.Play("WalkDown" + (walkDownIndex++ % 2 + 1));
        } else if (isEast(dir)) {
            anim.Play("WalkRight" + (walkRightIndex++ % 2 + 1));
        } else if (isWest(dir)) {
            anim.Play("WalkLeft" + (walkLeftIndex++ % 2 + 1));
        } else if (isNone(dir)) {
            anim.Play("None");
        } else {
            Debug.LogWarning("Hit a mystery zone: " + name);
        }
    }

    public void AnimateShoot(bool left) {
        if (left) {
            anim.Play("ThrowLeft_Down");
        } else {
            anim.Play("ThrowRight_Down");
        }
    }
}