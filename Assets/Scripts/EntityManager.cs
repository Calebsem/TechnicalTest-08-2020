using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [Header("Application")]
    public ApplicationState State = ApplicationState.Wizard;
    public List<EntityDefinition> entities;

    [Header("Scene")]
    public WizardController wizardController;
    public EntityPlacementController entityPlacementController;

    [Header("Resources")]
    public List<Mesh> availableMeshes;
    public GameObject entityPrefab;
    public GameObject buttonPrefab;

    public Type[] availableBehaviours { get; private set; } = new Type[]{
        typeof(Explode),
        typeof(GivePoints)
    };

    private IApplicationStateController currentController;
    private Dictionary<ApplicationState, IApplicationStateController> controllers;

    private void Awake()
    {
        entities = new List<EntityDefinition>();
        controllers = new Dictionary<ApplicationState, IApplicationStateController>();
        controllers.Add(ApplicationState.Wizard, wizardController);
        controllers.Add(ApplicationState.Placing, entityPlacementController);
    }

    private void Update()
    {
        if (currentController != null && currentController.Done)
        {
            SwitchState(ApplicationState.Idle);
        }
    }

    public void SwitchState(string name)
    {
        SwitchState((ApplicationState)Enum.Parse(typeof(ApplicationState), name));
    }

    private void SwitchState(ApplicationState state)
    {
        currentController?.End();
        if (controllers.ContainsKey(state))
        {
            State = state;
            currentController = controllers[State];
            currentController.Begin(this);
        }
        else
        {
            currentController = null;
            Debug.LogWarning($"No controller for state {state}");
        }
    }
}
