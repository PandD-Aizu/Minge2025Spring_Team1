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

        public void WithDraw(AllyInfo allyInfo)
        {
            view.HideCharacter();         // キャラクターを非表示にする
            model.InitAllyInfo(allyInfo); // 味方キャラクターの一部情報を初期化
        }
    }
}