using System;
using UnityEngine;

public class DestinationSlide : MonoBehaviour
{
    public Transform Destination;

    public bool done;

    private void FixedUpdate()
    {
        if (!done)
        {
            if (transform.position != Destination.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, Destination.position, .05f);
            }
            else
            {
                done = true;
                FMODSoundEffectsPlayer.Instance.PlaySoundEffect(SFX.Squish);
            }
        }
    }
}