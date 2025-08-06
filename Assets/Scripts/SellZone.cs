using UnityEngine;

public class SellZone : MonoBehaviour
{
   
    public static event System.Action<int> OnLogSold;

    private void OnTriggerEnter(Collider other)
    {
        Log log = other.GetComponent<Log>();
        if (log != null)
        {
            Debug.Log("Log entered sell zone. Value: " + log.value);

            
            OnLogSold?.Invoke(log.value);

            Destroy(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f); 
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
