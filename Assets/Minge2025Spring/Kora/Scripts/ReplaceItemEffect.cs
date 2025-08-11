using UnityEngine;
using CharacterDeploySys;
using NUnit.Framework;
using UnityEngine.UI;
using Unity.VisualScripting;
using CharacterInfo;
using Cost;
using UniRx;

//味方全員を撤退させ、その分のコストを獲得

public class ReplaceItemEffect : MonoBehaviour
{
    [Header("依存関係")]
    [SerializeField] private CharacterDeployModel model;
    [SerializeField] private CostControllerModel costModel;
    [SerializeField] private Text timer;
    [SerializeField] private GameObject CDOverlay;
    [Header("アイテムの使用CD")][SerializeField] private float CoolDown = 5f;
    [Header("アイテムのコスト")][SerializeField] private int cost;

    private float limitTime;
    private bool isUsable = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ReplaceEffect()
    {
        Debug.Log("Called ReplaceEffect. Cost is " + costModel.Cost.Value);
        if (isUsable && costModel.Cost.Value >= cost) 
        {
            isUsable = false;
            limitTime = CoolDown;

            int sum = 0;
            foreach (var allyinfo in model.AllyInfos)
            {
                if (allyinfo.CharacterDeployInfo == CharacterDeployInfo.DEPLOYED)
                {
                    sum += allyinfo.Cost;
                    allyinfo.Hp = 0;
                }
            }

            Debug.Log("Return cost" + sum);
            costModel.Cost.Value += sum - cost;

            //Effect
            CDOverlay.SetActive(true);
        }
    }

    private void Start()
    {
        limitTime = CoolDown;
        isUsable = true;
        timer.text = "";
        CDOverlay.SetActive(false);

        foreach (var allyinfo in model.AllyInfos)
        {
            allyinfo.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUsable) //coolDown
        {

            limitTime -= Time.deltaTime;

            if (limitTime < 0)
            {
                timer.text = "";
                limitTime = CoolDown;
                isUsable = true;
                CDOverlay.SetActive(false);
            }
            else
            {
                timer.text = limitTime.ToString("F1");
            }
        }
    }
}
