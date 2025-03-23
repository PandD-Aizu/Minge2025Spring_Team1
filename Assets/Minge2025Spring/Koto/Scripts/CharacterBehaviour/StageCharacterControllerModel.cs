using UnityEngine;

namespace CharacterBehaviour
{
    public class StageCharacterControllerModel : MonoBehaviour
    {
        [Header("フラグ")]
        [SerializeField] private bool isShowingCharacterStatus; // 撤退ボタンの多重表示を防ぐためのフラグ
        
        public bool IsShowingCharacterStatus { get => isShowingCharacterStatus; set => isShowingCharacterStatus = value; }
    }
}