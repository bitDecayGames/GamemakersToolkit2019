using System;
using UnityEngine;

public class DestinationSlide : MonoBehaviour
{
    public Vector3 Destination = Vector3.zero;

    public bool done;

    private void FixedUpdate()
    {
        if (!done)
        {
            if (transform.position != Destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, Destination, .07f);
            }
            else
            {
                done = true;
                FMODSoundEffectsPlayer.Instance.PlaySoundEffect(SFX.Squish);
            }
        }
    }
}