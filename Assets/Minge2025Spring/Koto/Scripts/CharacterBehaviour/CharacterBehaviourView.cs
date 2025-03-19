using UnityEngine;
using UnityEngine.UI;

namespace CharacterBehaviour
{
    public class CharacterBehaviourView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer characterSprite;
        [SerializeField] private SpriteRenderer attackRangeSprite;
        [SerializeField] private Slider hpSlider;
        
        public SpriteRenderer CharacterImage    { get => characterSprite; set => characterSprite = value; }
        public SpriteRenderer AttackRangeSprite { get => attackRangeSprite; set => attackRangeSprite = value; }
        public Slider HpSlider                  { get => hpSlider; set => hpSlider = value; }
    }
}