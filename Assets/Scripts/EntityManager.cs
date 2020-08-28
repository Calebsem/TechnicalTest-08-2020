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

    [Header("Resources")]
    public List<Mesh> availableMeshes;
    public GameObject entityPrefab;

    private readonly Type[] availableBehaviours = new Type[]{
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

        SwitchState(ApplicationState.Wizard);
    }

    private void Update()
    {
        if (currentController != null && currentController.Done)
        {
            SwitchState(State);
        }
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
            throw new Exception("Unhandled entity manager state");
        }
    }
}
