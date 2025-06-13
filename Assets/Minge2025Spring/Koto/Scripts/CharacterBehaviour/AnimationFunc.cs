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
            model.GiveDamageToEnemy(allyInfo.Attack, allyInfo.CharacterType);
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