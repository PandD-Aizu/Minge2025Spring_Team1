using System;
using UnityEngine;

namespace Puzzle
{
    // サンプルのパズル
    //　viewとmodelのデータのやり取りを仲介をするクラス
    public class TetrisPresenter : MonoBehaviour, IDisposable
    {
        [Header("依存関係")]
        [SerializeField] private TetrisModel model;
        [SerializeField] private TetrisView view;
        [SerializeField] private BlockScript block;

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
            // 落とすボタンが押されたときのイベントを登録
            view.PushButton.onClick.AddListener(() =>
            {
                view.ScalingCookie();                        // ボタンの拡縮アニメーション
                block.rb.useGravity = true;
                block.flag = false;
            });
        }

        public void ClearGame()
        {
            block.rb.useGravity = false;
            //view.PlayParticle();                         // パーティクルの再生
            model.IsSolved.SetValueAndForceNotify(true); // パズルを解いたことを通知
        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            
        }
    }
}