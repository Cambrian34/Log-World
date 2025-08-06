using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("Equipment Settings")]
    [SerializeField] private GameObject[] toolPrefabs; 

    private GameObject[] toolInstances;
    private int currentToolIndex = -1; 

    void Start()
    {
        
        toolInstances = new GameObject[toolPrefabs.Length];
        for (int i = 0; i < toolPrefabs.Length; i++)
        {
            toolInstances[i] = Instantiate(toolPrefabs[i], transform);
            toolInstances[i].SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipTool(0);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            EquipTool(-1);
        }
    }

    void EquipTool(int toolIndex)
    {
        if (toolIndex == currentToolIndex)
        {
            toolIndex = -1;
        }

        if (currentToolIndex != -1)
        {
            toolInstances[currentToolIndex].SetActive(false);
        }

        currentToolIndex = toolIndex;
        if (currentToolIndex != -1)
        {
            toolInstances[currentToolIndex].SetActive(true);
        }
    }


    public int GetCurrentToolIndex()
    {
        return currentToolIndex;
    }
}
