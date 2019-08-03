using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMove: MonoBehaviour, IMovementBehavior
{
    public MovementIntent GetMovementIntent(Vector2 direction)
    {
        return new MovementIntent(direction);
    }
}