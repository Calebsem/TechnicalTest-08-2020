using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardController : MonoBehaviour, IApplicationStateController
{
    [Header("Scene")]
    public CanvasGroup canvasGroup;
    public Text stepNameLabel;
    public Text stepInstructionsLabel;
    public Transform stepContentTransform;
    public Button nextButton;
    public Transform buttonList;
    public InputField nameField;
    public GameObject mainMenu;

    [Header("Debug")]
    public EntityDefinition definition;

    private IWizardStep currentStep;
    private Queue<IWizardStep> steps;

    [HideInInspector]
    public EntityManager entityManager;
    public bool Done { get; private set; }

    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void Begin(EntityManager manager)
    {
        Done = false;
        entityManager = manager;
        definition = new EntityDefinition();
        steps = new Queue<IWizardStep>();
        steps.Enqueue(new WizardSelectMeshStep());
        steps.Enqueue(new WizardSelectBehavioursStep());
        steps.Enqueue(new WizardGiveNameStep());
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        mainMenu.SetActive(false);
        ContinueWizard();
    }

    private void Update()
    {
        if (steps != null && currentStep != null && currentStep.Done)
        {
            ContinueWizard();
        }
    }

    private void ContinueWizard()
    {
        currentStep?.End();
        if (steps.Count > 0)
        {
            currentStep = steps.Dequeue();
            currentStep.Begin(this);
        }
        else
        {
            Done = true;
        }
    }

    public void End()
    {
        entityManager.entities.Add(definition);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        mainMenu.SetActive(true);
    }

    public void Cleanup()
    {
        foreach (Transform child in buttonList)
        {
            Destroy(child.gameObject);
        }
        nameField.text = "";
        nextButton.onClick.RemoveAllListeners();
    }
}
