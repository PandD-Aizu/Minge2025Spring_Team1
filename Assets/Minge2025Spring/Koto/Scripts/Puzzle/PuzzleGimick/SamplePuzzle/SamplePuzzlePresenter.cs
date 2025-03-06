using UnityEngine;

namespace Puzzle
{
    // サンプルのパズル
    //　ビューとモデルのデータのやり取りを仲介をするクラス
    public class SamplePuzzlePresenter : MonoBehaviour
    {
        [Header("依存関係")]
        [SerializeField] private SamplePuzzleModel model;
        [SerializeField] private SamplePuzzleView view;

        // @brief エントリポイント
        private void Start()
        {
            view.Initialize();
            SubScribeEvents();
        }
        
        // @brief エントリポイント
        private void Update()
        {
            view.AttractParticle();
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