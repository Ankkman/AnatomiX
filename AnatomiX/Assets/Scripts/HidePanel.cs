using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanel : MonoBehaviour
{
   public void ToggleObject(GameObject gameobjecttoToggle)
    {
        gameobjecttoToggle.SetActive(!gameobjecttoToggle.activeSelf);

    }
}
