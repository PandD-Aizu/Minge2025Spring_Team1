using CharacterBehaviour;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private CharacterBehaviourPresenter characterBehaviourPresenter;

    private void Update()
    {
        if (characterBehaviourPresenter.AllyInfo.Hp <= 0)
        {
            Destroy(characterBehaviourPresenter.gameObject);
        }
    }
}
