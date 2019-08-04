using System;
using UnityEngine;

public class Entity : MonoBehaviour {
    [HideInInspector] public IMovementBehavior movementbehavior;
    [HideInInspector] public MyAnimationCtrl MyAnimator;
    [HideInInspector] public LerpAnimator lerper;

    public string Name;

    private void Start() {
        movementbehavior = GetComponentInChildren<IMovementBehavior>();
        if (movementbehavior == null) throw new Exception("Missing IMovementBehavior on Entity: " + name);
        MyAnimator = GetComponentInChildren<MyAnimationCtrl>();
        if (MyAnimator == null) throw new Exception("Missing MyAnimationCtrl on Entity: " + name);
        lerper = GetComponentInChildren<LerpAnimator>();
        if (lerper == null) throw new Exception("Missing LerpAnimator on Entity: " + name);
    }

    public void Move(Vector3 destination, float time, Action callback) {
        Vector2 dir = new Vector2(destination.x-transform.position.x, destination.y-transform.position.y);
        if (MyAnimator != null)
        {
            MyAnimator.Animate(dir);
        }
        lerper.Begin(transform.position, destination, time, callback);
    }
    
}