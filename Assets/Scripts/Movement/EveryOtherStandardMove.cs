using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryOtherStandardMove: MonoBehaviour, IMovementBehavior
{
    bool doMove = false;

    public MovementIntent GetMovementIntent(Vector2 direction)
    {
        if(doMove)
        {
            doMove = false;
            return new MovementIntent(direction);
        } else {
            doMove = true;
            return new MovementIntent(Directions.None);
        }
    }
}