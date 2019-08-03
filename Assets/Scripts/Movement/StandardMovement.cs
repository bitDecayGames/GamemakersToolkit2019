using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMovement : MonoBehaviour, IMovementBehavior
{
    public MovementIntent GetMovementIntent(Vector2 direction)
    {
        return new MovementIntent(direction);
    }
}