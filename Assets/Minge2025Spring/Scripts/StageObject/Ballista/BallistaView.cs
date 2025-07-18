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
            Vector3 direction = target.position - ballistaObject.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            ballistaObject.transform.rotation = Quaternion.Slerp(ballistaObject.transform.rotation, rotation, Time.deltaTime * 2f);
        }
    }
}