using System;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Tile tile;
    public Entity entity;
    public String ascii;

    public override string ToString() {
        return ascii;
    }
}
