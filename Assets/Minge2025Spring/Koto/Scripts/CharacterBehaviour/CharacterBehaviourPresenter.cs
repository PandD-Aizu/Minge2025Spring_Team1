using CharacterInfo;
using UniRx;
using Unity.XR.OpenVR;
using UnityEngine;

namespace CharacterBehaviour
{
    public class CharacterBehaviourPresenter : MonoBehaviour
    {
        [Header("依存関係")] 
        [SerializeField] private AllyInfo allyInfo;
        [SerializeField] private CharacterBehaviourModel model;
        [SerializeField] private StageCharacterControllerModel characterControllerModel;
        [SerializeField] private CharacterBehaviourView view;
        
        //アクセサ
        public AllyInfo AllyInfo => allyInfo;
        
        // @brief エントリポイント
        private void Start()
        {
            SubscribeEvents();
            allyInfo.Init();
        }
        
        // @brief エントリポイント
        private void Update()
        {
            if(allyInfo.CharacterDeployInfo == CharacterDeployInfo.DEPLOYED)
            {
                view.HideCharacterAttackRange();
                
                switch(allyInfo.CharacterState)
                {
                    // TODO: allyInfoへの情報の渡し方を修正しておく
                
                    // 攻撃時
                    case CharacterState.ATTACK:
                        model.CheckEnemyList();
                        allyInfo.CharacterState = model.AttackEnemy(allyInfo.AttackCoolDown); // 攻撃対象がいる場合は敵を攻撃、いない場合は待機状態に遷移
                        break;
                
                    // 待機時
                    case CharacterState.WAIT:
                        break;
                
                    // 死亡時
                    case CharacterState.DEAD:
                        allyInfo.CharacterDeployInfo = CharacterDeployInfo.NOT_DEPLOYED; // ユニットを非配置状態に変更
                        view.AnimateWithDraw();
                        break;
                }
                
                model.AttackCoolDownTime += Time.deltaTime; // 攻撃クールタイムを更新
            }
            
            if(allyInfo.CharacterDeployInfo == CharacterDeployInfo.NOT_DEPLOYED)
            {
                view.ShowCharacterAttackRange();
                
                // キャラクターの向きによって攻撃範囲のコライダーを変更
                switch (allyInfo.CharacterDirection)
                {
                    case CharacterDirection.UP:
                        model.AttackRangeCollider.size = model.AttackRangeSize[0];
                        model.AttackRangeCollider.center = model.AttackRangeCenter[0];
                        view.AttackRangeSprite.transform.localScale = model.AttackRangeSpriteSize[0];
                        view.AttackRangeSprite.transform.localPosition = model.AttackRangeSpritePosition[0];
                        break;
                
                    case CharacterDirection.DOWN:
                        model.AttackRangeCollider.size = model.AttackRangeSize[1];
                        model.AttackRangeCollider.center = model.AttackRangeCenter[1];
                        view.AttackRangeSprite.transform.localScale = model.AttackRangeSpriteSize[1];
                        view.AttackRangeSprite.transform.localPosition = model.AttackRangeSpritePosition[1];
                        break;
                
                    case CharacterDirection.LEFT:
                        model.AttackRangeCollider.size = model.AttackRangeSize[2];
                        model.AttackRangeCollider.center = model.AttackRangeCenter[2];
                        view.AttackRangeSprite.transform.localScale = model.AttackRangeSpriteSize[2];
                        view.AttackRangeSprite.transform.localPosition = model.AttackRangeSpritePosition[2];
                        break;
                
                    case CharacterDirection.RIGHT:
                        model.AttackRangeCollider.size = model.AttackRangeSize[3];
                        model.AttackRangeCollider.center = model.AttackRangeCenter[3];
                        view.AttackRangeSprite.transform.localScale = model.AttackRangeSpriteSize[3];
                        view.AttackRangeSprite.transform.localPosition = model.AttackRangeSpritePosition[3];
                        break;
                }
            }
            
            CheckRayCast();
        }
        
        // @brief 衝突判定(Enter)
        // @param other 衝突したオブジェクト
        private void OnTriggerEnter(Collider other)
        {
            // 敵が攻撃範囲内に侵入した場合、攻撃対象リストに追加して攻撃状態に遷移
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enter Enemy");
                model.EnemyList.Add(other.gameObject);           // 攻撃対象リストに追加
                allyInfo.CharacterState = CharacterState.ATTACK; // 攻撃状態に遷移
            }
                
        }
        
        // @brief 衝突判定(Exit)
        // @param other 衝突したオブジェクト
        private void OnTriggerExit(Collider other)
        {
            // 敵が攻撃範囲から離れた場合、攻撃対象リストから削除
            if(other.gameObject.CompareTag("Enemy"))
            {
                model.EnemyList.Remove(other.gameObject);　// 攻撃対象リストから削除
            }
        }
        
        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            // 攻撃対象リストに攻撃対象が追加されたら、攻撃対象を再設定
            model.EnemyList
                .ObserveAdd()
                .Subscribe(_ => model.SetAttackPriority());

            // 攻撃アニメーションを再生
            model.IsAttackEnemy
                .Skip(1)
                .Subscribe(_ => view.AnimateAttackEnemy());
            
            // キャラクターのステータスを表示
            model.IsShowCharacterStatus
                .Skip(1)
                .Subscribe((isShow) => view.ShowCharacterStatus(isShow));

            // 撤退アニメーションを再生
            model.IsWithDraw
                .Skip(1)
                .Subscribe((isWithDraw) =>
                {
                    if (isWithDraw)
                    {
                        view.AnimateWithDraw();
                        model.IsWithDraw.Value = false;
                        characterControllerModel.IsShowingCharacterStatus = false;
                    }
                });
        }
        
        // @brief レイキャストを行い、キャラクターのステータスを表示する
        private void CheckRayCast()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Input.GetMouseButtonUp(0) && 
                Physics.Raycast(ray, out hit, Mathf.Infinity, allyInfo.CharacterRaycastLayer) && 
                !model.IsShowCharacterStatus.Value &&
                allyInfo.CharacterDeployInfo == CharacterDeployInfo.DEPLOYED)
            {
                if (hit.collider.gameObject == gameObject && !characterControllerModel.IsShowingCharacterStatus)
                {
                    model.IsShowCharacterStatus.Value = true;
                    characterControllerModel.IsShowingCharacterStatus = true;
                }
            }
            
            if(Input.GetMouseButtonUp(0) && 
               !Physics.Raycast(ray, out hit, Mathf.Infinity, allyInfo.CharacterRaycastLayer) &&
               model.IsShowCharacterStatus.Value &&
               allyInfo.CharacterDeployInfo == CharacterDeployInfo.DEPLOYED)
            {
                model.IsShowCharacterStatus.Value = false;
                characterControllerModel.IsShowingCharacterStatus = false;
            }
        }
    }
}