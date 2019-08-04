using System;
using UnityEngine;

public class PlayerAnimationCtrl : MyAnimationCtrl
{
    // TODO: hook up the correct animation names
    public new void Animate(Vector2 dir)
    {
        if (isZero(dir.x) && isPos(dir.y))
        {
            // north
            anim.Play("North_Move");
        }
        else if (isZero(dir.x) && isNeg(dir.y))
        {
            // south
            anim.Play("South_Move");
        }
        else if (isPos(dir.x) && isZero(dir.y))
        {
            // east
            anim.Play("East_Move");
        }
        else if (isNeg(dir.x) && isZero(dir.y))
        {
            // west
            anim.Play("West_Move");
        }
        else if (isZero(dir.x) && isZero(dir.y))
        {
            // none
            anim.Play("None");
        }
    }
}