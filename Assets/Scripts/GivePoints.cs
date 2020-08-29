using UnityEngine;

public class GivePoints : MonoBehaviour
{
    private bool gavePoints = false;
    private Vector3 originalPosition;
    private bool running = false;


    private void Update()
    {
        if (!running || gavePoints) return;
        transform.position = originalPosition + Vector3.up * Mathf.Cos(Time.time) / 2f;
    }

    private void Init()
    {
        originalPosition = transform.position + Vector3.up / 2f;
        running = true;
    }

    private void Apply(object payload)
    {
        if (gavePoints) return;
        TestController controller = payload as TestController;
        if (controller != null)
        {
            gavePoints = true;
            controller.points += 10;
            transform.position = originalPosition;
        }
    }
    private void Reset()
    {
        gavePoints = false;
        running = false;
    }
}
