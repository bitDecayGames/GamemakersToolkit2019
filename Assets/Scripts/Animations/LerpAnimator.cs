using System;
using UnityEngine;

public class LerpAnimator : MonoBehaviour {
    private bool isLerping = false;
    private Vector3 start;
    private Vector3 end;
    private float time;
    private float totalTime;
    private Action callback;

    public void Begin(Vector3 startingWorldPosition, Vector3 endWorldPosition, float time, Action callback) {
        isLerping = true;
        start = startingWorldPosition - endWorldPosition;
        end = new Vector3();
        this.time = time;
        totalTime = time;
        this.callback = callback;
    }

    private void Update() {
        if (isLerping) {
            time -= Time.deltaTime;
            if (time <= 0) {
                isLerping = false;
                transform.localPosition = new Vector3();
                if (callback != null) callback();
            } else {
                var lerp = Vector3.Lerp(end, start, time / totalTime);
                transform.localPosition = lerp;
            }
        }
    }
}