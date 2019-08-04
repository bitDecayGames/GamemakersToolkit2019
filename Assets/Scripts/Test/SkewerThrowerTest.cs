using System.Collections.Generic;
using UnityEngine;

public class SkewerThrowerTest : MonoBehaviour {
    public SkewerThrower thrower;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            run(buildPath_1());
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            run(buildPath_2());
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            run(buildPath_3());
        }
    }

    private List<Vector3> buildPath_1() {
        var p = new List<Vector3>();
        p.Add(new Vector3(-6, 2, 0));
        p.Add(new Vector3(6, 2, 0));
        return p;
    }

    private List<Vector3> buildPath_2() {
        var p = new List<Vector3>();
        p.Add(new Vector3(-6, -2, 0));
        p.Add(new Vector3(2, -2, 0));
        p.Add(new Vector3(2, 2, 0));
        p.Add(new Vector3(6, 2, 0));
        return p;
    }

    private List<Vector3> buildPath_3() {
        var p = new List<Vector3>();
        p.Add(new Vector3(-6, 4, 0));
        p.Add(new Vector3(-6, -4, 0));
        p.Add(new Vector3(-4, -4, 0));
        p.Add(new Vector3(-4, 4, 0));
        p.Add(new Vector3(0, 4, 0));
        p.Add(new Vector3(0, -3, 0));
        p.Add(new Vector3(2, -3, 0));
        p.Add(new Vector3(2, 2, 0));
        p.Add(new Vector3(3, 2, 0));
        p.Add(new Vector3(3, -1, 0));
        p.Add(new Vector3(4, -1, 0));
        p.Add(new Vector3(4, 0, 0));
        p.Add(new Vector3(5, 0, 0));
        return p;
    }

    private void run(List<Vector3> path) {
        // these are all the ingredients on the board
        var gos = new List<GameObject>();
        gos.AddRange(GameObject.FindGameObjectsWithTag("Ingredient"));

        // skewer length is just some visual sugar
        thrower.Shoot(path, gos.ConvertAll(go => go.transform), 4);
    }
}