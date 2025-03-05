using DG.Tweening;
using UnityEngine;

namespace Puzzle
{
    public class SamplePuzzleView : MonoBehaviour
    {
        [Header("画像")]
        [SerializeField] private GameObject cookie;         // クッキーの画像(魔力の塊の画像)
        [SerializeField] private ParticleSystem magicPower; // パーティクル(魔力の画像)

        // @brief クッキークリック時にクッキーを拡縮させる
        public void ScalingCookie()
        {
            cookie.transform.DOScale(0.8f, 0.2f)
                .OnComplete(() => cookie.transform.DOScale(1.0f, 0.2f));
        }

        // @brief クッキークリック時にパーティクルを再生する
        public void PlayParticle()
        {
            magicPower.Play();
        }
    }
}