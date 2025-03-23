using System;
using Unity.Cinemachine;
using UnityEngine;

public class VirtualCameraManager : MonoBehaviour
{
    [Header("Virtual Camera Settings")]
    [SerializeField] CinemachineCamera cinemachineCamera1;
    [SerializeField] CinemachineCamera cinemachineCamera2;

    private void Awake()
    {
        cinemachineCamera1.Priority = 1;
        cinemachineCamera2.Priority = 0;
    }

    public void SwichCamera()
    {
        int tempPriority = cinemachineCamera1.Priority;
        cinemachineCamera1.Priority = cinemachineCamera2.Priority;
        cinemachineCamera2.Priority = tempPriority;
    }
}
