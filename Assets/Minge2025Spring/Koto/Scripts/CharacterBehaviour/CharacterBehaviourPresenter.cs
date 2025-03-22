using CharacterInfo;
using UniRx;
using UnityEngine;

namespace CharacterBehaviour
{
    public class CharacterBehaviourPresenter : MonoBehaviour
    {
        [Header("依存関係")] 
        [SerializeField] private AllyInfo allyInfo;
        [SerializeField] private CharacterBehaviourModel model;
        [SerializeField] private CharacterBehaviourView view;
        
        //アクセサ
        public AllyInfo AllyInfo => allyInfo;
        
        // @brief エントリポイント
        private void Start()
        {
            SubscribeEvents();
        }
        
        // @brief エントリポイント
        private void Update()
        {
            switch(allyInfo.CharacterState)
            {
                // 攻撃時
                case CharacterState.ATTACK:
                    allyInfo.CharacterState = model.AttackEnemy(allyInfo.AttackCoolDown, allyInfo.Attack); // 攻撃対象がいる場合は敵を攻撃、いない場合は待機状態に遷移
                    break;
                
                // 待機時
                case CharacterState.WAIT:
                    break;
                
                // 死亡時
                case CharacterState.DEAD:
                    allyInfo.CharacterDeployInfo = CharacterDeployInfo.NOT_DEPLOYED; // ユニットを非配置状態に変更
                    Destroy(gameObject);　// 味方ユニットを消す
                    break;
            }

            model.AttackCoolDownTime += Time.deltaTime; // 攻撃クールタイムを更新
        }
        
        // @brief 衝突判定(Enter)
        // @param other 衝突したオブジェクト
        private void OnTriggerEnter(Collider other)
        {
            // 敵が攻撃範囲内に侵入した場合、攻撃対象リストに追加して攻撃状態に遷移
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enter Enemy");
                model.EnemyList.Add(other.gameObject);           // 攻撃対象リストに追加
                allyInfo.CharacterState = CharacterState.ATTACK; // 攻撃状態に遷移
            }
                
        }
        
        // @brief 衝突判定(Exit)
        // @param other 衝突したオブジェクト
        private void OnTriggerExit(Collider other)
        {
            // 敵が攻撃範囲から離れた場合、攻撃対象リストから削除
            if(other.gameObject.CompareTag("Enemy"))
            {
                model.EnemyList.Remove(other.gameObject);　// 攻撃対象リストから削除
            }
        }
        
        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            // 攻撃対象リストに攻撃対象が追加されたら、攻撃対象を再設定
            model.EnemyList
                .ObserveAdd()
                .Subscribe(_ => model.SetAttackPriority());

            // 攻撃アニメーションを再生
            model.IsAttackEnemy
                .Skip(1)
                .Subscribe(_ => view.AttackEnemy());
        }
    }
}