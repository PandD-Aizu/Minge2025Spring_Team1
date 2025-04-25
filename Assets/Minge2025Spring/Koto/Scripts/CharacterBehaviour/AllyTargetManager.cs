using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CharacterBehaviour
{
    public class AllyTargetManager : MonoBehaviour, ITargetManagement
    {
        public GameObject SetAttackPrioritySingle(List<GameObject> enemies, Vector3 position)
        {
            return enemies
                .Where(enemy => enemy != null)
                .OrderBy(enemy => Vector3.Distance(position, enemy.transform.position))
                .FirstOrDefault();
        }

        public List<GameObject> SetAttackPriorityMultiple(List<GameObject> enemies, Vector3 position, int maxTargets)
        {
            return enemies
                .Where(enemy => enemy != null)
                .OrderBy(enemy => Vector3.Distance(position, enemy.transform.position))
                .Take(maxTargets)
                .ToList();
        }
        
        public void UpdateTargetList(List<GameObject> enemies, Vector3 position)
        {
            // This method can be used to update the target list if needed
        }
    }
}