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
        buildManager.IsBuilding = true;
        SetPrefab(Random.Range(0,objects.Length - 1));
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
