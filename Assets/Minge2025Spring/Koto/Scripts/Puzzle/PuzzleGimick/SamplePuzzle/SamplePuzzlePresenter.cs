using UnityEngine;

namespace Puzzle
{
    public class SamplePuzzlePresenter : MonoBehaviour
    {
        [Header("依存関係")]
        [SerializeField] private SamplePuzzleModel model;
        [SerializeField] private SamplePuzzleView view;

        /* エントリポイント */
        private void Start()
        {
            SubScribeEvents();
        }
        
        // @brief イベント群の登録
        private void SubScribeEvents()
        {
            model.CookieButton.onClick.AddListener(() =>
            {
                view.ScalingCookie();
                view.PlayParticle();
            });
        }
    }
}