using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        return prefabList == null ? new List<GameObject>() : prefabList.prefabs;
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1f, 0.4f);
        for (int j = 0; j < 20; j++) {
            for (int i = 0; i < 20; i++)
            {
                Gizmos.DrawLine(Vector3.forward * -20 + (Vector3.right * (i - 10)) + (Vector3.up * (j - 10)), Vector3.forward * 20 + (Vector3.right * (i - 10)) + (Vector3.up * (j - 10)));
                Gizmos.DrawLine(Vector3.right * -20 + (Vector3.forward * (i - 10)) + (Vector3.up * (j - 10)), Vector3.right * 20 + (Vector3.forward * (i - 10)) + (Vector3.up * (j - 10)));
            }
        }
    }*/

}
