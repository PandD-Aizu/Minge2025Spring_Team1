using UniRx;
using UnityEngine;

namespace Puzzle
{
    // �T���v���̃p�Y��
    // �f�[�^���Ǘ�����N���X(Button�Ȃǂ͎g���Ȃ�)
    [CreateAssetMenu(fileName = "CircuitPuzzle", menuName = "ScriptableObject/CircuitPuzzle")]
    public class Circuit : AbstractPuzzleModel
    {
        [Header("�v���C���[�ɗ^����R�X�g")]
        [SerializeField] private int GIVECOST = 10; // �v���C���[�ɗ^����R�X�g

        /* getter �� setter */
        public override BoolReactiveProperty IsSolved { get => isSolved; set => isSolved = value; }

        // @brief �v���C���[�ɃR�X�g��^����
        // @return �v���C���[�ɗ^����R�X�g
        public override int GiveCost()
        {
            return GIVECOST;
        }
    }
}