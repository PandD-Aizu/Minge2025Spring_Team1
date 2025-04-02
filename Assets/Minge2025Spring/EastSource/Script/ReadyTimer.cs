using UnityEngine;

public class ReadyTimer
{
    private float timePassed;
    public bool isPassedReadyTime;

    public ReadyTimer()
    {
        isPassedReadyTime = true;
    }

    public void UpdateReadyTime()
    {
        if (isPassedReadyTime)
        {
            timePassed += Time.deltaTime;
            Debug.Log(timePassed);
            if (timePassed > GameSpawnManager.Instance.ReadyTime)
            {
                GameSpawnManager.Instance.UpdateNextSpawn();
                GameSpawnManager.Instance.SpawnEnemy();
                GameSpawnManager.Instance.IsPassedSpawnTime = true;
                GameSpawnManager.Instance.ReadyTimer = null;
            }
        }
    }
}
