using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterDeploySys
{
    public class CharacterDeployView : MonoBehaviour
    {
        [Header("UIを映しているカメラ")]
        [SerializeField] private Camera uiRenderCamera;
        
        [Header("味方のモデルを置く親オブジェクト")] 
        [SerializeField] private GameObject parent;
        
        [Header("味方ユニットのプレハブ")]
        [SerializeField] private List<GameObject> allyPrefabs;
        
        /* getter と setter */
        public Camera UIRenderCamera        { get => uiRenderCamera; }
        public List<GameObject> AllyPrefabs { get => allyPrefabs; }
        
        // @brief キャラクターのプレビューを表示する
        // @param previewChar プレビューするキャラクター, previewPos プレビューする位置
        // TODO: string参照をやめたい
        public void SetPreview(string charName, Vector3 previewPos)
        {
            GameObject previewChar = allyPrefabs.Find(x => x.name == charName);
            preview.SetActive(true);
            previewChar.transform.position = previewPos;
        }
        
        // @brief カーソル位置にキャラクターを配置
        // @param pos カーソルの位置
        public void DeployAlly(string prefabName, Vector3 pos)
        {
            GameObject newGameObject;
            newGameObject = Instantiate(allyPrefabs.Find(x => x.name == prefabName), pos, Quaternion.identity);
            newGameObject.transform.SetParent(parent.transform, false);
            newGameObject.SetActive(true);
        }
    }
}