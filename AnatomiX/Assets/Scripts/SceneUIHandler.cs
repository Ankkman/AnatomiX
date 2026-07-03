using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUIHandler : MonoBehaviour
{
    public GameObject ntPanel;
    public void ToggleObject(GameObject gameobjecttoToggle)
    {
        gameobjecttoToggle.SetActive(!gameobjecttoToggle.activeSelf);

    }

    public void Notipanel_deactive()
    {
        ntPanel.SetActive(false);

    }

    public void Notipanel_active()
    {
        ntPanel.SetActive(true);

    }
    


    
}

