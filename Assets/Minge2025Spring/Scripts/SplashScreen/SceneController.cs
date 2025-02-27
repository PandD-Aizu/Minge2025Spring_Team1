using UniRx;
using UnityEngine;

namespace SplashScreen
{
    public class SceneController : MonoBehaviour
    {
        [Header("依存関係")]
        [SerializeField] private LogoFader logoFader;

        private void Start()
        {
            logoFader.FadeSequence();
            logoFader.IsFadeComplete
                .Where(isComplete => isComplete)
                .Take(1)
                .Subscribe(_ => General.SceneController.LoadSceneAsync("Title"));
        }
    }
}

