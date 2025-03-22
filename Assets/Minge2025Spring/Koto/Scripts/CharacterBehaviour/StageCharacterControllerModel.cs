using UnityEngine;

namespace CharacterBehaviour
{
    public class StageCharacterControllerModel : MonoBehaviour
    {
        [Header("フラグ")]
        [SerializeField] private bool isShowingCharacterStatus;
        
        public bool IsShowingCharacterStatus { get => isShowingCharacterStatus; set => isShowingCharacterStatus = value; }
        
        
    }
}