using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Puzzle
{
    public class BlockScript : MonoBehaviour
    {
        public Rigidbody rb;
        private Vector3 startpos;
        private Vector3 pos;
        public bool flag;
        public TetrisPresenter tetrisPresenter;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            startpos = transform.position;
            pos = transform.localPosition;
            flag = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (flag == true)
            {
                transform.localPosition = new Vector3(Mathf.Sin(Time.time) * 300.0f + pos.x, pos.y, pos.z);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "ClearArea")
            {
                Debug.Log("clear");
                tetrisPresenter.ClearGame();
                transform.localPosition = startpos;
                rb.useGravity = false;
                flag = true;
            }
            else if (collision.gameObject.name == "OutArea")
            {
                Debug.Log("out");
                transform.localPosition = startpos;
                rb.useGravity = false;
                flag = true;
            }
        }

    }
}