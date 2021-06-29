using UnityEngine;

/**
 * Set the block to a random rotation to vary texture/decor patterns.
 */
public class CubeRotator : MonoBehaviour
{
    private float[] rotations = new float[] { 0, 90, 180, -90 };

    [SerializeField] private Transform model = null;
    [SerializeField] private bool xAxis = true;
    [SerializeField] private bool yAxis = true;
    [SerializeField] private bool zAxis = false;

    private void Awake()
    {
        model.transform.localRotation = Quaternion.Euler(new Vector3(xAxis ? GetRandomRotation() : 0f, yAxis ? GetRandomRotation() : 0f, zAxis ? GetRandomRotation() : 0f));
    }

    private float GetRandomRotation()
    {
        return rotations[Random.Range(0, rotations.Length)];
    }
}
