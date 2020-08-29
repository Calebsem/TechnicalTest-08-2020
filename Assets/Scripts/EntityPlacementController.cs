﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EntityPlacementController : MonoBehaviour, IApplicationStateController
{
    [Header("Scene")]
    public CanvasGroup canvasGroup;
    public Transform buttonList;

    public bool Done { get; private set; }

    private EntityManager entityManager;
    private Button selectedButton;
    private EntityDefinition selectedEntity;

    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        Input.simulateMouseWithTouches = true;
        selectedEntity = null;
    }

    public void GoBack()
    {
        Done = true;
    }

    public void Begin(EntityManager manager)
    {
        Done = false;
        Cleanup();
        entityManager = manager;
        selectedEntity = null;

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

    private void Update()
    {
        if (entityManager == null || entityManager.State != ApplicationState.Placing) return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)
                || EventSystem.current.currentSelectedGameObject != null
                || selectedEntity == null) return;

            if (touch.phase == TouchPhase.Ended)
            {
                RaycastHit hit;
                Ray ray = entityManager.camera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit, EntityManager.MaxRaycastDistance, LayerMask.GetMask("Scene")))
                {
                    GameObject instance = new GameObject($"{selectedEntity.name}-instance", selectedEntity.behaviours);

                    instance.layer = LayerMask.NameToLayer("Entity");

                    instance.transform.SetParent(hit.collider.transform);
                    instance.transform.position = hit.point + Vector3.up / 2f;

                    instance.GetComponent<MeshFilter>().mesh = selectedEntity.mesh;
                    instance.GetComponent<MeshRenderer>().material = entityManager.defaultEntityMaterial;

                    entityManager.instances.Add(instance);
                }
            }
        }
    }

    public void End()
    {
        Cleanup();
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
