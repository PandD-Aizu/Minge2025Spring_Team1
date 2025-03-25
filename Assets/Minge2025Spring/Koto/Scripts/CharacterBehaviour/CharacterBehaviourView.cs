using UnityEngine;
using UnityEngine.UI;

namespace CharacterBehaviour
{
    public class CharacterBehaviourView : MonoBehaviour
    {
        [SerializeField] private GameObject attackRangeSprite;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private GameObject withDrawButton;
        [SerializeField] private GameObject characterStatusPanel;
        [SerializeField] private Animator animator;
        
        public GameObject AttackRangeSprite     { get => attackRangeSprite; set => attackRangeSprite = value; }
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
            
            // アニメーション後、キャラクターを非表示にする
        }

        // @brief キャラクターの攻撃範囲を表示
        public void ShowCharacterAttackRange()
        {
            attackRangeSprite.SetActive(true);
        }
        
        // @brief キャラクターの攻撃範囲を非表示
        public void HideCharacterAttackRange()
        {
            attackRangeSprite.SetActive(false);
        }

        // @brief キャラクターのステータスを表示
        // @param isShow ステータスを表示するかどうか
        public void ShowCharacterStatus(bool isShow)
        {
            if (isShow)
            {
                characterStatusPanel.SetActive(true);
                withDrawButton.SetActive(true);
                ShowCharacterAttackRange();
            }
            else
            {
                characterStatusPanel.SetActive(false);
                withDrawButton.SetActive(false);
                HideCharacterAttackRange();
            }
        }

        // @brief キャラクターを非表示
        public void HideCharacter()
        {
            gameObject.SetActive(false);
        }
    }
}