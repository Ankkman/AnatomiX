using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionSceneManager : MonoBehaviour
{
    // Method to load the correct experiment scene after the instructions
    public void LoadExperimentScene()
    {
        // Check which experiment ID was clicked
        switch (ExperimentButtonHandler.experimentID)
        {
            case 1:
                SceneManager.LoadScene("Joystick_electricRing");
                break;
            case 2:
                SceneManager.LoadScene("Isotopes");
                break;
            // case 3:
            //     SceneManager.LoadScene("ExperimentScene3");
            //     break;
            // case 4:
            //     SceneManager.LoadScene("ExperimentScene4");
            //     break;
            default:
                Debug.LogError("Invalid Experiment ID");
                break;
        }
    }
}
