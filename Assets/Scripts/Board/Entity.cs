using System;
using UnityEngine;

public class Entity : MonoBehaviour {
    [HideInInspector] public IMovementBehavior movementbehavior;
    [HideInInspector] public MyAnimationCtrl animator;
    [HideInInspector] public PlayerAnimationCtrl playerAnimator;
    [HideInInspector] public LerpAnimator lerper;

    public string Name;

    private void Start() {
        movementbehavior = GetComponent<IMovementBehavior>();
        animator = GetComponent<MyAnimationCtrl>();
        playerAnimator = GetComponent<PlayerAnimationCtrl>();
        lerper = GetComponent<LerpAnimator>();
    }

    public void Move(Vector3 destination, float time, Action callback) {
        Vector2 dir = new Vector2(destination.x, destination.y);
        dir.Normalize();
        if (animator != null)
        {
            animator.Animate(dir);
        }
        animator.Animate(dir);
        if (playerAnimator != null)
        {
            playerAnimator.Animate(dir);
        }
        lerper.Begin(transform.position, destination, time, callback);
    }
    
}