using UnityEngine;
using UnityEngine.UI;

public class WizardSelectMeshStep : IWizardStep
{
    public bool Done { get; private set; }

    private WizardController wizardController;
    private Button selectedButton;

    public void Begin(WizardController controller)
    {
        wizardController = controller;

        wizardController.Cleanup();

        wizardController.stepNameLabel.text = "Step 1 : Choose mesh";
        wizardController.stepInstructionsLabel.text = "Pick a mesh in the list bellow. It will be the representation of your entity in your scene.";

        wizardController.nextButton.onClick.AddListener(ClickNext);
        wizardController.nextButton.interactable = false;
        wizardController.nextButton.GetComponentInChildren<Text>().text = "Continue Wizard";

        foreach (Mesh mesh in wizardController.entityManager.availableMeshes)
        {
            GameObject meshBtnObject = GameObject.Instantiate(wizardController.buttonPrefab, wizardController.buttonList);
            meshBtnObject.GetComponentInChildren<Text>().text = mesh.name;
            Button button = meshBtnObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                if (selectedButton != null)
                {
                    selectedButton.image.color = Color.white;
                }
                selectedButton = button;
                selectedButton.image.color = Color.green;
                wizardController.definition.mesh = mesh;
                wizardController.nextButton.interactable = true;
            });
        }
    }

    private void ClickNext()
    {
        Done = true;
    }

    public void End()
    {
        wizardController.Cleanup();
    }
}
