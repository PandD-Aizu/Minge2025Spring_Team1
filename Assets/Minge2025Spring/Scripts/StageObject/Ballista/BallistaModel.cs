using UnityEngine;

namespace StageObject
{
    public class BallistaModel : MonoBehaviour
    {
        [SerializeField] private float attackCoolDown = 2f; // 攻撃クールダウン時間
        private float currentCoolDown = 0f; // 現在のクールダウン時間
        
        [SerializeField] private GameObject targetEnemy;
        
        public float AttackCoolDown { get => attackCoolDown; private set => attackCoolDown = value; }
        public float CurrentCoolDown { get => currentCoolDown; set => currentCoolDown = value; }
        public GameObject TargetEnemy { get => targetEnemy; set => targetEnemy = value; }
    }
}