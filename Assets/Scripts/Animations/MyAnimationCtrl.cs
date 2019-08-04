using System;
using UnityEngine;

public class MyAnimationCtrl : MonoBehaviour {
    public Animator anim;
    
    private void Start() {
        anim = GetComponentInChildren<Animator>();
    }
    
    // TODO: hook up the correct animation names
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

    public bool isZero(float f) {
        return Math.Abs(f) < 0.001f;
    }

    public bool isPos(float f) {
        return f > 0 && Math.Abs(1 - f) < 0.001f;
    }

    public bool isNeg(float f) {
        return f < 0 && Math.Abs(1 - f) < 0.001f;
    }
}