using UnityEngine;

public class FallenTree : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int logsToProduce = 5;
    [SerializeField] private GameObject logSectionPrefab;

    public void BuckLog(Vector3 positionToSpawn)
    {
        if (logsToProduce > 0)
        {
            if (logSectionPrefab != null)
            {
                Debug.Log("Producing one log section.");
                Instantiate(logSectionPrefab, positionToSpawn, Quaternion.identity);
                logsToProduce--;
            }
            else
            {
                Debug.LogError("Log Section Prefab is not assigned!");
            }
        }
        else
        {
            Debug.Log("No more logs to produce from this trunk.");
            Destroy(gameObject, 1f);
        }
    }
}
