using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TintCanvasController : MonoBehaviour {
    private const string SuccessDisplay = "Ka-nice!";
    private const string SuccessContinue = "Press Spacebar to proceed";
    private const string FailureDisplay = "Ka-failed...";
    private const string FailureContinue = "Press spacebar to retry";

    public Image Tint;
    public TextMeshProUGUI DisplayText;
    public TextMeshProUGUI ContinueText;

    private Action onSpace = null;

    private Action onTimerEnd;
    private float timer;

    private void Update() {
        if (onSpace != null) {
            onSpace();
            onSpace = null;
        }

        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer < 0 && onTimerEnd != null) {
                onTimerEnd();
                onTimerEnd = null;
            }
        }
    }

    public void Reset() {
        Tint.enabled = false;
        DisplayText.enabled = false;
        ContinueText.enabled = false;
        FMODMusicPlayer.Instance.SetParameter(ParametersListEnum.Parameters.LowPass, 0);
    }

    public void Fail(Action onSpace) {
        timer = 2;
        onTimerEnd = () => {
            this.onSpace = onSpace;
            DisplayText.text = FailureDisplay;
            ContinueText.text = FailureContinue;
            Tint.enabled = true;
            DisplayText.enabled = true;
            ContinueText.enabled = true;
            FMODMusicPlayer.Instance.SetParameter(ParametersListEnum.Parameters.LowPass, 1f);
        };
    }

    public void Success(Action onSpace) {
        timer = 2;
        onTimerEnd = () => {
            this.onSpace = onSpace;
            DisplayText.text = SuccessDisplay;
            ContinueText.text = SuccessContinue;
            Tint.enabled = true;
            DisplayText.enabled = true;
            ContinueText.enabled = true;
            FMODMusicPlayer.Instance.SetParameter(ParametersListEnum.Parameters.LowPass, 1f);
        };
    }
}