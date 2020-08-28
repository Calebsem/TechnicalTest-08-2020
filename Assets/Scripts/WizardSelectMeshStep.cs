using UnityEngine;
using UnityEngine.UI;

public class WizardSelectMeshStep : IWizardStep
{
    public bool Done { get; private set; }

    private WizardController wizardController;

    public void Begin(WizardController controller)
    {
        wizardController = controller;

        wizardController.stepNameLabel.text = "Step 1 : Choose mesh";
        wizardController.stepInstructionsLabel.text = "Pick a mesh in the list bellow. It will be the representation of your entity in your scene.";

        wizardController.nextButton.onClick.RemoveAllListeners();
        wizardController.nextButton.onClick.AddListener(ClickNext);

        foreach (Transform child in wizardController.meshList)
        {
            GameObject.Destroy(child);
        }

        foreach (Mesh mesh in wizardController.entityManager.availableMeshes)
        {
            GameObject meshBtnObject = GameObject.Instantiate(wizardController.buttonPrefab, wizardController.meshList);
            meshBtnObject.GetComponentInChildren<Text>().text = mesh.name;
            meshBtnObject.GetComponent<Button>().onClick.AddListener(() =>
            {
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
        wizardController.nextButton.onClick.RemoveAllListeners();
    }
}
