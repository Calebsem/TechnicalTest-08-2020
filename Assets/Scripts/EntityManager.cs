using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [Header("Application")]
    public ApplicationState State = ApplicationState.Wizard;
    public List<EntityDefinition> entities;

    [Header("Resources")]
    public List<Mesh> availableMeshes;
    public GameObject entityPrefab;

    private readonly Type[] availableBehaviours = new Type[]{
        typeof(Explode),
        typeof(GivePoints)
    };

    private void Awake()
    {
        entities = new List<EntityDefinition>();
    }
}
