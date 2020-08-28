using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardController : MonoBehaviour, IApplicationStateController
{
    public CanvasGroup canvasGroup;
    public Text stepNameLabel;
    public Text stepInstructionsLabel;
    public Transform stepContentTransform;
    public bool isDone;

    private IWizardStep currentStep;
    private Queue<IWizardStep> steps;
    private EntityManager entityManager;

    public bool Done => isDone;

    private void Awake()
    {
        canvasGroup.alpha = 0;
    }

    public void Begin(EntityManager manager)
    {
        entityManager = manager;
        isDone = false;
        steps = new Queue<IWizardStep>();
        canvasGroup.alpha = 1;
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
            isDone = true;
        }
    }

    public void End()
    {
        canvasGroup.alpha = 0;
    }
}
