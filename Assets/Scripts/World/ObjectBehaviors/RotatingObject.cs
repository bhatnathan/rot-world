using UnityEngine;

public class RotatingObject : MonoBehaviour
{

    [SerializeField] private bool isRotationVectorRandom = false;
    [SerializeField] private Vector3 rotationVector = Vector3.right;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private Space rotationSpace;

    private void Awake()
    {
        if (isRotationVectorRandom)
        {
            rotationVector = new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));
        }
    }

    void Update()
    {
        transform.Rotate(rotationVector, rotationSpeed * Time.deltaTime, rotationSpace);
    }
}
