using System;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class CircuitPresenter : MonoBehaviour, IDisposable
    {
<<<<<<< HEAD:Assets/Minge2025Spring/Kora/Scripts/CircuitPuzzle.cs
        [Header("�ˑ��֌W")]
        [SerializeField] private PointClickerModel model;
        [SerializeField] private PointClickerView view;

        // @brief �G���g���|�C���g
=======
        [Header("依存関係")]
        [SerializeField] private CircuitModel CircuitModel;
        [SerializeField] private PointClickerView view;

        // @brief エントリポイント
>>>>>>> 1afd1afe4bacf805f02fc0eadac5a8332f13ea1f:Assets/Minge2025Spring/Kora/Scripts/CircuitPresenter.cs
        private void Start()
        {

            SubscribeEvents();
        }

<<<<<<< HEAD:Assets/Minge2025Spring/Kora/Scripts/CircuitPuzzle.cs
        // @brief �G���g���|�C���g
=======
        // @brief エントリポイント
>>>>>>> 1afd1afe4bacf805f02fc0eadac5a8332f13ea1f:Assets/Minge2025Spring/Kora/Scripts/CircuitPresenter.cs
        private void Update()
        {
        }

<<<<<<< HEAD:Assets/Minge2025Spring/Kora/Scripts/CircuitPuzzle.cs
        // @brief �C�x���g�Q�̓o�^
=======
        // @brief イベント群の登録
>>>>>>> 1afd1afe4bacf805f02fc0eadac5a8332f13ea1f:Assets/Minge2025Spring/Kora/Scripts/CircuitPresenter.cs
        private void SubscribeEvents()
        {

        }

<<<<<<< HEAD:Assets/Minge2025Spring/Kora/Scripts/CircuitPuzzle.cs
        // @brief ���������[�N��h�����߂̏���
=======
        // @brief メモリリークを防ぐための処理
>>>>>>> 1afd1afe4bacf805f02fc0eadac5a8332f13ea1f:Assets/Minge2025Spring/Kora/Scripts/CircuitPresenter.cs
        public void Dispose()
        {

        }
    }
}
