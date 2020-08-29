using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityPlacementController : MonoBehaviour, IApplicationStateController
{
    [Header("Scene")]
    public CanvasGroup canvasGroup;
    public Transform buttonList;
    public GameObject mainMenu;

    [Header("Debug")]
    public EntityDefinition selectedEntity;

    [HideInInspector]
    public EntityManager entityManager;
    public bool Done { get; private set; }

    private Button selectedButton;

    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public void GoBack()
    {
        Done = true;
    }

    public void Begin(EntityManager manager)
    {
        Done = false;
        Cleanup();
        mainMenu.SetActive(false);
        entityManager = manager;

        foreach (EntityDefinition entity in entityManager.entities)
        {
            GameObject meshBtnObject = GameObject.Instantiate(entityManager.buttonPrefab, buttonList);
            meshBtnObject.GetComponentInChildren<Text>().text = entity.name;
            Button button = meshBtnObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                if (selectedButton != null)
                {
                    selectedButton.image.color = Color.white;
                }
                selectedEntity = entity;
                selectedButton = button;
                selectedButton.image.color = Color.green;
            });
        }

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
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
        foreach (Transform child in buttonList)
        {
            Destroy(child.gameObject);
        }
    }
}
