using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour, IApplicationStateController
{
    [Header("Scene")]
    public CanvasGroup canvasGroup;
    public GameObject mainMenu;
    public Text pointsLabel;

    [Header("Level")]
    public int points;

    [HideInInspector]
    public EntityManager entityManager;
    public bool Done { get; private set; }

    private readonly List<EntityController> activatedControllers = new List<EntityController>();

    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        Input.simulateMouseWithTouches = true;
    }

    public void GoBack()
    {
        Done = true;
    }

    public void Begin(EntityManager manager)
    {
        Cleanup();
        mainMenu.SetActive(false);
        entityManager = manager;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    private void Update()
    {
        if (entityManager == null || entityManager.State != ApplicationState.Testing) return;
        pointsLabel.text = $"{points} points";
    }

    public void End()
    {
        Cleanup();
        mainMenu.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    private void Cleanup()
    {
        activatedControllers.Clear();
        points = 0;
    }
}
