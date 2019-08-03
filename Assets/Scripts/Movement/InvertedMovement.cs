using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedMovement: MonoBehaviour, IMovementBehavior
{
    public MovementIntent GetMovementIntent(Vector2 direction)
    {
        return new MovementIntent(direction*-1);
    }
}