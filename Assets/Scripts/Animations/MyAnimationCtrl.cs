using System;
using UnityEngine;

public class MyAnimationCtrl : MonoBehaviour {
    [HideInInspector] public Animator anim;

    void Start() {
        init();
    }

    protected void init() {
        anim = GetComponentInChildren<Animator>();
        if (anim == null) throw new Exception("Missing Animator on: " + name);
    }
    
    // TODO: hook up the correct animation names
    public virtual void Animate(Vector2 dir) {
        dir.Normalize();
        if (isNorth(dir)) {
            anim.Play("North_Move");
        } else if (isSouth(dir)) {
            anim.Play("South_Move");
        } else if (isEast(dir)) {
            anim.Play("East_Move");
        } else if (isWest(dir)) {
            anim.Play("West_Move");
        } else if (isNone(dir)) {
            anim.Play("None");
        } else {
//            Debug.Log("This shouldn't happen: " + dir);
        }
    }

    protected bool isNorth(Vector2 dir) {
        return isZero(dir.x) && isPos(dir.y);
    }

    protected bool isSouth(Vector2 dir) {
        return isZero(dir.x) && isNeg(dir.y);
    }

    protected bool isEast(Vector2 dir) {
        return isPos(dir.x) && isZero(dir.y);
    }

    protected bool isWest(Vector2 dir) {
        return isNeg(dir.x) && isZero(dir.y);
    }

    protected bool isNone(Vector2 dir) {
        return isZero(dir.x) && isZero(dir.y);
    }

    public bool isZero(float f) {
        return Math.Abs(f) < 0.001f;
    }

    public bool isPos(float f) {
        return f > 0 && Math.Abs(1 - f) < 0.001f;
    }

    public bool isNeg(float f) {
        return f < 0 && 1 - Math.Abs(f) < 0.001f;
    }
}