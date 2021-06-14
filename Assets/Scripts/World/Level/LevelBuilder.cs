using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    protected GameObject activePrefab;

    [SerializeField] private LevelBuilderPrefabs prefabList;

    public void SetActivePrefab(GameObject active_prefab)
    {
        this.activePrefab = active_prefab;
    }

    public GameObject GetActivePrefab()
    {
        return this.activePrefab;
    }

    public List<GameObject> GetPrefabList()
    {
        return prefabList == null ? new List<GameObject>() : prefabList.Prefabs;
    }

}
