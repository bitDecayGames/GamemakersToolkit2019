using System;
using UnityEngine;

public class Entity : MonoBehaviour {
    [HideInInspector] public IMovementBehavior movementbehavior;
    [HideInInspector] public MyAnimationCtrl MyAnimator;
    [HideInInspector] public PlayerAnimationCtrl PlayerAnimator;
    [HideInInspector] public LerpAnimator lerper;

    public string Name;

    private void Start() {
        movementbehavior = GetComponent<IMovementBehavior>();
        MyAnimator = GetComponent<MyAnimationCtrl>();
        PlayerAnimator = GetComponent<PlayerAnimationCtrl>();
        lerper = GetComponent<LerpAnimator>();
    }

    public void Move(Vector3 destination, float time, Action callback) {
        Vector2 dir = new Vector2(destination.x-transform.position.x, destination.y-transform.position.y);
        if (MyAnimator != null)
        {
            MyAnimator.Animate(dir);
        }
        if (PlayerAnimator != null)
        {
            PlayerAnimator.Animate(dir);
        }
        lerper.Begin(transform.position, destination, time, callback);
    }
    
}