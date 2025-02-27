using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace SplashScreen
{
    public class LogoFader : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<bool> IsFadeComplete => isFadeComplete;
        private readonly BoolReactiveProperty isFadeComplete = new BoolReactiveProperty(false);
        
        [SerializeField] private float fadeSpeed = 1.0f;        // フェード速度
        [SerializeField] private float fadeInterval = 1.0f;     // フェード間隔
        [SerializeField] private List<SpriteRenderer> logoList; // ロゴ画像
        
        private Sequence sequence; // シーケンス

        public void Start()
        {
            foreach (var logo in logoList)
                logo.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        // @brief ロゴ画像のフェード処理
        public void FadeSequence()
        {
            // シーケンスを初期化
            sequence = DG.Tweening.DOTween.Sequence();

            foreach (var logo in logoList)
            {
                sequence // シーケンスにフェード処理を追加
                    .Append(logo.DOFade(1.0f, fadeSpeed))
                    .AppendInterval(fadeInterval)
                    .Append(logo.DOFade(0.0f, fadeSpeed))
                    .AppendInterval(fadeInterval);
            }

            // シーケンスが終了したらフラグを立てる
            sequence.OnComplete(() => isFadeComplete.Value = true);
        }
    }
}