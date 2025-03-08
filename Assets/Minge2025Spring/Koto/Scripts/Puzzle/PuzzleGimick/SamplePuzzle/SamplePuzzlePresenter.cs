using System;
using UnityEngine;

namespace Puzzle
{
    // サンプルのパズル
    //　viewとmodelのデータのやり取りを仲介をするクラス
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
            // クッキーボタンが押されたときのイベントを登録
            view.CookieButton.onClick.AddListener(() =>
            {
                view.ScalingCookie();                        // クッキーの拡縮アニメーション
                view.PlayParticle();                         // パーティクルの再生
                model.IsSolved.SetValueAndForceNotify(true); // パズルを解いたことを通知
            });
        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            
        }
    }
}