using System.Collections.Generic;
using CharacterInfo;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;
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

        public void InitPreview()
        {
            foreach(var allyPrefab in allyPrefabs)
                allyPrefab.SetActive(false);
        }
        
        // @brief キャラクターのプレビューを表示する
        // @param previewChar プレビューするキャラクター, previewPos プレビューする位置
        // TODO: string参照をやめたい
        public void SetPreview(string charName, Vector3 previewPos)
        {
            GameObject previewChar = allyPrefabs.Find(x => x.name == charName);
            previewChar.SetActive(true);
            previewChar.transform.localPosition = previewPos;
        }
        
        // @brief カーソル位置にキャラクターを配置
        // @param pos カーソルの位置
        public void DeployAlly(string charName)
        {
            GameObject deployChar = allyPrefabs.Find(x => x.name == charName);
            deployChar.SetActive(true);
        }

        // @brief キャラクターの攻撃範囲を表示
        // @param charName キャラクターの名前, mousePos マウスの位置
        public void ShowCharacterAttackRange(string charName, List<AllyInfo> allyInfos, Vector3 mousePos)
        {
            GameObject deployChar = allyPrefabs.Find(x => x.name == charName);
            AllyInfo allyInfo = allyInfos.Find(x => x.CharacterName == charName);

            if (deployChar == null || allyInfo == null)
                return;
            
            float dx = mousePos.x - deployChar.transform.position.x;
            float dy = mousePos.z - deployChar.transform.position.z;
            
            float rad = Mathf.Atan2(dy, dx);
            float deg = rad * Mathf.Rad2Deg;
            
            Debug.Log("deg: " + deg);
            
            // カーソルの位置によって、キャラクターの攻撃範囲を変更する
            // Atan2の返り値は-πからπの範囲
            if(deg <= 45 && deg >= -45)        // 右
                allyInfo.CharacterDirection = CharacterDirection.RIGHT;
            else if(deg < 135 && deg > 45)     // 上
                allyInfo.CharacterDirection = CharacterDirection.UP;
            else if(deg <= -135 || deg >= 135) // 左
                allyInfo.CharacterDirection = CharacterDirection.LEFT;
            else if(deg < -45 && deg > -135)   // 下
                allyInfo.CharacterDirection = CharacterDirection.DOWN;
        }
    }
}