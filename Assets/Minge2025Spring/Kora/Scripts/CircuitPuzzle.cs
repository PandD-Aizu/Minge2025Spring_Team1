using System;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class CuircuitPuzzle : MonoBehaviour, IDisposable
    {
        [Header("�ˑ��֌W")]
        [SerializeField] private PointClickerModel model;
        [SerializeField] private PointClickerView view;

        // @brief �G���g���|�C���g
        private void Start()
        {
            model.Init();
            view.Initialize();

            SubscribeEvents();
        }

        // @brief �G���g���|�C���g
        private void Update()
        {
        }

        // @brief �C�x���g�Q�̓o�^
        private void SubscribeEvents()
        {

        }

        // @brief ���������[�N��h�����߂̏���
        public void Dispose()
        {

        }
    }
}
