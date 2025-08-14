using System.Collections;
using TMPro;
using UnityEngine;

namespace Title
{
    public class CreditController : MonoBehaviour
    {
        [Header("UIパーツ")]
        [SerializeField] private TextMeshProUGUI creditText;

        [Header("スクロール速度")]
        [SerializeField] private float scrollSpeed = 50.0f;

        [Header("表示するテキスト")]
        [SerializeField] [TextArea(5, 10)] private string creditRollText;

        private Coroutine scrollingCoroutine;
        
        private void OnEnable()
        {
            // もし既にコルーチンが動いていれば停止する
            if (scrollingCoroutine != null)
            {
                StopCoroutine(scrollingCoroutine);
            }
            
            // 新しくスクロールを開始する
            scrollingCoroutine = StartCoroutine(ScrollCredits());
        }
        
        private void OnDisable()
        {
            // 安全のためにコルーチンを停止する
            if (scrollingCoroutine != null)
            {
                StopCoroutine(scrollingCoroutine);
                scrollingCoroutine = null;
            }
        }

        private IEnumerator ScrollCredits()
        {
            creditText.text = creditRollText;
            
            yield return new WaitForEndOfFrame();
            
            var textRectTransform = creditText.GetComponent<RectTransform>();
            var parentRectTransform = (RectTransform)creditText.transform.parent;
            
            float parentHeight = parentRectTransform.rect.height;
            float textHeight = textRectTransform.rect.height;
            
            Vector2 startPosition = new Vector2(
                textRectTransform.anchoredPosition.x, 
                -(parentHeight / 2) - (textHeight / 2)
            );
            
            float targetY = (parentHeight / 2) + (textHeight / 2);
            
            textRectTransform.anchoredPosition = startPosition;
            
            while (textRectTransform.anchoredPosition.y < targetY)
            {
                textRectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
                yield return null;
            }
            
            Debug.Log("スタッフロール終了");
            scrollingCoroutine = null;
        }
    }
}