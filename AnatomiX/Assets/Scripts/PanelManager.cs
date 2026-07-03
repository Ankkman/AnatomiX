using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels; // Array to store your panels
    public Button[] buttons; // Array to store your buttons

    void Start()
    {
        // Ensure only the first panel is active at the start
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }

        // Add listeners to buttons
        buttons[0].onClick.AddListener(() => ActivatePanel(0));
        buttons[1].onClick.AddListener(() => ActivatePanel(1));
        buttons[2].onClick.AddListener(() => ActivatePanel(2));
        buttons[3].onClick.AddListener(() => ActivatePanel(3));
    }

    void ActivatePanel(int index)
    {
        
        // Deactivate all panels
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
            
        }

        // Activate the selected panel
        if (index >= 0 && index < panels.Length)
        {
            panels[index].SetActive(true);
            
        }
    }
}
