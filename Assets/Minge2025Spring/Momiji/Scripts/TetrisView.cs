using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    // サンプルのパズル
    // UIの描画などを管理するクラス
    public class TetrisView : MonoBehaviour
    {
        [Header("ボタン")]
        [SerializeField] private Button pushButton; // 落とすボタン
        
        [Header("画像")]
        [SerializeField] private GameObject push; // ボタンの画像
        
        [Header("パーティクル")]
        [SerializeField] private ParticleSystem magicParticle; // パーティクル(魔力の画像)
        [SerializeField] private Vector3 target;               // 粒子を集めたい位置
        
        private const float FORCE = 50.0f;           // パーティクルの吸引力
        private ParticleSystem.Particle[] particles; // 粒子の個数を管理する配列
        
        /* getter と setter */
        public Button PushButton  => pushButton;

        // @brief 描画の初期化関数
        public void Initialize()
        {
            particles = new ParticleSystem.Particle[magicParticle.main.maxParticles];
        }
        
        // @brief ボタンクリック時にボタンを拡縮させる
        public void ScalingCookie()
        {
            push.transform.DOScale(0.8f, 0.2f)
                .OnComplete(() => push.transform.DOScale(1.0f, 0.2f));
        }

        // @brief クリア時にパーティクルを排出する
        public void PlayParticle()
        {
            magicParticle.Emit(5);
        }
        
        // @brief パーティクルを引き寄せる
        // TODO: ForceField使うのでいらないかも(保険として残しておく)
        public void AttractParticle()
        {
            // nullチェック
            if (magicParticle == null) 
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
    }
}