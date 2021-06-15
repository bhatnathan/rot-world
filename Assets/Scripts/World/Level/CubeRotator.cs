using UnityEngine;

/**
 * Set the block to a random rotation to vary texture/decor patterns.
 */
public class CubeRotator : MonoBehaviour
{
    private float[] rotations = new float[] { 0, 90, 180, -90 };

    [SerializeField] private Transform model = null;

    private void Awake()
    {
        model.transform.localRotation = Quaternion.Euler(new Vector3(GetRandomRotation(), GetRandomRotation()));
    }

    private float GetRandomRotation()
    {
        return rotations[Random.Range(0, rotations.Length)];
    }
}
