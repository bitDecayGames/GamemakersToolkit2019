using System;
using UnityEngine;

public class Entity : MonoBehaviour {
    [HideInInspector] public IMovementBehavior movementbehavior;
    [HideInInspector] public MyAnimationCtrl animator;
    [HideInInspector] public LerpAnimator lerper;

    private void Start() {
        movementbehavior = GetComponent<IMovementBehavior>();
        animator = GetComponent<MyAnimationCtrl>();
        lerper = GetComponent<LerpAnimator>();
    }

    public void Move(Vector3 destination, float time, Action callback) {
        Vector2 dir = new Vector2(destination.x, destination.y);
        dir.Normalize();
        animator.Animate(dir);
        lerper.Begin(transform.position, destination, time, callback);
    }
    
}