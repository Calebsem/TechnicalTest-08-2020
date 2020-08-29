using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardSelectBehavioursStep : IWizardStep
{
    public bool Done { get; private set; }

    private WizardController wizardController;
    private List<Type> selectedTypes;

    public void Begin(WizardController controller)
    {
        wizardController = controller;
        selectedTypes = new List<Type>();
        selectedTypes.Add(typeof(MeshFilter));
        selectedTypes.Add(typeof(MeshRenderer));
        selectedTypes.Add(typeof(BoxCollider));

        wizardController.Cleanup();

        wizardController.stepNameLabel.text = "Step 2 : Choose behaviours";
        wizardController.stepInstructionsLabel.text = "Pick behaviours in the list bellow. It will be the effect your entity has in your scene when you tap on it. You can choose to have one of them, both or none.";

        wizardController.nextButton.onClick.RemoveAllListeners();
        wizardController.nextButton.onClick.AddListener(ClickNext);
        wizardController.nextButton.interactable = true;
        wizardController.nextButton.GetComponentInChildren<Text>().text = "Continue Wizard";

        foreach (Type type in wizardController.entityManager.availableBehaviours)
        {
            GameObject meshBtnObject = GameObject.Instantiate(wizardController.entityManager.buttonPrefab, wizardController.buttonList);
            meshBtnObject.GetComponentInChildren<Text>().text = type.Name;
            Button button = meshBtnObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                if (selectedTypes.Contains(type))
                {
                    button.image.color = Color.white;
                    selectedTypes.Remove(type);
                }
                else
                {
                    button.image.color = Color.green;
                    selectedTypes.Add(type);
                }
            });
        }
    }

    private void ClickNext()
    {
        wizardController.definition.behaviours = selectedTypes.ToArray();
        Done = true;
    }

    public void End()
    {
        wizardController.Cleanup();
    }
}
