using CharacterInfo;
using UnityEngine;
using UnityEngine.Events;

namespace CharacterBehaviour
{
    public class AnimationFunc : MonoBehaviour
    {
        [SerializeField] private CharacterBehaviourModel model;
        [SerializeField] private CharacterBehaviourView view;

        public void AttackEnemy(AllyInfo allyInfo)
        {
            model.GiveDamageToEnemy(allyInfo.Attack);
        }

        public void WithDraw()
        {
            view.HideCharacter();
        }
        
    }
}