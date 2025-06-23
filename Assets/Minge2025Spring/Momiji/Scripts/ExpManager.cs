using UnityEngine;
using CharacterInfo;

public class ExpManager : MonoBehaviour
{
    public int playerExp;
    private int addExp = 10;
    private int upAttack = 50;
    private int level = 0;
    public AllyInfo[] characters;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerExp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerExp >= 40 && level == 0) levelUp();
        if (playerExp >= 90 && level == 1) levelUp();
    }

    void levelUp()
    {
        for (int i = 0; i < 4; i++) characters[i].Attack += upAttack;
        level++;
    }

    public void AddExp(GameObject enemy)
    {
        if (Equals(enemy.gameObject.name, "_BaseEnemyExample"))
        {
            playerExp += addExp;
        }
        else if (Equals(enemy.gameObject.name, "FlyEnemy"))
        {
            playerExp += addExp;
        }
    }
}
