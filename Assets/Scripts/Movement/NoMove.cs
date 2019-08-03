using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMove: MonoBehaviour, IMovementBehavior
{
    public MovementIntent GetMovementIntent(Vector2 direction)
    {
        return new MovementIntent(Directions.None);
    }
}
