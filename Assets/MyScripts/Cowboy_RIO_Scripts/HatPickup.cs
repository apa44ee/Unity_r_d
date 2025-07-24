using UnityEngine;

public class HatPickup : MonoBehaviour
{
    public string targetTag = "Player";
    public Vector3 positionOffset = new Vector3(0, 0.1f, 0); // Зміщення по замовчуванню
    public Vector3 rotationOffset = new Vector3(0, 180f, 0);  // Обертання по замовчуванню

    private Transform attachPoint;
    private bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return;
        if (!other.CompareTag(targetTag)) return;
        
        // Автоматичне пошук точки кріплення на гравці
        var player = other.transform;
        attachPoint = player.Find("HatAnchor") ?? CreateAttachmentPoint(player);
        
        AttachHat();
    }

    private Transform CreateAttachmentPoint(Transform player)
    {
        GameObject anchor = new GameObject("HatAnchor");
        anchor.transform.SetParent(player);
        anchor.transform.localPosition = positionOffset;
        anchor.transform.localEulerAngles = rotationOffset;
        return anchor.transform;
    }

    private void AttachHat()
    {
        transform.SetParent(attachPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        
        // Вимкнути фізику
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
        
        // Вимкнути колайдер
        if (TryGetComponent<Collider>(out var collider))
            collider.enabled = false;

        isPickedUp = true;
    }

    private void LateUpdate()
    {
        if (!isPickedUp || attachPoint == null) return;
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
    }
}