using DG.Tweening;
using UnityEngine;

namespace Puzzle
{
    // サンプルのパズル
    // UIの描画などを管理するクラス
    public class SamplePuzzleView : MonoBehaviour
    {
        [Header("画像")]
        [SerializeField] private GameObject cookie; // クッキーの画像(魔力の塊の画像)
        
        [Header("パーティクル")]
        [SerializeField] private ParticleSystem magicParticle; // パーティクル(魔力の画像)
        [SerializeField] private Vector3 target;               // 粒子を集めたい位置
        
        private const float FORCE = 50.0f;           // パーティクルの吸引力
        private ParticleSystem.Particle[] particles; // 粒子の個数を管理する配列

        // @brief 描画の初期化関数
        public void Initialize()
        {
            particles = new ParticleSystem.Particle[magicParticle.main.maxParticles];
        }
        
        // @brief クッキークリック時にクッキーを拡縮させる
        public void ScalingCookie()
        {
            cookie.transform.DOScale(0.8f, 0.2f)
                .OnComplete(() => cookie.transform.DOScale(1.0f, 0.2f));
        }

        // @brief クッキークリック時にパーティクルを排出する
        public void PlayParticle()
        {
            magicParticle.Emit(10);
        }
        
        // @brief パーティクルを引き寄せる
        public void AttractParticle()
        {
            // nullチェック
            if (magicParticle == null || target == null) 
                return;

            // パーティクルの最大数を取得
            int maxParticles = magicParticle.main.maxParticles;
            if (particles == null || particles.Length < maxParticles)
            {
                particles = new ParticleSystem.Particle[maxParticles];
            }

            // 粒子の数を取得
            int count = magicParticle.GetParticles(particles);

            // ここで粒子を集める
            for (int i = 0; i < count; i++)
            {
                Vector3 direction = (target - particles[i].position).normalized;
                particles[i].velocity += direction * FORCE * Time.deltaTime;
            }
            
            magicParticle.SetParticles(particles, count);
        }
        
        // @brief パーティクルが他のオブジェクトに衝突したときに呼び出される
        // @param other 衝突したオブジェクト
        private void OnParticleCollision(GameObject other)
        {
            Debug.Log(other.transform.tag);
            if (other.transform.CompareTag("Cost"))
            {
                int numParticlesAlive = magicParticle.GetParticles(particles);

                for (int i = 0; i < numParticlesAlive; i++)
                {
                    particles[i].remainingLifetime = 0; // パーティクルを即座に消す
                }

                magicParticle.SetParticles(particles, numParticlesAlive);
            }
        }
    }
}