using UnityEngine;
using Utils;

public class MenuController : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            FMODMusicPlayer.Instance.StopThenDestroy();
            CurrentLevelNumber.Instance.LevelNumber = CurrentLevelNumber.Instance.HighestLevel; // first level or highest level reached
            GetComponent<EasyNavigator>().GoToScene("GameScene");
            FMODSoundEffectsPlayer.Instance.PlaySoundEffect(SFX.PressStart);
        }
    }
}