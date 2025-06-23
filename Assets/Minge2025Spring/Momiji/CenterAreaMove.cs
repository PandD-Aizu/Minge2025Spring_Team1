using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class CenterAreaMove : MonoBehaviour{
    [SerializeField] private GameObject CenterCannotWalk;
    private float timer;　//時間計算用変数
    private float movingtime = 30f; //床が動くクールタイム
    private float speed = 0.1f;　//床が動くスピード
    private bool flag; //床が上か下かの判定
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
        flag = false;
    }

    // Update is called once per frame
    public async void Update()
    {
        timer += Time.deltaTime;
        
        if (CenterCannotWalk.transform.position.y > -2.75 && flag==true)
        {
            CenterCannotWalk.transform.position += new Vector3(0, -speed, 0); //床を下げる
        }
        else if (CenterCannotWalk.transform.position.y < -0.79 && flag==false)
        {
            CenterCannotWalk.transform.position += new Vector3(0, speed, 0); //床を上げる
        }

        if (timer >= movingtime)　//一定時間経過後に
        {
            if (flag == true)　//床が下がっていたら
            {
                timer = 0;
                flag = false;
                CenterCannotWalk.layer = 10;
                CenterCannotWalk.tag = "EnemyUnWalkable";
            }
            else //床が上がっていたら
            {
                timer = 0;
                flag = true;
                CenterCannotWalk.layer = 9;
                CenterCannotWalk.tag = "EnemyWalkable";
            }
        }
    }
}
