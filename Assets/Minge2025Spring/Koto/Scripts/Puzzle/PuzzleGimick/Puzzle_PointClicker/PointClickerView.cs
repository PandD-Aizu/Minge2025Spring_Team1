using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class PointClickerView : MonoBehaviour
    {
        [Header("魔法陣出現位置の範囲")]
        [MinMaxSlider(-300.0f, 300.0f)] 
        [SerializeField] private Vector2 spawnXRange;
        
        [MinMaxSlider(-200.0f, 100.0f)]
        [SerializeField] private Vector2 spawnYRange;
        
        [Header("ボタン")]
        [SerializeField] private Button point;

        [Header("画像")] 
        [SerializeField] private GameObject magicCircle;        // 魔法陣の画像
        [SerializeField] private List<GameObject> magicCircles; // 魔法陣の画像のリスト
         
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
        
        // @brief 魔法陣を複製
        // @return 複製した魔法陣のオブジェクト
        public GameObject CopyMagicCircle()
        {
            Vector3 spawnRange = new Vector3(Random.Range(spawnXRange.x, spawnXRange.y), 
                                             Random.Range(spawnYRange.x, spawnYRange.y), 
                                             0);                                                   // 出現位置の範囲
            GameObject newMagicCircle = Instantiate(magicCircle, spawnRange, Quaternion.identity); // 魔法陣を複製
            newMagicCircle.SetActive(true);                                                        // 魔法陣を表示
            newMagicCircle.transform.SetParent(gameObject.transform, false);        // 親オブジェクトを設定
            
            PlaySpawnAnim(newMagicCircle); // 出現時のアニメーション
            
            magicCircles.Add(newMagicCircle); // 魔法陣のリストに追加

            return newMagicCircle;
        }
        
        // @brief 出現時のアニメーション
        // @param magicCircle 魔法陣のオブジェクト
        private void PlaySpawnAnim(GameObject magicCircle)
        {
            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(magicCircle.transform.DOScale(0.0f, 0.5f))
                .Append(magicCircle.transform.DOScale(1.0f, 1.0f))
                .OnComplete(() => magicCircle.transform.GetComponent<Button>().interactable = true); // クリック可能にする
        }
        
        // @brief クリック時のアニメーション
        // @param magicCircle 魔法陣のオブジェクト
        public void PlayClickAnim(GameObject magicCircle)
        {
            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(magicCircle.transform.DOScale(1.2f, 0.5f))
                .Append(magicCircle.transform.DOScale(0.0f, 1.0f))
                .OnComplete(() => Destroy(magicCircle)); // 魔法陣を削除
        }
        
        // @brief パーティクルを再生
        public void PlayParticle(GameObject magicCircle)
        {
            magicCircle.transform.GetComponentInChildren<ParticleSystem>().Emit(2);
        }
        
        // @brief パーティクルを引き寄せる
        // TODO: ForceFieldつかうのでいらないかも(保険として残しておく)
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