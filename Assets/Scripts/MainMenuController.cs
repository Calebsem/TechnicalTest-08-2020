using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour, IApplicationStateController
{
    [Header("Scene")]
    public CanvasGroup canvasGroup;
    public Button placeEntitiesButton;
    public Button testButton;

    public bool Done => false;

    private EntityManager entityManager;

    public void Begin(EntityManager manager)
    {
        entityManager = manager;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    private void Update()
    {
        if (entityManager.entities.Count > 0)
        {
            placeEntitiesButton.interactable = true;
        }
        else
        {
            placeEntitiesButton.interactable = false;
        }
        if (entityManager.instances.Count > 0)
        {
            testButton.interactable = true;
        }
        else
        {
            testButton.interactable = false;
        }
    }

    public void End()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
