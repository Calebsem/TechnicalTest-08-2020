using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestController : MonoBehaviour, IApplicationStateController
{
    [Header("Scene")]
    public CanvasGroup canvasGroup;
    public GameObject mainMenu;
    public Text pointsLabel;
    public new Camera camera;

    [Header("Level")]
    public int points;

    [HideInInspector]
    public EntityManager entityManager;
    public bool Done { get; private set; }

    private readonly List<GameObject> activatedEntities = new List<GameObject>();

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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)
                || EventSystem.current.currentSelectedGameObject != null) return;

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit, EntityManager.MaxRaycastDistance, LayerMask.GetMask("Entity")))
                {
                    hit.collider.SendMessage("Apply", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
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
        activatedEntities.Clear();
        points = 0;
    }
}
