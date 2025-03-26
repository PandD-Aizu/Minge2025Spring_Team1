using CharacterInfo;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterBehaviour
{
    public class CharacterBehaviourView : MonoBehaviour
    {
        [Header("カメラ")]
        [SerializeField] private CinemachineCamera camera;
        
        [Header("攻撃範囲のスプライト")]
        [SerializeField] private GameObject attackRangeSprite;
        
        [Header("HPスライダー")]
        [SerializeField] private Slider hpSlider;
        
        [Header("撤退ボタン")]
        [SerializeField] private GameObject withDrawButton;
        
        [Header("キャラクターステータスパネル")]
        [SerializeField] private GameObject characterStatusPanel;
        
        [Header("アニメーター")]
        [SerializeField] private Animator animator;
        
        public GameObject AttackRangeSprite     { get => attackRangeSprite; set => attackRangeSprite = value; }
        public Slider HpSlider                  { get => hpSlider; set => hpSlider = value; }
        public Animator Animator                { get => animator; set => animator = value; }

        
        // @brief カメラの優先度を設定
        // @param priority カメラの優先度
        public void SetCameraPriority(int priority)
        {
            camera.Priority = priority;
        }
        
        public void UpdateHpSlider(AllyInfo allyInfo)
        {
            if(allyInfo.MaxHp != hpSlider.maxValue)
                hpSlider.maxValue = allyInfo.MaxHp;
            
            hpSlider.value = allyInfo.Hp;
        }
        
        // @brief キャラクターの攻撃モーションを再生
        public void AnimateAttackEnemy()
        {
            animator.SetTrigger("IsAttackEnemy");
        }

        public void AnimateWait()
        {
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Wait"))
            {
                animator.SetTrigger("IsWait");
            }
        }

        // @brief キャラクターの撤退モーションを再生
        public void AnimateWithDraw()
        {
            animator.SetTrigger("IsWithDraw");
        }

        // @brief キャラクターの攻撃範囲を表示
        public void ShowCharacterAttackRange()
        {
            attackRangeSprite.SetActive(true);
        }
        
        // @brief キャラクターの攻撃範囲を非表示
        public void HideCharacterAttackRange()
        {
            attackRangeSprite.SetActive(false);
        }

        // @brief キャラクターのステータスを表示
        // @param isShow ステータスを表示するかどうか
        public void ShowCharacterStatus(bool isShow)
        {
            if (isShow)
            {
                characterStatusPanel.SetActive(true);
                withDrawButton.SetActive(true);
                ShowCharacterAttackRange();
                SetCameraPriority(10);
            }
            else
            {
                characterStatusPanel.SetActive(false);
                withDrawButton.SetActive(false);
                HideCharacterAttackRange();
                SetCameraPriority(0);
            }
        }

        // @brief キャラクターを非表示
        public void HideCharacter()
        {
            gameObject.SetActive(false);
        }
    }
}