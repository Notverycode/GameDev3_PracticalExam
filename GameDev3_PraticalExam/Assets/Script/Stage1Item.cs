

using UnityEngine;

public class Stage1Item : MonoBehaviour
{
    public ItemType itemType;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Stage1Manager manager =
            FindFirstObjectByType<Stage1Manager>();

        if (manager == null)
            return;

        switch (itemType)
        {
            case ItemType.AddTime:
                manager.AddTime(3);
                break;

            case ItemType.RemoveTime:
                manager.AddTime(-5);
                break;

            case ItemType.AddScore:
                manager.AddScore(500);
                break;

            case ItemType.RemoveScore:
                manager.AddScore(-500);
                break;
        }

        Destroy(gameObject);
    }
}


