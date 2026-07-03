using UnityEngine;
using UnityEngine.UI;

public class SideMenuController : MonoBehaviour
{
    public GameObject sideMenuPanel; // Assign your side menu panel here
    public Button hamburgerButton; // Assign your hamburger button here
    public Button backButton; // Assign your back button here

    private bool isMenuOpen = false;

    void Start()
    {
        // Set the menu to be initially hidden
        sideMenuPanel.SetActive(false);

        // Add listeners to buttons
        hamburgerButton.onClick.AddListener(ToggleMenu);
        backButton.onClick.AddListener(BackToHome);
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        sideMenuPanel.SetActive(isMenuOpen);
    }

    void BackToHome()
    {
        // Add logic to return to home page here
        // This could be loading a new scene, enabling a home UI, etc.
        Debug.Log("Returning to Home Page");

        // Example: if you're using SceneManager to load scenes
        // SceneManager.LoadScene("HomeScene");
    }
}
