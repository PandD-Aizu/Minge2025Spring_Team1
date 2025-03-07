using System;
using UnityEngine;

namespace Puzzle
{
    // サンプルのパズル
    //　ビューとモデルのデータのやり取りを仲介をするクラス
    public class SamplePuzzlePresenter : MonoBehaviour, IDisposable
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
            view.CookieButton.onClick.AddListener(() =>
            {
                view.ScalingCookie();
                view.PlayParticle();
                model.IsSolved.SetValueAndForceNotify(true);
            });
        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            
        }
    }
}