using CharacterInfo;
using CharacterUI;
using Cost;
using General;
using UnityEngine;

namespace CharacterDeploySys
{
    public class CharacterDeployPresenter : MonoBehaviour
    {
        [Header("依存関係")]
        [SerializeField] private CharacterDeployModel model;
        [SerializeField] private CharacterDeployView view;
        [SerializeField] private CharacterDeployListen listen;
        [SerializeField] private CostControllerModel costControllerModel;

        // @brief エントリポイント
        private void Start()
        {
            model.Init();
            SubscribeEvents();
        }
        
        // @brief エントリポイント
        private void Update()
        {
            SetCharacterPos();
            view.CheckCharacterWithDraw(model.AllyInfos);
        }

        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            
        }
        
        // @brief キャラクターの位置を設定
        private void SetCharacterPos()
        {
            AllyInfo allyInfo = null;
            Ray uiRay = view.UIRenderCamera.ScreenPointToRay(Input.mousePosition);
            Ray cellRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit characterHit = new RaycastHit();
            RaycastHit cellHit = new RaycastHit();
            
            // キャラクターのUIをクリックしたら、プレビューを表示
            if (Input.GetMouseButton(0) && 
                Physics.Raycast(uiRay, out characterHit, Mathf.Infinity, model.CharacterUILayer) &&
                !model.IsDeployActive)
            {
                allyInfo = characterHit.collider.gameObject.GetComponent<CharacterUIPresenter>().Model; // キャラ情報を取得する
                model.SelectedCharacterName = allyInfo.CharacterName;                                   // 選択中のキャラクター名を設定
                ClickCharacterUI(allyInfo, characterHit);
                listen.ButtonPushEmitter.Play();
            }

            // マウスを押している間、3D空間上にプレビューを表示する
            if (Input.GetMouseButton(0) && 
                Physics.Raycast(cellRay, out cellHit, Mathf.Infinity, model.DeployLayer) && 
                model.IsDeployActive &&
                !model.IsSelectCharaAttackRange)
            {
                PreviewCharacterOnDeployLayer(cellHit);
            }
            
            // マウスを押している状態で、配置可能マス外にカーソルがある場合、プレビューをカーソルに追従するようにする
            if (Input.GetMouseButton(0) && 
                !Physics.Raycast(cellRay, out cellHit, Mathf.Infinity, model.DeployLayer) &&
                model.IsDeployActive &&
                !model.IsSelectCharaAttackRange)
            {
                PreviewCharacterOutDeployLayer(cellRay);
            }
            
            // キャラクターの攻撃範囲を表示する
            if (Input.GetMouseButton(0) && model.IsSelectCharaAttackRange)
            {
                Physics.Raycast(cellRay, out cellHit, Mathf.Infinity, model.CellLayer);
                if(cellHit.point != Vector3.zero)
                    view.ShowCharacterAttackRange(model.SelectedCharacterName, model.AllyInfos, cellHit.transform.position);
            }

            // キャラクターの攻撃範囲を確定する
            if (Input.GetMouseButtonUp(0) && model.IsSelectCharaAttackRange)
            {
                SelectCharaAttackRange();
                listen.SetCharaEmitter.Play();
            }

            // 設置可能であり、マウスを離したときユニットを配置する
            if (Input.GetMouseButtonUp(0) && model.IsDeployActive && model.IsDeployAvailable && !model.IsSelectCharaAttackRange)
            {
                DeployCharacter();
                listen.ButtonPushModernEmitter.Play();
            }
            
            // 設置可能ではないが、マウスを離したときプレビューを初期化する
            if (Input.GetMouseButtonUp(0) && model.IsDeployActive && !model.IsDeployAvailable)
            {
                InitCharacterDeploy();
            }
        }
        
        // @brief キャラクターのプレビューを表示する
        // @param allyInfo キャラクター情報, characterHit キャラクターのヒット情報
        private void ClickCharacterUI(AllyInfo allyInfo, RaycastHit characterHit)
        {
            // コストが足りていたら、プレビューを表示する
            if(costControllerModel.Cost.Value >= allyInfo.Cost)
            {
                //view.ChangeCursorPreviewSprite(allyInfo.CharacterSprite);   // プレビューの画像をキャラの画像に変更
                SetLayer(allyInfo.CharacterDeployableLayer);                // プレビューのレイヤーを変更
                    
                model.Cost = allyInfo.Cost;                                 // コストを設定
                model.SelectedCharacter = characterHit.collider.gameObject; // キャラクターのUIを設定
                model.IsDeployActive = true;                                // ユニット配置可能フラグを立てる
            }
        }

        // @brief 配置可能マスの上でキャラクターのプレビューを表示する
        // @param cellHit 配置可能マスのヒット情報
        private void PreviewCharacterOnDeployLayer(RaycastHit cellHit)
        {
            Vector3 characterDeployPos = cellHit.collider.gameObject.transform.position + Vector3.up; // キャラクターの配置プレビュー = セルの中心座標 + y軸上方向の単位ベクトル
            view.SetPreview(model.SelectedCharacterName, characterDeployPos);                         // プレビューを表示
            model.IsDeployAvailable = true;                                                           // ユニット配置可能フラグを立てる
        }
        
        // @brief 配置可能マス外でキャラクターのプレビューを表示する
        // @param ray レイ
        private void PreviewCharacterOutDeployLayer(Ray ray)
        {
            Vector3 previewPos = GetMouseWorldPosition(ray);
            view.SetPreview(model.SelectedCharacterName, previewPos);
            model.IsDeployAvailable = false;
        }

        // @brief キャラクターを配置
        private void DeployCharacter()
        {
            view.DeployAlly(model.SelectedCharacterName); // ユニットを配置する
            model.IsSelectCharaAttackRange = true;
        }

        // @brief キャラクターの攻撃範囲を確定する
        private void SelectCharaAttackRange()
        {
            costControllerModel.Cost.Value -= model.Cost; // コストを減らす
            model.SelectedCharacter.SetActive(false);     // 選択中のキャラUIを削除する
            model.SetCharacterDeployInfo();
            model.Init();
        }
        
        // @brief キャラ配置の処理を初期化する
        private void InitCharacterDeploy()
        {
            view.InitPreview(model.SelectedCharacterName); // プレビューを初期化
            model.Init();
        }
        
        // @brief 配置可能なレイヤーを変更
        private void SetLayer(LayerMask layer)
        {
            model.DeployLayer = layer;
        }
        
        // @brief マウスのワールド座標を取得
        // @param ray レイ
        // @return マウスのワールド座標
        private Vector3 GetMouseWorldPosition(Ray ray)
        {
            // カーソル位置のワールド座標を取得
            Plane plane = new Plane(Vector3.up, Vector3.zero); // (0, 0, 0)を基準にする
            
            if (plane.Raycast(ray, out float distance))
                return ray.GetPoint(distance) + Vector3.up;
            
            return Vector3.up;
        }
    }
}