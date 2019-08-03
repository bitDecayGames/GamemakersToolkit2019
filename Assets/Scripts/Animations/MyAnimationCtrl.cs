using System;
using UnityEngine;

public class MyAnimationCtrl : MonoBehaviour {
    private Animator anim;

    private void Start() {
        anim = GetComponentInChildren<Animator>();
    }

    public void Animate(Vector2 dir) {
        if (isZero(dir.x) && isPos(dir.y)) {
            // north
            anim.Play("North_Move");
        } else if (isZero(dir.x) && isNeg(dir.y)) {
            // south
            anim.Play("South_Move");
        } else if (isPos(dir.x) && isZero(dir.y)) {
            // east
            anim.Play("East_Move");
        } else if (isNeg(dir.x) && isZero(dir.y)) {
            // west
            anim.Play("West_Move");
        } else if (isZero(dir.x) && isZero(dir.y)) {
            // none
            anim.Play("None");
        }
    }

    private bool isZero(float f) {
        return Math.Abs(f) < 0.001f;
    }

    private bool isPos(float f) {
        return f > 0 && Math.Abs(1 - f) < 0.001f;
    }

    private bool isNeg(float f) {
        return f < 0 && Math.Abs(1 - f) < 0.001f;
    }
}