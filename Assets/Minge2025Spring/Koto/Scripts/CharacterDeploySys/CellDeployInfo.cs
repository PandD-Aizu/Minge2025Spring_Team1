using UnityEngine;

namespace CharacterDeploySys
{
    public class CellDeployInfo : MonoBehaviour
    {
        [SerializeField] private bool isDeployed;
        
        public bool IsDeployed { get => isDeployed; set => isDeployed = value; }
    }
}