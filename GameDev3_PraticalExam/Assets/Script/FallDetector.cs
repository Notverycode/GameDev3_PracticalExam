using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Stage1Manager manager1 =
            FindFirstObjectByType<Stage1Manager>();

        Stage2Manager manager2 =
            FindFirstObjectByType<Stage2Manager>();

        if (manager1 != null)
        {
            manager1.LoseGame();
        }
        else if (manager2 != null)
        {
            manager2.LoseGame();
        }
    }
}