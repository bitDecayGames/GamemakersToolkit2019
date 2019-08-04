using System;
using UnityEngine;

public class FlashSprite : MonoBehaviour
{
    public const float FadeTime = 5f;
    public float timeToFade;

    public SpriteRenderer MySpriteRenderer;

    private void Start()
    {
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        if (MySpriteRenderer == null)
        {
            throw new Exception("Unable to find sprite renderer");
        }
    }

    private void Update()
    {
        if (timeToFade >= 0)
        {
            timeToFade -= Time.deltaTime;
            MySpriteRenderer.color = new Color(1, 1, 1, timeToFade/FadeTime); 
        }
    }

    public void Flash()
    {
        MySpriteRenderer.enabled = true;
        MySpriteRenderer.color = new Color(255, 255, 255, 255); 
        timeToFade = FadeTime;
    }
}