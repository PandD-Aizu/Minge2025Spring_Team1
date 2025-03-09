using CharacterInfo;
using UnityEngine;

namespace CharacterUI
{
    public class CharacterUIPresenter : MonoBehaviour
    {
        [Header("依存関係")] 
        [SerializeField] private AllyInfo model;
        [SerializeField] private CharacterUIView view;
        
        public AllyInfo Model { get => model; }

        // @brief エントリポイント
        private void Start()
        {
            view.Init(model.Cost.ToString(), model.CharacterSprite);
        }
        
        // @brief イベント群の登録
        private void SubscribeEvents()
        {
            
        }
    }
}