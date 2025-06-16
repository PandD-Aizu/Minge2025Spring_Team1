using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public int playerExp;
    public ScriptableObject[] characters;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerExp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerExp >= 30)
        {
            for (int i = 0; i < 4; i++) ; //characters[i].Attack += 50;
        }
    }

    public void AddExp(GameObject enemy)
    {
        if (Equals(enemy.gameObject.name, "_BaseEnemy"))
        {
            playerExp += 10;
        }
        else if (Equals(enemy.gameObject.name, "FlyEnemy"))
        {
            playerExp += 10;
        }
    }
}
