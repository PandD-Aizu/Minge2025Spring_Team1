using UnityEngine;
using System.Collections.Generic;


public class CheckClear : MonoBehaviour
{
    //初期化
    private List<GameObject> obj_List = new List<GameObject>();
    private int i;
    GameObject obj;
    RotativePiece script;
    private bool clear = true;

    void Start()
    {
        //パズルのピースを取得
        obj_List.Add(GameObject.Find("RotatablePiece"));
        obj_List.Add(GameObject.Find("RotatablePiece (1)"));
        obj_List.Add(GameObject.Find("RotatablePiece (2)"));
        obj_List.Add(GameObject.Find("RotatablePiece (3)"));

        if (clear) Debug.Log("Clear True");
    }

    void Update()
    {
        //パズルをクリアしているかどうか
        for (i = 0; i < obj_List.Count; i++)
        {
            obj = obj_List[i];
            script = obj.GetComponent<RotativePiece>();
            clear = true;
            if (!script.Collect) clear = false;
        }
        if (clear) Debug.Log("Clear Puzzle_Curcuit");
    }
}
