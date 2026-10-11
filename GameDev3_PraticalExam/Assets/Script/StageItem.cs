
using UnityEngine;

public class StageItem : MonoBehaviour
{
    public ItemType itemType;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player"))
            return;

        Stage1Manager manager1 =
            FindFirstObjectByType<Stage1Manager>();

        Stage2Manager manager2 =
            FindFirstObjectByType<Stage2Manager>();

        if (manager1 != null)
        {
            ApplyItem(manager1.AddTime, manager1.AddScore);
        }
        else if (manager2 != null)
        {
            ApplyItem(manager2.AddTime, manager2.AddScore);
        }
        else
        {
            Debug.LogError("No Stage1Manager or Stage2Manager found!");
            return;
        }

        collected = true;
        Destroy(gameObject);
    }

    private void ApplyItem(
        System.Action<float> addTime,
        System.Action<int> addScore)
    {
        switch (itemType)
        {
            case ItemType.AddTime:
                addTime(3);
                break;

            case ItemType.RemoveTime:
                addTime(-5);
                break;

            case ItemType.AddScore:
                addScore(500);
                break;

            case ItemType.RemoveScore:
                addScore(-500);
                break;
        }
    }
}
