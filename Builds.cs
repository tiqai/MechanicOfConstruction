using UnityEngine;

public class Builds : MonoBehaviour
{
    public GameObject[] objects;
    private BuildManager buildManager;

    private void Start()
    {
        buildManager = GetComponent<BuildManager>();
    }

    public void StartBuilding()
    {
        // later
    }

    public void SetPrefab(int id)
    {
	    if (buildManager.CreatedBuild != null)
            {
                Destroy(buildManager.CreatedBuild);
            }

        buildManager.PrefabBuild = objects[id];
    }
}
