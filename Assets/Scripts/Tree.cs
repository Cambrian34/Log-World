using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Tree Settings")]
    [SerializeField] private int health = 3; 
    [SerializeField] private GameObject fallingTreePrefab;

    public void TakeDamage(Vector3 hitDirection)
    {
        health--;

        Debug.Log("Tree health: " + health);
        

        if (health <= 0)
        {
            Fall(hitDirection);
        }
    }

    private void Fall(Vector3 fallDirection)
    {
        if (fallingTreePrefab != null)
        {
            GameObject fallingTree = Instantiate(fallingTreePrefab, transform.position, transform.rotation);

            Rigidbody rb = fallingTree.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
                Vector3 force = (fallDirection + Vector3.up * 0.2f) * 10f;
                rb.AddForce(force, ForceMode.Impulse);
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Falling Tree Prefab not assigned on " + name);
            Destroy(gameObject);
        }
    }
}
