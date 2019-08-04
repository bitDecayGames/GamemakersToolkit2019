using System;
using UnityEngine;

public class PlayerAnimationCtrl : MyAnimationCtrl {
    private Animator anim;

    private void Start() {
        anim = GetComponentInChildren<Animator>();
    }
    
    // TODO: hook up the correct animation names
    public new void Animate(Vector2 dir) {
        Debug.Log("Call reached the custom animator class");
        
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