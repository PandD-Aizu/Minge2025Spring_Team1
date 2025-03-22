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

        // @brief キャラクターの攻撃モーションを再生
        public void AnimateAttackEnemy()
        {
            animator.SetTrigger("IsAttackEnemy");
        }

        // @brief キャラクターの撤退モーションを再生
        public void AnimateWithDraw()
        {
            animator.SetTrigger("IsWithDraw");
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

        // @brief キャラクターを非表示
        public void HideCharacter()
        {
            gameObject.SetActive(false);
        }
    }
}