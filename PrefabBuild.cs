using System.Collections.Generic;
using UnityEngine;

public class PrefabBuild : MonoBehaviour
{
    [SerializeField] private Material canM, cantM;
    public List<Material> materials = new List<Material>();
    public Transform ray;

    private bool canPlace;
    private Collider[] colliders;
    private MeshFilter[] meshes;

    private void Start()
    {
        colliders = GetComponentsInChildren<Collider>();
        meshes = GetComponentsInChildren<MeshFilter>();

        for (int i = 0; i < meshes.Length; i++)
        {
            materials.Add(meshes[i].GetComponent<Renderer>().material);
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(ray.transform.position, ray.transform.forward, out hit, 1.5f))
        {
            if (hit.transform.tag != "Ground")
                canPlace = false;
            else
                canPlace = true;
        }
        else
        {
            canPlace = true;
        }

        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].GetComponent<Renderer>().material = canPlace ? canM : cantM;
        }
    }


    public bool Place(Vector3 placePosition, Vector3 placeRotation)
    {   
        transform.position = placePosition;
        transform.localEulerAngles = placeRotation;

        if (canPlace)
        {
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].GetComponent<Renderer>().material = materials[i];
            }
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = true;
            }
        }

        if (canPlace)
        {
            Destroy(this);
        }
	    return canPlace;
    }
}
