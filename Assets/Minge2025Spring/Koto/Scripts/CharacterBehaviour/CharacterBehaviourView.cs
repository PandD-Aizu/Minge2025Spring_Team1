using UnityEngine;
using UnityEngine.UI;

namespace CharacterBehaviour
{
    public class CharacterBehaviourView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer attackRangeSprite;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Animator animator;
        
        public SpriteRenderer AttackRangeSprite { get => attackRangeSprite; set => attackRangeSprite = value; }
        public Slider HpSlider                  { get => hpSlider; set => hpSlider = value; }
        public Animator Animator                { get => animator; set => animator = value; }

        public void AttackEnemy()
        {
            animator.SetTrigger("isAttackEnemy");
        }
    }
}