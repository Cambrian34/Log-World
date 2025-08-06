using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Camera playerCamera;

    [Header("Dependencies")]
    [SerializeField] private EquipmentManager equipmentManager;

    [Header("Carry System")]
    [SerializeField] private Transform carryPosition;
    private GameObject heldLog = null; 
    void Update()
    {
        if (heldLog != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DropLog();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformRaycastInteraction();
        }
    }

    private void DropLog()
    {
        Debug.Log("Dropping log.");
        heldLog.transform.SetParent(null);

        Collider logCollider = heldLog.GetComponent<Collider>();
        if (logCollider != null)
        {
            logCollider.enabled = true;
        }

        Rigidbody rb = heldLog.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; 
        }

        heldLog = null;
    }

    private void PerformRaycastInteraction()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, interactionDistance, interactableLayer))
        {
            Log logToPickup = hitInfo.collider.GetComponent<Log>();
            if (logToPickup != null)
            {
                Debug.Log("Picking up log.");
                heldLog = logToPickup.gameObject;

                Collider logCollider = heldLog.GetComponent<Collider>();
                if (logCollider != null)
                {
                    logCollider.enabled = false;
                }

                Rigidbody rb = heldLog.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true; 
                }
                heldLog.transform.SetParent(carryPosition);
                heldLog.transform.localPosition = Vector3.zero;
                heldLog.transform.localRotation = Quaternion.identity;
                return;
            }

            int equippedTool = equipmentManager.GetCurrentToolIndex();
            if (equippedTool == 0) 
            {
                Tree standingTree = hitInfo.collider.GetComponent<Tree>();
                if (standingTree != null)
                {
                    Vector3 hitDirection = (hitInfo.point - playerCamera.transform.position).normalized;
                    standingTree.TakeDamage(hitDirection);
                    return;
                }

                FallenTree fallenTree = hitInfo.collider.GetComponent<FallenTree>();
                if (fallenTree != null)
                {
                    fallenTree.BuckLog(hitInfo.point);
                }
            }
        }
    }
}
