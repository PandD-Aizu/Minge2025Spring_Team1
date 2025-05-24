using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace EnhanceCardApplySys
{
    public class EnhanceCardApplySysModel : MonoBehaviour
    {
        [SerializeField] private List<EnhanceCard.EnhanceCard> enhanceCards;
        [SerializeField] private GameObject enhanceCardParent;
        [SerializeField] private LayerMask enhanceCardUILayer;
        [SerializeField] private LayerMask characterLayer;

        private ReactiveProperty<bool> isDeployActive = new ReactiveProperty<bool>();

        public LayerMask EnhanceCardUILayer => enhanceCardUILayer;
        public LayerMask CharacterLayer => characterLayer;
        public bool IsDeployActiveValue { get => isDeployActive.Value; set => isDeployActive.Value = value; }
        public IObservable<bool> IsDeployActive => isDeployActive.AsObservable();
        
    }
}