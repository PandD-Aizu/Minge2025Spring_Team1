using UnityEngine;
using UnityEngine.UI;

public class RotativePiece : MonoBehaviour
{
    Transform myTransform;
    [Header("受け渡しのためpublic")][SerializeField] public bool Collect;
    [Header("クリアに必要なピースかどうか")][SerializeField] public bool NeedPuzzle;
    [Header("正解のRotate")][SerializeField] public float CollectRotate_z;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myTransform = this.transform;
        Vector3 worldAngle = myTransform.eulerAngles;
        float world_angle_z = worldAngle.z;

        //このピースが正解しているか
        Collect = true;
        if (NeedPuzzle == false) Collect = false;
        if (world_angle_z != CollectRotate_z) Collect = false; 
    }

    public void Click()
    {
        myTransform.Rotate(0.0f, 0.0f, -90.0f, Space.World);
    }
}
