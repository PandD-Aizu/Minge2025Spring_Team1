using UnityEngine;
using UnityEngine.UI;

namespace CharacterBehaviour
{
    public class CharacterBehaviourView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer attackRangeSprite;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private GameObject withDrawButton;
        [SerializeField] private GameObject characterStatusPanel;
        [SerializeField] private Animator animator;
        
        public SpriteRenderer AttackRangeSprite { get => attackRangeSprite; set => attackRangeSprite = value; }
        public Slider HpSlider                  { get => hpSlider; set => hpSlider = value; }
        public Animator Animator                { get => animator; set => animator = value; }

        public void AttackEnemy()
        {
            animator.SetTrigger("isAttackEnemy");
        }

        // @brief キャラクターのステータスを表示
        // @param isShow ステータスを表示するかどうか
        public void ShowCharacterStatus(bool isShow)
        {
            if (isShow)
            {
                characterStatusPanel.SetActive(true);
                withDrawButton.SetActive(true);
            }
            else
            {
                characterStatusPanel.SetActive(false);
                withDrawButton.SetActive(false);
            }
        }
    }
}