using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour, IApplicationStateController
{
    [Header("Scene")]
    public CanvasGroup canvasGroup;
    public bool Done => false;

    public void Begin(EntityManager manager)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void End()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
