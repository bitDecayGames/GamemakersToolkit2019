using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TintCanvasController : MonoBehaviour {
    private const string SuccessDisplay = "Ka-nice!";
    private const string SuccessContinue = "Press Spacebar to proceed";
    private const string FailureDisplay = "Ka-failed: ";
    private const string FailureContinue = "Press spacebar to retry";

    public Image Tint;
    public TextMeshProUGUI DisplayText;
    public TextMeshProUGUI ContinueText;

    private Action onSpace = null;

    private Action onTimerEnd;
    private float timer;

    private void Update() {
        if (onSpace != null && Input.GetKeyDown(KeyCode.Space)) {
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

    public void Fail(GameOverReason reason, Action onSpace) {
        timer = 2;
        onTimerEnd = () => {
            this.onSpace = onSpace;
            DisplayText.text = FailureDisplay + ReasonToString(reason);
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

    private string ReasonToString(GameOverReason reason)
    {
        switch(reason)
        {
            case GameOverReason.DIDNT_SKEWER_ALL_INGREDIENTS:
                return "Some Ingredients didn't get skewered!";
            case GameOverReason.DROPPED_INGREDIENT_IN_WATER:
                return "An Ingredient got wet. Dinner is ruined!";
            case GameOverReason.SKEWERD_YOURSELF:
                return "Um, try not to kill yourself.";
            case GameOverReason.SMOOSHED_INGREDIENTS:
                return "Ingredients got squished together. Gross.";
            case GameOverReason.TOUCHED_INGREDIENT:
                return "Don't touch your food.";
            case GameOverReason.SKEWERED_ALL_INGREDIENTS:
                return "Won!";
            default:
                return "I have no idea why. At All.";
        }
    }
}
