using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using Lean.Touch;
using TMPro;

public class InstantPlaceManager : MonoBehaviour
{
    private GameObject currentPlacedObject; // To keep track of the currently placed object
    private static int index = 1;
    private TextMeshPro currValText;   
    public AudioSource audioSource;
    public Camera arCamera; // Reference to the AR camera
    public VariableJoystick variableJoystick;

    // Start is called before the first frame update
    void Start()
    {
        

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                // Add an AudioSource component if not found
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    // Update is called once per frame
    public void ClickToPlace(GameObject objectToPlace)
    {
        if (arCamera == null)
        {
            Debug.LogError("AR Camera reference not set!");
            return;
        }

        if (currentPlacedObject != null)
        {
            Destroy(currentPlacedObject); // Destroy the previous object if it exists
        }

        // Play the audio
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }

        // Get the position and rotation of the camera
        Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 4.0f; // 2 meters in front of the camera
        Quaternion spawnRotation = Quaternion.LookRotation(arCamera.transform.forward); // Look towards the camera's forward direction
        Debug.Log(spawnPosition);
        Debug.Log(spawnRotation);
        // Instantiate the object at the calculated position and rotation
        currentPlacedObject = Instantiate(objectToPlace, spawnPosition, spawnRotation);    

        JoystickPlayerExample joystickPlayerExample = currentPlacedObject.GetComponent<JoystickPlayerExample>();
        if (joystickPlayerExample != null)
        {
            joystickPlayerExample.SetJoystick(variableJoystick);
        }
    }

    public void Reload_Scene()
    {
        if (currentPlacedObject != null)
        {
            Destroy(currentPlacedObject); // Destroy the previous object if it exists
        }
    }

    public GameObject CurrentPlacedObject
    {
        get { return currentPlacedObject; }
    }

public void Previous_button()
{
    // Ensure the currentPlacedObject is not null
    if (currentPlacedObject != null)
    {
        // Access the intermediate empty game object (first child of currentPlacedObject)
        Transform intermediateObject = currentPlacedObject.transform.GetChild(0);

        // Check if index is within the valid range
        if (index >= 1 && index <= intermediateObject.childCount)
        {
            Debug.Log("Current placed object is not null");
            Debug.Log(currentPlacedObject.transform.childCount);

            // Access the desired child of the intermediate object
            Transform childObject = intermediateObject.GetChild(index - 1);
            index -= 1;
            Debug.Log(childObject);

            // Check if the childObject is not null and deactivate it
            if (childObject != null)
            {
                childObject.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Could not find the object");
            }
        }
        else
        {
            Debug.Log("Closed all");
        }
    }
    else
    {
        Debug.Log("Current placed object is null");
    }
}

    public void Next_button()
{
    // Ensure the currentPlacedObject is not null
    if (currentPlacedObject != null)
    {
        // Access the intermediate empty game object (first child of currentPlacedObject)
        Transform intermediateObject = currentPlacedObject.transform.GetChild(0);

        // Check if index is within the bounds of the intermediate object's children count
        if (index < intermediateObject.childCount)
        {
            Debug.Log("Current placed object is not null");
            Debug.Log(currentPlacedObject.transform.childCount);

            // Access the desired child of the intermediate object
            Transform childObject = intermediateObject.GetChild(index);
            index += 1;
            Debug.Log(childObject);

            // Check if the childObject is not null and activate it
            if (childObject != null)
            {
                childObject.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Could not find the object");
            }
        }
        else
        {
            Debug.Log("Opened all");
        }
    }
    else
    {
        Debug.Log("Current placed object is null");
    }
}
}
