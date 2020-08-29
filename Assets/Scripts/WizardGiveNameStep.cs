using System;
using System.Linq;
using UnityEngine.UI;

public class WizardGiveNameStep : IWizardStep
{
    public bool Done { get; private set; }

    private WizardController wizardController;

    public void Begin(WizardController controller)
    {
        wizardController = controller;

        wizardController.Cleanup();

        wizardController.stepNameLabel.text = "Step 3 : Name entity";
        wizardController.stepInstructionsLabel.text = "Choose a name for your entity. It will help you identify what entity an instance is using when placed in the scene. If no name is given, a default one is generated.";

        wizardController.nextButton.onClick.AddListener(ClickNext);
        wizardController.nextButton.interactable = true;
        wizardController.nextButton.GetComponentInChildren<Text>().text = "Save Entity";

        wizardController.nameField.gameObject.SetActive(true);
    }

    private void ClickNext()
    {
        Done = true;
        if (string.IsNullOrWhiteSpace(wizardController.nameField.text))
        {
            wizardController.definition.name = $"{wizardController.definition.mesh.name} - {string.Join(" / ", wizardController.definition.behaviours.Skip(4).Select(b => b.Name))}";
        }
        else
        {
            wizardController.definition.name = wizardController.nameField.text;
        }
    }

    public void End()
    {
        wizardController.Cleanup();
        wizardController.nameField.gameObject.SetActive(false);
    }
}
