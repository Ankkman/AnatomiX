using UnityEngine;
using UnityEngine.SceneManagement;

public class ExperimentButtonHandler : MonoBehaviour
{
    // Variable to store the experiment ID to load
    public static int experimentID = 0;

    // Method called by button to load instruction scene
    public void OnExperimentButtonClicked(int id)
    {
        experimentID = id; // Store the clicked experiment ID
        SceneManager.LoadScene("Instruction"); // Load the Instruction Scene
    }
}
