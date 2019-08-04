using UnityEngine;

public class Tile : MonoBehaviour {
    public string Name;
    public bool Standable;
    public bool Solid;

    public void SetSprite(Sprite sprite) {
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
    }
}