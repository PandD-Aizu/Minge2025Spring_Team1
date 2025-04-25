using System.Collections.Generic;
using CharacterInfo;
using UnityEngine;

namespace CharacterBehaviour
{
    public class AnimationFunc : MonoBehaviour
    {
        [SerializeField] private CharacterBehaviourModel model;
        [SerializeField] private CharacterBehaviourView view;

        // @brief 敵攻撃時の処理
        // @param allyInfo 味方キャラクターの情報
        public void AttackEnemy(AllyInfo allyInfo)
        {
            float damage = allyInfo.Attack;
            List<GameObject> targets = model.TargetEnemies;
            
            switch (allyInfo.AttackType)
            {
                case AttackType.CLOSE_RANGE_SINGLE:
                    model.AllyAttack.AttackSingleCloseRange(targets[0], damage);
                    break;
                
                case AttackType.CLOSE_RANGE_MULTIPLE:
                    model.AllyAttack.AttackMultipleCloseRange(targets, damage);
                    break;
                
                case AttackType.LONG_RANGE_SINGLE:
                    model.AllyAttack.AttackSingleLongRange(targets[0], damage);
                    break;
                
                case AttackType.LONG_RANGE_MULTIPLE:
                    model.AllyAttack.AttackMultipleLongRange(targets, damage);
                    break;
            }
            
            model.GiveDamageToEnemy(allyInfo.Attack);
        }

        // @brief キャラクター撤退時の処理
        // @param allyInfo 味方キャラクターの情報
        public void WithDraw(AllyInfo allyInfo)
        {
            view.HideCharacter();         // キャラクターを非表示にする
            model.InitAllyInfo(allyInfo); // 味方キャラクターの一部情報を初期化
        }
    }
}