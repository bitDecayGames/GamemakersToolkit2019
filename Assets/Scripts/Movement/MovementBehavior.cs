using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementBehavior
{
    MovementIntent GetMovementIntent(Vector2 direction);
}
