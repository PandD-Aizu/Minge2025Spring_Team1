using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Puzzle
{
    public class PointClickerPresenter : MonoBehaviour, IDisposable
    {
        [Header("依存関係")]
        [SerializeField] private PointClickerModel model;
        [SerializeField] private PointClickerView view;

        // @brief エントリポイント
        private void Start()
        {
            model.Init();
            view.Initialize();
            for (int i = 0; i < model.InitMagicCircle; i++)
                CopyMagicCircle();
            
            SubscribeEvents();
        }

        // @brief エントリポイント
        private void Update()
        {
            // 10%の確率で魔法陣を複製
            if (Random.Range(0, 100) < 10 && model.CurrentMagicCircle < model.MaxMagicCircle)
                CopyMagicCircle();

            view.AttractParticle();
        }
        
        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            
        }
        
        // @brief 魔法陣を複製
        private void CopyMagicCircle()
        {
            Debug.Log("CopyMagicCircle");
            GameObject newMagicCircle = view.CopyMagicCircle();
            
            // 魔法陣クリック時のイベントを登録
            newMagicCircle.GetComponent<Button>().onClick
                .AddListener(() =>
                {
                    newMagicCircle.GetComponent<Button>().interactable = false; // クリック不可にする
                    view.PlayParticle(newMagicCircle);                         // パーティクルの再生
                    view.PlayClickAnim(newMagicCircle);          // クリック時のアニメーション
                    model.IsSolved.SetValueAndForceNotify(true); // パズルを解いたことを通知
                    model.CurrentMagicCircle--;                  // 魔法陣の数を減らす
                });
            
            model.CurrentMagicCircle++; // 魔法陣の数を増やす
        }
        
        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {
            
        }
    }
}