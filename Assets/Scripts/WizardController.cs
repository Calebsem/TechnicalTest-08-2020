using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    public EntityManager entityManager;
    public CanvasGroup canvasGroup;

    private void Update()
    {
        if (entityManager.State == ApplicationState.Wizard && canvasGroup.alpha == 0)
        {
            canvasGroup.alpha = 1;
        }
        else if (entityManager.State != ApplicationState.Wizard && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha = 0;
        }
    }
}
