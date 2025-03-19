using UnityEngine;
using System.Collections.Generic;
using Puzzle;


public class CheckClear : MonoBehaviour
{
    //初期化
    private List<GameObject> obj_List = new List<GameObject>();
    private int i;
    GameObject obj;
    RotativePiece script;
    private bool clear = true;
    private bool once = true;
    [SerializeField] private CircuitModel model;

    void Start()
    {
        //パズルのピースを取得
        obj_List.Add(GameObject.Find("RotatablePiece"));
        obj_List.Add(GameObject.Find("RotatablePiece (1)"));
        obj_List.Add(GameObject.Find("RotatablePiece (2)"));
        obj_List.Add(GameObject.Find("RotatablePiece (3)"));
    }

    void Update()
    {
        //パズルをクリアしているかどうか
<<<<<<< HEAD
=======
        clear = true;
>>>>>>> 1afd1afe4bacf805f02fc0eadac5a8332f13ea1f
        for (i = 0; i < obj_List.Count; i++)
        {
            obj = obj_List[i];
            script = obj.GetComponent<RotativePiece>();
            if (!script.Collect) clear = false;
        }
        if (clear && once)
        {
            Debug.Log("Clear Puzzle_Curcuit");
            model.IsSolved.SetValueAndForceNotify(true);
            once = false;
        }
    }
}
