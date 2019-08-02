using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils {
    public class EasyNavigator : MonoBehaviour {

        public void GoToScene(string sceneName)
        {
            if (!FadeToBlack.Instance.IsFadingOut())
            {
                FadeToBlack.Instance.FadeOutToScene(2f, sceneName);
                FMODSoundEffectsPlayer.Instance.PlaySoundEffect(SFX.Sound);
            }
        }
    }
}