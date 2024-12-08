using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    public string pickupTag = "PickUp";   // Tag for pickable objects
    public string playerTag = "Player";   // Tag for the player

    [Header("Target Object")]
    public GameObject objectToDeactivate; 

    [Header("Scene Settings")]
    public string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the correct tag
        if (other.CompareTag(pickupTag))
        {
            Debug.Log("Trigger Activated by: " + other.name);

            // Deactivate the target object
            SetTargetObjectActive(false);
        }

        // Check if the player enters the trigger
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player entered the trigger area. Loading next scene...");
            LoadNextScene();

            Debug.Log("Player detected!");
        }
        Debug.Log("Something entered the trigger: " + other.name);
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
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(isActive);
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set in the Inspector.");
        }
    }
}