﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class SkewerThrower : MonoBehaviour {
    public Material Material;
    
    private LineRenderer _lineRenderer;

    private List<Vector3> points;
    private List<Vector3> line;
    private float skewerLength;
    private bool isMoving;
    private float moveSpeed = 1f / 30f;

    private float tipPercent;
    private float tailPercent;

    void Start() {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.01f;
        _lineRenderer.endWidth = 0.1f;
        _lineRenderer.startColor = new Color(0, 1, 1);
        _lineRenderer.endColor = new Color(1, 0, 1);
        _lineRenderer.material = Material;
    }

    void Update() {
        // TODO: take this out and put it somewhere else
        if (Input.GetKeyDown(KeyCode.Space)) {
            var p = new List<Vector3>();
            p.Add(new Vector3(-6, -2, 0));
            p.Add(new Vector3(2, -2, 0));
            p.Add(new Vector3(2, 2, 0));
            p.Add(new Vector3(6, 2, 0));
            Shoot(p, 4);
        }

        if (isMoving) {
            tipPercent += moveSpeed;
            if (tipPercent > 1) isMoving = false;
            line.Clear();
            line.Add(pointOnLine(points, tipPercent));
            line.AddRange(pointsBetweenPercent(points, tailPercent, tipPercent));
            line.Add(pointOnLine(points, tailPercent));
            if (lineLength(line) > skewerLength) tailPercent += moveSpeed;

            _lineRenderer.positionCount = line.Count;
            _lineRenderer.SetPositions(line.ToArray());
        }
    }

    public void Shoot(List<Vector3> points, float skewerLength) {
        if (points == null || points.Count < 2) throw new Exception("There must be at least two points in the list, a start and end point");
        if (skewerLength <= 0) throw new Exception("The skewer length must be greater than 0");
        isMoving = true;
        tipPercent = 0;
        tailPercent = 0;
        this.points = points;
        this.skewerLength = skewerLength;
        line = new List<Vector3>();
    }

    private static Vector3 pointOnLine(List<Vector3> line, float percent) {
        if (line == null || line.Count < 2) throw new Exception("The line cannot be less than two points, its called 2D for a reason");
        if (percent <= 0) return line[0];
        if (percent >= 1) return line[line.Count - 1];

        float total = lineLength(line);
        float target = total * percent;

        float cur = 0f;
        for (int i = 0; i + 1 < line.Count; i += 1) {
            var dist = Vector3.Distance(line[i], line[i + 1]);
            if (cur + dist > target) {
                var percentDiff = (target - cur) / dist;
                return pointOnLine(line[i], line[i + 1], percentDiff);
            } else cur += dist;
        }

        return line[line.Count - 1];
    }

    private static Vector3 pointOnLine(Vector3 start, Vector3 end, float percent) {
        if (percent < 0 || percent > 1) throw new Exception("Percent means a number between 0 and 1, 0 being the first point, and 1 being the last point");
        return (end - start) * percent + start;
    }

    private static List<Vector3> pointsBetweenPercent(List<Vector3> line, float start, float end) {
        if (line == null || line.Count < 2) throw new Exception("The line cannot be less than two points, its called 2D for a reason");

        List<Vector3> points = new List<Vector3>();

        float total = lineLength(line);
        float startTarget = total * start;
        float endTarget = total * end;

        float cur = 0f;
        for (int i = 0; i + 1 < line.Count; i += 1) {
            cur += Vector3.Distance(line[i], line[i + 1]);
            if (cur > endTarget) break;
            if (cur > startTarget) points.Add(line[i + 1]);
        }

        points.Reverse();
        return points;
    }

    private static float lineLength(List<Vector3> line) {
        if (line == null || line.Count < 2) throw new Exception("The line cannot be less than two points, its called 2D for a reason");
        float total = 0f;
        for (int i = 0; i + 1 < line.Count; i += 1) {
            total += Vector3.Distance(line[i], line[i + 1]);
        }

        return total;
    }
}