using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[System.Serializable]
public class IoTData
{
    public string jx;
    public string jy;
    public string BT1;
    public string BT2;
    public string BT3;
    public string BT4;
    public string BT5;
}

public class CustomBluetoothManager : MonoBehaviour
{
    public Text deviceAdd;
    public Text dataToSend;
    public Text receivedData;
    public GameObject devicesListContainer;
    public GameObject deviceMACText;
    private bool isConnected;

    private static AndroidJavaClass unity3dbluetoothplugin;
    private static AndroidJavaObject BluetoothConnector;
    public GameObject sidePanel;
    public GameObject ntPanel;
    public InstantPlaceManager placeManager;  // Reference to the InstantPlaceManager
    public GameObject[] prefabs;  // Array of prefabs to spawn
    
    

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>(); // Store spawned objects by name
    private GameObject currentPlacedObject; // Reference to the currently placed object
    private string currentPrefabName; // Track the current prefab name

    //make dictionary to store the current prefab's original state
    private Dictionary<string, Vector3> originalPositions = new Dictionary<string, Vector3>(); // Store original positions
    private Dictionary<string, Quaternion> originalRotations = new Dictionary<string, Quaternion>(); // Store original rotations
    private Dictionary<string, Vector3> originalScales = new Dictionary<string, Vector3>(); // Store original scales
    
    void Start()
    {
        InitBluetooth();
        isConnected = false;
        StartCoroutine(ProcessDataStream());
    }

