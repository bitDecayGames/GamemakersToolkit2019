using System;
using SuperTiled2Unity;
using UnityEngine;

public class DestinationSlide : MonoBehaviour
{
    public Vector3 Destination = Vector3.zero;
    public String ExtraSound;
    public GameObject FlashScreen;

    public bool done;

    private void FixedUpdate()
    {
        if (!done)
        {
            if (transform.position != Destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, Destination, .065f);
            }
            else
            {
                done = true;
                FMODSoundEffectsPlayer.Instance.PlaySoundEffect(SFX.Squish);
                if (!ExtraSound.IsEmpty())
                {
                    FMODSoundEffectsPlayer.Instance.PlaySoundEffect(ExtraSound);
                }

                if (FlashScreen != null)
                {
                    FlashScreen.GetComponent<FlashSprite>().Flash();
                }
            }
        }
    }
}