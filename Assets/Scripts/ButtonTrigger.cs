using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    public string pickupTag = "PickUp"; // Tag for pickable objects

    [Header("Target Object")]
    public GameObject objectToDeactivate; // The object to deactivate/activate

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the correct tag
        if (other.CompareTag(pickupTag))
        {
            Debug.Log("Trigger Activated by: " + other.name);

            // Deactivate the target object
            SetTargetObjectActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object leaving the trigger has the correct tag
        if (other.CompareTag(pickupTag))
        {
            Debug.Log("Trigger Deactivated by: " + other.name);

            // Reactivate the target object
            SetTargetObjectActive(true);
        }
    }

    private void SetTargetObjectActive(bool isActive)
    {
            objectToDeactivate.SetActive(isActive);
    }
}