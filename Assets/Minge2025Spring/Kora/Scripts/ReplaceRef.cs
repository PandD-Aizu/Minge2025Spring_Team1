using System.Drawing;
using UnityEngine;

public class ReplaceRef : MonoBehaviour
{
    [SerializeField] private float scale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(scale, scale, scale);
    }
}
