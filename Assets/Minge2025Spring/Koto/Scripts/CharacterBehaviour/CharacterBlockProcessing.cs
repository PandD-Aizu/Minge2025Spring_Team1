using CharacterInfo;
using UnityEngine;

namespace CharacterBehaviour
{
    public class CharacterBlockProcessing : MonoBehaviour
    {
        // @brief 現在のブロック数をチェックする
        // @param allyInfo 味方キャラクターの静的な情報, model キャラクターの動的な情報
        public bool CheckCurrentBlockNum(AllyInfo allyInfo, CharacterBehaviourModel model)
        {
            if (model.EnemyListValue.Count > allyInfo.BlockNum)
                return false;
            else
                return true;
        }

        // @brief ブロック数を変更する
        // @param allyInfo 味方キャラクターの静的な情報, newBlockNum 新しいブロック数
        public void ChangeBlockNum(AllyInfo allyInfo, int newBlockNum)
        {
            allyInfo.BlockNum = newBlockNum;
        }
    }
}