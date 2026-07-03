using UnityEngine;
using UnityEngine.UI;

public class SubjectButtonController : MonoBehaviour
{
    public Button physicsButton;
    public Button chemistryButton;
    public Button biologyButton;
    public Button geographyButton;

    public Sprite darkPhysicsSprite;
    public Sprite lightPhysicsSprite;
    public Sprite darkChemistrySprite;
    public Sprite lightChemistrySprite;
    public Sprite darkBiologySprite;
    public Sprite lightBiologySprite;
    public Sprite darkGeographySprite;
    public Sprite lightGeographySprite;

    void Start()
    {
        // Initially set Physics as the dark button
        SetButtonState(physicsButton, darkPhysicsSprite);
        SetButtonState(chemistryButton, lightChemistrySprite);
        SetButtonState(biologyButton, lightBiologySprite);
        SetButtonState(geographyButton, lightGeographySprite);

        // Add onClick listeners
        physicsButton.onClick.AddListener(() => OnSubjectButtonClicked(physicsButton));
        chemistryButton.onClick.AddListener(() => OnSubjectButtonClicked(chemistryButton));
        biologyButton.onClick.AddListener(() => OnSubjectButtonClicked(biologyButton));
        geographyButton.onClick.AddListener(() => OnSubjectButtonClicked(geographyButton));
    }

    void OnSubjectButtonClicked(Button clickedButton)
    {
        // Reset all buttons to light state
        SetButtonState(physicsButton, lightPhysicsSprite);
        SetButtonState(chemistryButton, lightChemistrySprite);
        SetButtonState(biologyButton, lightBiologySprite);
        SetButtonState(geographyButton, lightGeographySprite);

        // Set the clicked button to dark state
        if (clickedButton == physicsButton)
            SetButtonState(physicsButton, darkPhysicsSprite);
        else if (clickedButton == chemistryButton)
            SetButtonState(chemistryButton, darkChemistrySprite);
        else if (clickedButton == biologyButton)
            SetButtonState(biologyButton, darkBiologySprite);
        else if (clickedButton == geographyButton)
            SetButtonState(geographyButton, darkGeographySprite);
    }

    void SetButtonState(Button button, Sprite sprite)
    {
        button.image.sprite = sprite;
    }
}