using UnityEngine;
using Unity.Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineCamera targetCamera;
    public CinemachineCamera freeLookCamera;   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetCamera.Prioritize();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            freeLookCamera.Prioritize();
        }
    }
}