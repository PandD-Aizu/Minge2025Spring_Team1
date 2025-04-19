using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Dialogue
{
    [RequireComponent(typeof(LoopScrollRect))]
    [DisallowMultipleComponent]
    public sealed class LogWindowView : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource
    {
        [Header("表示させるPrefab")]
        [SerializeField] private GameObject prefab;

        [Header("ボタン")] 
        [SerializeField] private Button backButton;
        
        [Header("要素数")]
        [SerializeField] int totalCount = 5;

        private List<Scenario> scenarioData; // シナリオデータ(ログが表示するデータ)
        private ObjectPool<GameObject> pool; // オブジェクトプール
        
        /* getter と setter */
        public int TotalCount    { get => totalCount; set => totalCount = value; }

        // @brief 初期化処理
        public void Initialize()
        {
            // オブジェクトプールを作成
            pool = new ObjectPool<GameObject>(
                // オブジェクト生成処理
                () => Instantiate(prefab),
                // オブジェクトがプールから取得される時の処理
                o => o.SetActive(true),
                // オブジェクトがプールに戻される時の処理
                o =>
                {
                    o.transform.SetParent(transform);
                    o.SetActive(false);
                });
            
            scenarioData = new List<Scenario>(); // シナリオデータを初期化

            var scrollRect = GetComponent<LoopScrollRect>();
            scrollRect.prefabSource = this;
            scrollRect.dataSource = this;
            scrollRect.totalCount = totalCount;
            scrollRect.RefillCells();
            
            transform.localPosition = new Vector3(0, 1100, 0);
            gameObject.SetActive(false);
            
            // 戻るボタンのイベント登録
            backButton.onClick
                .AddListener(() =>
                {
                    transform.DOLocalMoveY(1100, 0.5f)
                        .SetEase(Ease.OutSine)
                        .OnComplete(() => gameObject.SetActive(false));
                });
        }

        // @brief シナリオデータを追加
        // @param scenario シナリオデータ
        public void AddScenario(Scenario scenario)
        {
            scenarioData.Add(scenario);
            totalCount = scenarioData.Count;
    
            var scrollRect = GetComponent<LoopScrollRect>();
            scrollRect.totalCount = totalCount;
            gameObject.SetActive(true);
            scrollRect.RefillCells();                        // ログを更新
            gameObject.SetActive(false);
        }
        
        // @brief シナリオデータをクリア
        public void ClearScenarioData()
        {
            scenarioData.Clear();
            totalCount = 0;
            
            var scrollRect = GetComponent<LoopScrollRect>();
            scrollRect.totalCount = totalCount;
            gameObject.SetActive(true);
            scrollRect.RefillCells();                        // ログを更新
            gameObject.SetActive(false);
        }

        // @brief 要素が表示される時の処理を書く
        // @param trans 要素のTransform, index 要素の通し番号
        void LoopScrollDataSource.ProvideData(Transform trans, int index)
        {
            trans.GetChild(1).GetComponent<SpriteRenderer>(); // TODO: 画像を表示する処理を書く
            trans.GetChild(2).GetComponent<TextMeshProUGUI>().text = scenarioData[index].name;
            trans.GetChild(3).GetComponent<TextMeshProUGUI>().text = scenarioData[index].sentence;
        }

        // @brief オブジェクトが新しく表示のために必要になった時に呼ばれる
        // @param index 要素の通し番号
        GameObject LoopScrollPrefabSource.GetObject(int index)
        {
            // オブジェクトプールからGameObjectを取得
            return pool.Get();
        }
        
        // @brief オブジェクトが不要になった時に呼ばれる
        // @param trans 要素のTransform
        void LoopScrollPrefabSource.ReturnObject(Transform trans)
        {
            // オブジェクトプールにGameObjectを返却
            pool.Release(trans.gameObject);
        }
    }
}
