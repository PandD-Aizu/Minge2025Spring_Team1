using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class CenterAreaMove : MonoBehaviour
{
    [SerializeField] private GameObject CenterEnemyWalk;
    [SerializeField] private GameObject CenterCannotWalk;
    private float timer;
    private float movingtime = 10f;
    private float speed = 0.5f;

    private Transform centerpos;
    private bool flag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
        flag = true;
    }

    // Update is called once per frame
    public async void Update()
    {
        timer += Time.deltaTime;

        if (timer >= movingtime)
        {
            await MovingArea();
            if (flag == true)
            {
                centerpos = CenterCannotWalk.transform;
                //Instantiate(CenterEnemyWalk, centerpos);
                //DestroyImmediate(CenterCannotWalk,true);
                timer = 0;
                flag = false;
            }
            else
            {
                centerpos = CenterEnemyWalk.transform;
                //Instantiate(CenterCannotWalk, centerpos);
                //DestroyImmediate(CenterEnemyWalk,true);
                timer = 0;
                flag = true;
            }
        }
    }

    public async UniTask MovingArea()
    {
        if (flag)
        {
            while (CenterCannotWalk.transform.position.y > -2)
            {
                await UniTask.Delay(1000);
                CenterCannotWalk.transform.position += new Vector3(0, -speed, 0);
            }
        }
        else
        {
            while (CenterCannotWalk.transform.position.y < 0)
            {
                await UniTask.Delay(1000); 
                CenterCannotWalk.transform.position += new Vector3(0, speed, 0);
            }
            
        }
    }
}
