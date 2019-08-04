using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TintCanvasController : MonoBehaviour
{
    private const string SuccessDisplay = "Ka-nice!";
    private const string SuccessContinue = "Press Spacebar to proceed";
    private const string FailureDisplay = "Ka-failed...";
    private const string FailureContinue = "Press spacebar to retry";

    public Image Tint;
    public TextMeshProUGUI DisplayText;
    public TextMeshProUGUI ContinueText;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Reset();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Fail();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Success();
        }
    }

    private void Reset()
    {
        Tint.enabled = false;
        DisplayText.enabled = false;
        ContinueText.enabled = false;
        FMODMusicPlayer.Instance.SetParameter(ParametersListEnum.Parameters.LowPass, 0);
    }
    
    private void Fail()
    {
        DisplayText.text = FailureDisplay;
        ContinueText.text = FailureContinue;
        Tint.enabled = true;
        DisplayText.enabled = true;
        ContinueText.enabled = true;
        FMODMusicPlayer.Instance.SetParameter(ParametersListEnum.Parameters.LowPass, 1f);
    }

    private void Success()
    {
        DisplayText.text = SuccessDisplay;
        ContinueText.text = SuccessContinue;
        Tint.enabled = true;
        DisplayText.enabled = true;
        ContinueText.enabled = true;
        FMODMusicPlayer.Instance.SetParameter(ParametersListEnum.Parameters.LowPass, 1f);
    }
}