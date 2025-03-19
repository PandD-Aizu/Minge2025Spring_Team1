using UnityEngine;

public class RotativePiece : MonoBehaviour
{
    Transform myTransform;
<<<<<<< HEAD
    [Header("�󂯓n���̂���public")][SerializeField] public bool Collect;
    [Header("�N���A�ɕK�v�ȃs�[�X���ǂ���")][SerializeField] public bool NeedPuzzle;
    [Header("������Rotate")][SerializeField] public float CollectRotate_z;
=======
    [Header("受け渡しのためpublic")][SerializeField] public bool Collect;
    [Header("クリアに必要なピースかどうか")][SerializeField] public bool NeedPuzzle;
    [Header("正解のRotate")][SerializeField] public float CollectRotate_z;
>>>>>>> 1afd1afe4bacf805f02fc0eadac5a8332f13ea1f

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

<<<<<<< HEAD
        //���̃s�[�X���������Ă��邩
=======
        //このピースが正解しているか
>>>>>>> 1afd1afe4bacf805f02fc0eadac5a8332f13ea1f
        Collect = true;
        if (NeedPuzzle == false) Collect = true;
        else if (world_angle_z != CollectRotate_z)
        {
            Collect = false;
        }
    }

    public void Click()
    {
        myTransform.Rotate(0.0f, 0.0f, -90.0f, Space.World);
    }
}
