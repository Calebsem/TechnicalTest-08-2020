using System.Collections;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private Coroutine explosionCoroutine;
    private bool exploded = false;

    private void Apply()
    {
        if (exploded) return;
        explosionCoroutine = StartCoroutine(ExplodeCoroutine());
    }

    IEnumerator ExplodeCoroutine()
    {
        exploded = true;
        for (int i = 0; i < 5; i++)
        {
            transform.localScale *= 1.1f;
            yield return null;
        }
    }

    private void Reset()
    {
        StopCoroutine(explosionCoroutine);
        transform.localScale = Vector3.one;
        exploded = false;
    }
}
