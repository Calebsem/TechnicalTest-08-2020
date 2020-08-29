using System.Collections;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private Coroutine explosionCoroutine;
    private bool exploded = false;
    private Vector3 originalPosition;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = Color.green;
    }

    private void Apply(object payload)
    {
        if (exploded) return;
        explosionCoroutine = StartCoroutine(ExplodeCoroutine());
    }

    IEnumerator ExplodeCoroutine()
    {
        exploded = true;
        originalPosition = transform.position;
        const float stepCount = 10f;
        for (int i = 0; i < stepCount; i++)
        {
            transform.localScale *= 1.1f;
            transform.position = originalPosition + Random.insideUnitSphere / 4f;
            meshRenderer.material.color = Color.Lerp(Color.green, Color.white, i / stepCount);
            yield return null;
        }
    }

    private void Reset()
    {
        StopCoroutine(explosionCoroutine);
        transform.localScale = Vector3.one;
        transform.position = originalPosition;
        meshRenderer.material.color = Color.green;
        exploded = false;
    }
}
