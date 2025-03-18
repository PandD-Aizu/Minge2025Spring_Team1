using UnityEngine;
using UnityEngine.UI;

public class RotativePiece : MonoBehaviour
{
    Transform myTransform;
    [Header("�󂯓n���̂���public")][SerializeField] public bool Collect;
    [Header("�N���A�ɕK�v�ȃs�[�X���ǂ���")][SerializeField] public bool NeedPuzzle;
    [Header("������Rotate")][SerializeField] public float CollectRotate_z;

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

        //���̃s�[�X���������Ă��邩
        Collect = true;
        if (NeedPuzzle == false) Collect = false;
        if (world_angle_z != CollectRotate_z) Collect = false; 
    }

    public void Click()
    {
        myTransform.Rotate(0.0f, 0.0f, -90.0f, Space.World);
    }
}
