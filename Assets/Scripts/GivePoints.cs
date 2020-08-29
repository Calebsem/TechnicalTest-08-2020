using UnityEngine;

public class GivePoints : MonoBehaviour
{
    private bool gavePoints = false;
    private void Apply(object payload)
    {
        if (gavePoints) return;
        TestController controller = payload as TestController;
        if (controller != null)
        {
            gavePoints = true;
            controller.points += 10;
        }
    }
    private void Reset()
    {
        gavePoints = false;
    }
}
