using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupRange = 5f;     // Range to detect objects
    public Transform holdPoint;        // Position where the object will be held
    public LayerMask pickupLayer;      // LayerMask to detect only pickable objects
    private Rigidbody heldObject;     // Reference to the currently held object

    [Header("Throwing Settings")]
    public float maxThrowForce = 25f;     // Maximum throw force
    public float throwChargeRate = 12.5f; // Rate at which throw force charges
    private float currentThrowForce;     // Current throw force
    private bool isChargingThrow;        // Tracks whether throw is being charged

    [Header("References")]
    public Camera playerCamera;       

    [Header("Keybinds")]
    public KeyCode pickupKey = KeyCode.E;      
    public KeyCode throwKey = KeyCode.Mouse0;  

    private void Update()
    {
        DebugRaycastCheck();

        if (Input.GetKeyDown(pickupKey))
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                DropObject();
            }
        }

        HandleThrowInput();
    }

    private void FixedUpdate()
    {
      
        if (heldObject != null)
        {
            MoveHeldObject();
        }
    }

    private void HandleThrowInput()
    {
        // Start charging the throw
        if (Input.GetKeyDown(throwKey) && heldObject != null)
        {
            isChargingThrow = true;
            currentThrowForce = 0f; // Reset throw force
            Debug.Log("Charging throw...");
        }

        // Increment the throw force while holding the key
        if (isChargingThrow && Input.GetKey(throwKey))
        {
            currentThrowForce += throwChargeRate * Time.deltaTime;
            currentThrowForce = Mathf.Clamp(currentThrowForce, 0f, maxThrowForce);
            Debug.Log($"Throw Power: {currentThrowForce:F1}");
        }

        // Release the throw
        if (Input.GetKeyUp(throwKey) && heldObject != null)
        {
            ThrowObject(currentThrowForce);
            isChargingThrow = false;
            Debug.Log($"Threw object with force: {currentThrowForce:F1}");
        }
    }

    private void TryPickupObject()
    {
        // Raycast from the camera in its forward direction
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer))
        {
            if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                // Pick up the object
                heldObject = rb;
                heldObject.useGravity = false;
                heldObject.drag = 10f;

                // Parent the object to the hold point
                heldObject.transform.parent = holdPoint;
                heldObject.velocity = Vector3.zero; // Stop any initial motion
                heldObject.transform.position = holdPoint.position;

                Debug.Log($"Picked up: {heldObject.name}");
            }
        }
    }

    private void DropObject()
    {
        if (heldObject == null) return;

        // Detach the object
        heldObject.useGravity = true;
        heldObject.drag = 0f;
        heldObject.transform.parent = null; // Detach from holdPoint

        Debug.Log($"Dropped: {heldObject.name}");
        heldObject = null;
    }

    private void ThrowObject(float throwForce)
    {
        if (heldObject == null) return;

        // Throw the currently held object
        heldObject.useGravity = true;
        heldObject.drag = 0f;
        heldObject.transform.parent = null;

        // Apply a forward force in the direction of the camera
        heldObject.AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse);

        Debug.Log($"Threw object with force: {throwForce:F1}");
        heldObject = null;
    }

    private void MoveHeldObject()
    {
        // Smoothly move the object to the hold point position
        Vector3 targetPosition = holdPoint.position;
        Quaternion targetRotation = holdPoint.rotation;

        heldObject.MovePosition(Vector3.Lerp(heldObject.position, targetPosition, Time.fixedDeltaTime * 10f));
        heldObject.MoveRotation(Quaternion.Slerp(heldObject.rotation, targetRotation, Time.fixedDeltaTime * 10f));
    }

    private void DebugRaycastCheck()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            //Debug.Log($"Looking at: {hit.collider.name}");
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.red);
        }
    }

    private void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * pickupRange);
        }
    }
}
