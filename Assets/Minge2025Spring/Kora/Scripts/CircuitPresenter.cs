using System;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class CircuitPresenter : MonoBehaviour, IDisposable
    {
        [Header("依存関係")]
        [SerializeField] private CircuitModel CircuitModel;
        [SerializeField] private PointClickerView view;

        // @brief エントリポイント
        private void Start()
        {

            SubscribeEvents();
        }

        // @brief エントリポイント
        private void Update()
        {
        }

        // @brief イベント群の登録
        private void SubscribeEvents()
        {

        }

        // @brief メモリリークを防ぐための処理
        public void Dispose()
        {

        }
    }
}
