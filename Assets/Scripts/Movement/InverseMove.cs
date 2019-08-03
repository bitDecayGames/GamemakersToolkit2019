using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseMove: MonoBehaviour, IMovementBehavior
{
    public MovementIntent GetMovementIntent(Vector2 direction)
    {
        return new MovementIntent(direction*-1);
    }
}