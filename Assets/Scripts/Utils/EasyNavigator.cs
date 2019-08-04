using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils {
    public class EasyNavigator : MonoBehaviour {

        public void GoToScene(string sceneName)
        {
            if (!FadeToBlack.Instance.IsFadingOut())
            {
                FadeToBlack.Instance.FadeOutToScene(2.5f, sceneName);
            }
        }
        
        public void GoToSceneWithSoundWithClick(string sceneName)
        {
            if (!FadeToBlack.Instance.IsFadingOut())
            {
                FMODSoundEffectsPlayer.Instance.PlaySoundEffect(SFX.Button);
                FadeToBlack.Instance.FadeOutToScene(2.5f, sceneName);
            }
        }

        public void GoToSceneQuick(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }
}