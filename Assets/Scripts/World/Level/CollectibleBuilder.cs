using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollectibleBuilder : MonoBehaviour
{
    [Tooltip("The cubes of the collectible will be made up of these colors.")]
    [SerializeField] private Color[] colors;
    [Tooltip("The proportion of subcubes to be culled to make the final shape.")]
    [SerializeField] private float cullFactor;

    void Awake()
    {        
        List<MeshRenderer> subcubes = new List<MeshRenderer>();
        subcubes.AddRange(GetComponentsInChildren<MeshRenderer>());

        List<int> chosenIndices = ChooseNRandomIndices(subcubes, Mathf.RoundToInt(subcubes.Count * cullFactor));

        for(int i = 0; i < chosenIndices.Count; i++)
        {
            subcubes[chosenIndices[i]].gameObject.SetActive(false);
        }

        for(int i = 0; i < subcubes.Count; i++)
        {            
            if(!subcubes[i].gameObject.activeInHierarchy) { continue; }

            Material cloned_material = new Material(subcubes[i].material);
            cloned_material.color = colors[Random.Range(0, colors.Length)];
            subcubes[i].material = cloned_material;                            
        }
    }

    private List<int> ChooseNRandomIndices(List<MeshRenderer> list, int n)
    {
        n = Mathf.Clamp(n, 0, list.Count - 1);

        int[] indices = Enumerable.Range(0, list.Count).ToArray();

        List<int> chosenIndices = new List<int>();
       

        for(int i = 0; i < n; i++)
        {
            int j = indices[Random.Range(0, indices.Length)];

            int temp = indices[i];
            indices[i] = indices[j];
            indices[j] = temp;

            chosenIndices.Add(indices[i]);            
        }        

        return chosenIndices;
    }



}
