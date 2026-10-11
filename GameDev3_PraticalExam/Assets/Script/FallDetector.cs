
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Stage1Manager manager =
                FindFirstObjectByType<Stage1Manager>();

            if (manager != null)
                manager.LoseGame();
        }
    }
}
