using UnityEngine;

public class HatPickup : MonoBehaviour
{
    public string targetTag = "Player";
    public Transform attachPoint;

    private bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        // Log trigger entry
        Debug.Log("Trigger entered by: " + other.name);

        if (isPickedUp)
        {
            Debug.Log("Hat already picked up.");
            return;
        }

        if (!other.CompareTag(targetTag))
        {
            Debug.Log("Entered object does not have the correct tag.");
            return;
        }

        if (attachPoint == null)
        {
            Debug.Log("Attach point is not assigned.");
            return;
        }

        // Attach the hat to the player
        transform.SetParent(attachPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        isPickedUp = true;
        Debug.Log("Hat successfully picked up.");
    }
}