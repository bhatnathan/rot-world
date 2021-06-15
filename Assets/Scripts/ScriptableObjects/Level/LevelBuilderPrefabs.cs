using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelBuilderPrefabs : ScriptableObject
{
    [SerializeField] private List<GameObject> prefabs;

    public List<GameObject> Prefabs
    {
        get { return prefabs; }
    }
}
