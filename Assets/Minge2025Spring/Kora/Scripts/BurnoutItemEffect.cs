using UnityEngine;
using CharacterDeploySys;
using NUnit.Framework;
using UnityEngine.UI;
using Unity.VisualScripting;
using CharacterInfo;
using Cost;
using UniRx;

//一定時間味方全員の攻撃力をバフするが、その後味方は全滅する
//ReplaceItemで全滅を回避
public class BurnoutItemEffect : MonoBehaviour
{
    [Header("依存関係")]
    [SerializeField] private CharacterDeployModel model;
    [SerializeField] private CostControllerModel costModel;
    [SerializeField] private Text CDTimer;
    [SerializeField] private Text ItemTimer;
    [SerializeField] private GameObject ItemOverlay;
    [SerializeField] private GameObject CDOverlay;
    [Header("アイテムの使用CD")][SerializeField] private float CoolDown = 5f;
    [Header("アイテムの使用時間")][SerializeField] private float burnTime = 5f;
    [Header("アイテムのコスト")][SerializeField] private int cost;
    [Header("バフ(Attack * x)")][SerializeField] private float buff;

    private float limitCDTime;
    private float limitItemTime;
    private bool isUsed = false;
    private bool isUsing = false;
    private int[] InitAttack = new int[4];
    private bool status = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BurnoutEffect()
    {
        Debug.Log("Called BurnoutEffect. Cost is " + costModel.Cost.Value);
        if (!isUsed && costModel.Cost.Value >= cost)
        {
            isUsed = true;
            isUsing = true;
            status = true;
            limitCDTime =CoolDown;
            limitItemTime = burnTime;
            costModel.Cost.Value -= cost;

            //Effect
            ItemOverlay.SetActive(true);
        }
    }

    private void Start()
    {
        //init
        limitCDTime = CoolDown;
        limitItemTime = burnTime;
        isUsed = false;
        isUsing = false;
        CDTimer.text = "";
        ItemTimer.text = "";
        ItemOverlay.SetActive(false);
        CDOverlay.SetActive(false);

        foreach (var allyinfo in model.AllyInfos)
        {
            allyinfo.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsed)
        {
            if (isUsing) //usingItem
            {
                if (status)
                {
                    //Effect
                    ItemOverlay.SetActive(true);

                    //buff
                    for (int i = 0; i < model.AllyInfos.Count; i++)
                    {
                        if (model.AllyInfos[i].CharacterDeployInfo == CharacterDeployInfo.DEPLOYED)
                        {
                            InitAttack[i] = model.AllyInfos[i].Attack;
                            model.AllyInfos[i].Attack = (int)((float)model.AllyInfos[i].Attack * buff);
                        }
                    }
                }

                //ItemTimer
                limitItemTime -= Time.deltaTime;

                if (limitItemTime < 0) //finish
                {
                    ItemTimer.text = "";
                    limitItemTime = burnTime;
                    isUsing = false;
                    status = false;
                    ItemOverlay.SetActive(false);

                    //Effect
                    CDOverlay.SetActive(true);

                    for(int i = 0; i < model.AllyInfos.Count; i++)
                    {
                        if (model.AllyInfos[i].CharacterDeployInfo == CharacterDeployInfo.DEPLOYED)
                        {
                            model.AllyInfos[i].Hp = 0;
                            model.AllyInfos[i].Attack = InitAttack[i];
                        }
                    }
                }
                else
                {
                    ItemTimer.text = limitItemTime.ToString("F1");
                }
            }
            else //CoolDown
            {
                //CDTimer
                limitCDTime -= Time.deltaTime;

                if (limitCDTime < 0) //finish
                {
                    CDTimer.text = "";
                    limitCDTime = CoolDown;
                    isUsed = false;
                    CDOverlay.SetActive (false);
                }
                else
                {
                    CDTimer.text = limitCDTime.ToString("F1");
                }
            }
        }
    }
}
