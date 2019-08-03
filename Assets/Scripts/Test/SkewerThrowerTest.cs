using System.Collections.Generic;
using UnityEngine;

public class SkewerThrowerTest : MonoBehaviour {
    public SkewerThrower thrower;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // these are the points on the board
            var p = new List<Vector3>();
            // this is the player position
            p.Add(new Vector3(-6, -2, 0));
            // this is the position of a mirror
            p.Add(new Vector3(2, -2, 0));
            // this is the position the second mirror
            p.Add(new Vector3(2, 2, 0));
            // this is the ending position of the 
            p.Add(new Vector3(6, 2, 0));

            // these are all the ingredients on the board
            var gos = new List<GameObject>();
            gos.AddRange(GameObject.FindGameObjectsWithTag("Ingredient"));

            // skewer length is just some visual sugar
            thrower.Shoot(p, gos.ConvertAll(go => go.transform), 4);
        }
    }
}