using System.Collections.Generic;
using UnityEngine;

namespace EnhanceCardApplySys
{
    public class EnhanceCardApplySysModel : MonoBehaviour
    {
        [SerializeField] private List<EnhanceCard.EnhanceCard> enhanceCards;

        [SerializeField] private GameObject enhanceCardParent;
    }
}