using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupRange = 5f;     // Range to detect objects
    public Transform holdPoint;        // Position where the object will be held
    public LayerMask pickupLayer;      // LayerMask to detect only pickable objects
    private Rigidbody heldObject;      // Reference to the currently held object

    [Header("Throwing Settings")]
    public float maxThrowForce = 1000f;  // Maximum throw force
    public float throwChargeRate = 500f; // Rate at which throw force charges
    private float currentThrowForce;     // Current throw force
    private bool isChargingThrow;        // Tracks whether throw is being charged

    [Header("UI Elements")]
    public Slider throwChargeBar;       // Reference to the UI Slider

    [Header("References")]
    public Camera playerCamera;         // Reference to the player's camera

    [Header("Keybinds")]
    public KeyCode pickupKey = KeyCode.E;      // Key to pick up/drop objects
    public KeyCode throwKey = KeyCode.Mouse1;  // Key to throw objects (Right Mouse Button)

    private void Start()
    {
        if (throwChargeBar != null)
        {
            throwChargeBar.gameObject.SetActive(false);

            // Set the slider's max value to the max throw force
            throwChargeBar.maxValue = maxThrowForce;
            throwChargeBar.value = 0; // Start at 0
        }
    }
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
            currentThrowForce = 0f;

            // Show the charge bar
            if (throwChargeBar != null)
            {
                throwChargeBar.gameObject.SetActive(true);
                throwChargeBar.value = 0f; // Reset bar value
            }
        }

        // Increment the throw force while holding the key
        if (isChargingThrow && Input.GetKey(throwKey))
        {
            currentThrowForce += throwChargeRate * Time.deltaTime;
            currentThrowForce = Mathf.Clamp(currentThrowForce, 0f, maxThrowForce);

            // Update the charge bar value
            if (throwChargeBar != null)
            {
                throwChargeBar.value = currentThrowForce;
            }
        }

        // Release the throw
        if (Input.GetKeyUp(throwKey) && heldObject != null)
        {
            ThrowObject(currentThrowForce);
            isChargingThrow = false;

            // Hide the charge bar
            if (throwChargeBar != null)
                throwChargeBar.gameObject.SetActive(false);

            Debug.Log($"Threw object with force: {currentThrowForce:F1}");
        }
    }

    private void TryPickupObject()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer))
        {
            if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                heldObject = rb;
                heldObject.useGravity = false;
                heldObject.isKinematic = true;
                heldObject.collisionDetectionMode = CollisionDetectionMode.Continuous;

                Debug.Log($"Picked up: {heldObject.name}");
            }
        }
    }

    private void DropObject()
    {
        if (heldObject == null) return;

        heldObject.useGravity = true;
        heldObject.isKinematic = false;
        heldObject.collisionDetectionMode = CollisionDetectionMode.Discrete;
        heldObject.transform.parent = null;
        heldObject = null;
    }

    private void ThrowObject(float throwForce)
    {
        if (heldObject == null) return;

        heldObject.useGravity = true;
        heldObject.isKinematic = false;
        heldObject.collisionDetectionMode = CollisionDetectionMode.Discrete;

        heldObject.transform.parent = null;
        heldObject.AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse);
        heldObject = null;
    }

    private void MoveHeldObject()
    {
        Vector3 targetPosition = holdPoint.position;
        heldObject.MovePosition(targetPosition);
        heldObject.MoveRotation(holdPoint.rotation);
    }

    private void DebugRaycastCheck()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.red);
        }
    }
}
