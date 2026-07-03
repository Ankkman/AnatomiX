using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HardwareControllerChecker : MonoBehaviour
{
    public GameObject qr_Panel; // Reference to the popup panel UI
    // public GameObject hc_notipanel;
    // public Button hardwareControllerButton;
    public InstructionSceneManager ism;
    public CustomBluetoothManager csm;
   
    

    // Start is called before the first frame update
    void Start()
    {
        qr_Panel.SetActive(false); // Hide the popup initially
        // hardwareControllerButton.onClick.AddListener(CheckMacAddress);

    }

    // Update is called once per frame
    public void CheckMacAddress()
    {
        string macAddress = PlayerPrefs.GetString("QRCode", string.Empty);

        if (string.IsNullOrEmpty(macAddress))
        {
            // MAC address not found, show popup to add controller
            qr_Panel.SetActive(true);
        }
        else
        {
            // MAC address found, proceed with hardware controller

            Debug.Log("MAC Address found: " + macAddress);
            // Add your logic to proceed with the hardware controller
           
            ism.LoadExperimentScene();
            csm.StartConnection();
            
           
        }
    }
  
}
