using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform playerRespawnPoint; // Position where the player will respawn
    private GameObject player;           // Reference to the player GameObject

    [Header("Pickable Object Settings")]
    private Dictionary<GameObject, Vector3> pickableObjectPositions = new Dictionary<GameObject, Vector3>();

    private void Start()
    {
        // Find the player by tag and save the reference
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("No GameObject with tag 'Player' found in the scene.");
        }

        // Store initial positions of all pickupable objects
        GameObject[] pickableObjects = GameObject.FindGameObjectsWithTag("PickUp");

        foreach (GameObject obj in pickableObjects)
        {
            pickableObjectPositions[obj] = obj.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the death zone
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the death zone. Respawning...");
            RespawnPlayer();
        }

        // Check if a pickupable object enters the death zone
        if (other.CompareTag("PickUp"))
        {
            Debug.Log("Pickupable object entered the death zone. Respawning...");
            RespawnPickableObject(other.gameObject);
        }
    }

    private void RespawnPlayer()
    {
        // Ensure player and respawn point exist
        if (player != null && playerRespawnPoint != null)
        {
            // Move the entire player GameObject (root)
            Transform playerRoot = player.transform.root;
            playerRoot.position = playerRespawnPoint.position;

            // Reset Rigidbody velocity for smooth teleportation
            Rigidbody playerRb = playerRoot.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
            }

            Debug.Log("Player respawned to: " + playerRespawnPoint.position);
        }
        else
        {
            Debug.LogWarning("Player or Respawn Point is not set.");
        }
    }

    private void RespawnPickableObject(GameObject obj)
    {
        if (pickableObjectPositions.ContainsKey(obj))
        {
            // Reset the object's position
            obj.transform.position = pickableObjectPositions[obj];

            // Reset the object's Rigidbody velocity
            Rigidbody rb = obj.GetComponentInParent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            Debug.Log("Pickupable object respawned to: " + pickableObjectPositions[obj]);
        }
    }
}
