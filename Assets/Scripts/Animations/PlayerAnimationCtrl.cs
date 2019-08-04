using System;
using UnityEngine;

public class PlayerAnimationCtrl : MonoBehaviour {
    public Animator anim;

    private int walkUpIndex;
    private int walkDownIndex;
    private int walkRightIndex;
    private int walkLeftIndex;
    
    private void Start() {
        anim = GetComponentInChildren<Animator>();
    }
    
    // TODO: hook up the correct animation names
    public void Animate(Vector2 dir) {
        if (isZero(dir.x) && isPos(dir.y)) {
            // north
            Debug.Log($"Playing {"WalkUp" + (walkUpIndex % 2 + 1)}");
            anim.Play("WalkUp" + (walkUpIndex++ % 2 + 1));
        } else if (isZero(dir.x) && isNeg(dir.y)) {
            // south
            Debug.Log($"Playing {"WalkDown" + (walkDownIndex % 2 + 1)}");
            anim.Play("WalkDown" + (walkDownIndex++ % 2 + 1));
        } else if (isPos(dir.x) && isZero(dir.y)) {
            // east
            Debug.Log($"Playing {"WalkRight" + (walkRightIndex % 2 + 1)}");
            anim.Play("WalkRight" + (walkRightIndex++ % 2 + 1));
        } else if (isNeg(dir.x) && isZero(dir.y)) {
            // west
            Debug.Log($"Playing {"WalkLeft" + (walkLeftIndex % 2 + 1)}");
            anim.Play("WalkLeft" + (walkLeftIndex++ % 2 + 1));
        } else if (isZero(dir.x) && isZero(dir.y)) {
            // none
            Debug.Log("Playing nothing");
            anim.Play("None");
        }
        else
        {
            Debug.Log("Hit a mystery zone");
        }
    }

    public bool isZero(float f) {
        return Math.Abs(f) < 0.001f;
    }

    public bool isPos(float f) {
        return f > 0;
    }

    public bool isNeg(float f) {
        return f < 0;
    }
}