    public void InitBluetooth()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)
            || !Permission.HasUserAuthorizedPermission(Permission.FineLocation)
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADMIN")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADVERTISE")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
        {
            Permission.RequestUserPermissions(new string[] {
                Permission.CoarseLocation,
                Permission.FineLocation,
                "android.permission.BLUETOOTH_ADMIN",
                "android.permission.BLUETOOTH",
                "android.permission.BLUETOOTH_SCAN",
                "android.permission.BLUETOOTH_ADVERTISE",
                "android.permission.BLUETOOTH_CONNECT"
            });
        }

        unity3dbluetoothplugin = new AndroidJavaClass("com.example.unity3dbluetoothplugin.BluetoothConnector");
        BluetoothConnector = unity3dbluetoothplugin.CallStatic<AndroidJavaObject>("getInstance");
    }

    public void StartScanDevices()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        foreach (Transform child in devicesListContainer.transform)
        {
            Destroy(child.gameObject);
        }

        BluetoothConnector.CallStatic("StartScanDevices");
    }

    public void StopScanDevices()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        BluetoothConnector.CallStatic("StopScanDevices");
    }

    public void ScanStatus(string status)
    {
        Toast("Scan Status: " + status);
    }

    public void NewDeviceFound(string data)
    {
        GameObject newDevice = Instantiate(deviceMACText);
        newDevice.GetComponent<Text>().text = data;
        newDevice.transform.SetParent(devicesListContainer.transform, false);
    }

    public void GetPairedDevices()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        string[] data = BluetoothConnector.CallStatic<string[]>("GetPairedDevices");

        foreach (Transform child in devicesListContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var d in data)
        {
            GameObject newDevice = Instantiate(deviceMACText);
            newDevice.GetComponent<Text>().text = d;
            newDevice.transform.SetParent(devicesListContainer.transform, false);
        }
    }

    public void StartConnection()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        string macAddress = PlayerPrefs.GetString("QRCode", string.Empty);
        if (!string.IsNullOrEmpty(macAddress))
        {
            BluetoothConnector.CallStatic("StartConnection", macAddress.ToUpper());
        }
        else
        {
            Debug.LogError("MAC Address is empty. Please scan the QR code first.");
        }        
    }

    public void StopConnection()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        if (isConnected)
            BluetoothConnector.CallStatic("StopConnection");
    }

    public void ConnectionStatus(string status)
    {
        Toast("Connection Status: " + status);
        isConnected = status == "connected";
    }

    public void ReadData(string data)
    {
        Debug.Log("BT Stream: " + data);

        try
        {
            IoTData iotData = JsonUtility.FromJson<IoTData>(data);
            HandleIoTData(iotData);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to parse JSON: " + e.Message);
        }
    }

    private void HandleIoTData(IoTData data)
    {
        ControlOrbitMovement(data.jx, data.jy);

        if (data.BT1 == "0")
        {
            PerformActionForBT1();
            UiButtonHandler();
        }

        if (data.BT2 == "1")
        {
            PerformActionForBT2();
        }

        if (data.BT3 == "1")
        {
            PerformActionForBT3();
        }

        if (data.BT4 == "1")
        {
            PerformActionForBT4();
        }

        if (data.BT5 == "1")
        {
            PerformActionForBT5();
        }
    }

    private void ControlOrbitMovement(string jx, string jy)
    {
        if (float.TryParse(jx, out float jxValue) && float.TryParse(jy, out float jyValue))
        {
            if (currentPlacedObject != null)
            {
                List<float> arr = new List<float> { 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130 };
                if (!arr.Contains(jxValue) && !arr.Contains(jyValue))
                {
                    float rotationSpeed = 1.0f;
                    float rotationX = (jxValue - 127.0f) * rotationSpeed * Time.deltaTime;
                    currentPlacedObject.transform.Rotate(0, rotationX, 0, Space.World);
                }
            }
        }
        else
        {
            Debug.LogError("Failed to parse jx or jy values to float.");
        }
    }

    public void ControlZoom(int direction)
    {
        if (currentPlacedObject != null)
        {
            float zoomSpeed = 0.8f;
            float zoomAmount = direction * zoomSpeed * Time.deltaTime;
            Vector3 newScale = currentPlacedObject.transform.localScale + new Vector3(zoomAmount, zoomAmount, zoomAmount);

            if (newScale.x > 0 && newScale.y > 0 && newScale.z > 0)
            {
                currentPlacedObject.transform.localScale = newScale;
            }
        }
    }

    // Hardware controller Functions
    public void PerformActionForBT1()
    {
        // Example action for BT1
        SpawnPrefabByIndex(0);  // Assuming BT1 spawns the first prefab
    }

    public void PerformActionForBT2()
    {
        placeManager.Previous_button();
        currentPlacedObject = placeManager.CurrentPlacedObject;
    }

    public void PerformActionForBT3()
    {
        placeManager.Next_button();
        currentPlacedObject = placeManager.CurrentPlacedObject;
    }

    public void PerformActionForBT4()
    {
        ControlZoom(1);
    }

    public void PerformActionForBT5()
    {
        ControlZoom(-1);
    }

    public void UiButtonHandler()
    {
        sidePanel.SetActive(true);
        ntPanel.SetActive(false);
    }

    // Additional Virtual controller functions for multiple prefab handling 
    public void SpawnPrefabByIndex(int prefabIndex)
    {
        if (prefabIndex >= 0 && prefabIndex < prefabs.Length)
        {
            // Place the prefab by index
            placeManager.ClickToPlace(prefabs[prefabIndex]);
            currentPlacedObject = placeManager.CurrentPlacedObject;
            currentPrefabName = prefabs[prefabIndex].name;
            spawnedObjects[currentPrefabName] = currentPlacedObject;

            // Store original transformations
            if (currentPlacedObject != null)
            {
                originalPositions[currentPrefabName] = currentPlacedObject.transform.position;
                originalRotations[currentPrefabName] = currentPlacedObject.transform.rotation;
                originalScales[currentPrefabName] = currentPlacedObject.transform.localScale;
            }
        }
        else
        {
            Debug.LogError("Invalid prefab index: " + prefabIndex);
        }
    }

    //reset the current prefab
    public void ResetCurrentPrefab()
    {
        if (currentPlacedObject != null && originalPositions.ContainsKey(currentPrefabName))
        {
            // Reset position, rotation, and scale
            currentPlacedObject.transform.position = originalPositions[currentPrefabName];
            currentPlacedObject.transform.rotation = originalRotations[currentPrefabName];
            currentPlacedObject.transform.localScale = originalScales[currentPrefabName];
        }
    }

    
    private IEnumerator ProcessDataStream()
    {
        while (true)
        {
            if (isConnected)
            {
                string data = BluetoothConnector.Call<string>("ReadDataFromStream");
                if (!string.IsNullOrEmpty(data))
                {
                    ReadData(data);
                }
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    public void WriteData()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        if (isConnected)
            BluetoothConnector.CallStatic("WriteData", dataToSend.text.ToString());
    }

    public void ReadLog(string data)
    {
        Debug.Log(data);
    }

    public void Toast(string data)
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        BluetoothConnector.CallStatic("Toast", data);
    }
}
