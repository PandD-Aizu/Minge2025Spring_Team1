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
            if (model.TargetEnemy != null)
            {
                view.RotateToEnemy(model.TargetEnemy.transform);
            }
            
            if (model.TargetEnemy != null && model.CurrentCoolDown >= model.AttackCoolDown)
            {
                // クールタイムをリセット
                model.CurrentCoolDown = 0.0f;
                
                model.TargetEnemy.GetComponent<EnemyStatus>()?.TakeDamage(150); // ターゲットの敵にダメージを与える
            }
            
            // クールダウンの更新
            model.CurrentCoolDown += Time.deltaTime;
        }

        public void OnTriggerStay(Collider other)
        {
            Debug.Log("OnTriggerStay");
            
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