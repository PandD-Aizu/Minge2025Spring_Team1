using Cost;
using UnityEngine;

public class SkipPuzzlephase : MonoBehaviour
{

    [SerializeField] private CostControllerModel model;

    public void run()
    {
        model.Cost.Value = 99;
    }
}
