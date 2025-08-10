using System;
using UnityEngine;

namespace StageObject
{
    public class BallistaPresenter : MonoBehaviour
    {
        [SerializeField] private BallistaModel model;
        [SerializeField] private BallistaView view;

        public void Update()
        {
            // ターゲットが設定されている場合、敵の方向に向ける
            if (model.TargetEnemy != null)
            {
                view.RotateToEnemy(model.TargetEnemy.transform);
                view.PlayMoveAnimation();
            }
            
            // クールダウンが完了している場合、攻撃を実行
            if (model.TargetEnemy != null && model.CurrentCoolDown >= model.AttackCoolDown)
            {
                model.CurrentCoolDown = 0.0f; // クールタイムをリセット
                
                view.PlayAttackAnimation();
                model.TargetEnemy.GetComponent<EnemyStatus>()?.TakeDamage(150); // ターゲットの敵にダメージを与える
            }
            
            // クールダウンの更新
            model.CurrentCoolDown += Time.deltaTime;
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") && model.TargetEnemy == null)
            {
                model.TargetEnemy = other.gameObject;
            }
        }
        
        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") && model.TargetEnemy == other.gameObject)
            {
                model.TargetEnemy = null;
            }
        }
    }

}