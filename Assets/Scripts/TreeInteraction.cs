using UnityEngine;

public class TreeInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Camera playerCamera;

    void Awake()
    {
        if (playerCamera == null)
        {
            playerCamera = GetComponent<Camera>();
        }
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, interactionDistance, interactableLayer))
        {
            Tree tree = hitInfo.collider.GetComponent<Tree>();

            if (tree != null)
            {
               
                Debug.Log("Looking at tree: " + tree.name);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Vector3 hitDirection = (hitInfo.point - playerCamera.transform.position).normalized;
                    tree.TakeDamage(hitDirection);
                }
            }
        }
        else
        {
           
        }
    }

    void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * interactionDistance);
        }
    }
}
