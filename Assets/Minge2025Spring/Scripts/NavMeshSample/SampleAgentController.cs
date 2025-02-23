using UnityEngine;
using UnityEngine.AI;

public class SampleAgentController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent; // NavMeshAgentのコンポーネント
    [SerializeField] private Transform target;   // 目標のTransform

    private void Update()
    {
        agent.SetDestination(target.position); // NavMeshAgentの目標地点を設定
    }
}
