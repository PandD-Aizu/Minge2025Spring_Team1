using UnityEngine;

namespace StageObject
{
    public class BallistaView : MonoBehaviour
    {
        [SerializeField] private GameObject ballistaObject;

        /// <summary>
        /// Ballistaの向きをターゲットの方向に向ける
        /// </summary>
        /// <param name="target">敵のTransform</param>>
        public void RotateToEnemy(Transform target)
        {
            // ターゲットへの方向ベクトルを計算（モデルの向きに合わせて反転）
            Vector3 direction = ballistaObject.transform.position - target.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            ballistaObject.transform.rotation = Quaternion.Slerp(
                ballistaObject.transform.rotation,
                rotation,
                Time.deltaTime * 2f
            );
        }
    }
}