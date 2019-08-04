using UnityEngine;
using Utils;

public class MenuController : MonoBehaviour
{

    private bool _startPressed;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !_startPressed) {
            FMODMusicPlayer.Instance.StopThenDestroy();
            CurrentLevelNumber.Instance.LevelNumber = CurrentLevelNumber.Instance.HighestLevel; // first level or highest level reached
            GetComponent<EasyNavigator>().GoToScene("Tutorial01");
            FMODSoundEffectsPlayer.Instance.PlaySoundEffect(SFX.PressStart);

            _startPressed = true;
        }
    }
}