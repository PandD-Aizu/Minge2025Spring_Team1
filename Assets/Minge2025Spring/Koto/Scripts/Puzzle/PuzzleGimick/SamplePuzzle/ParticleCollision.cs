using UnityEngine;

namespace Puzzle
{
    public class ParticleCollision : MonoBehaviour
    {
        // @brief パーティクルが他のオブジェクトに衝突したときに呼び出される
        // @param other 衝突したオブジェクト
        private void OnParticleCollision(GameObject other)
        {
            // CostUIに衝突したとき
            if (other.transform.CompareTag("Cost"))
            {
                // TODO: パーティクルが衝突したときにCostUIの表示を変えたいなとか
                // 内部的な値は変えずに表示だけを変える
            }
        }
    }
}