using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public CustomBluetoothManager cbm;
    public int prefabIndex;

    public void spawnPrefab()
    {
        cbm.SpawnPrefabByIndex(prefabIndex);
    }

    public void ResetCurrentPrefab()
    {
        cbm.ResetCurrentPrefab();
    }
}
