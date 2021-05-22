using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private QuaternionReference worldRotation;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //BEGIN TEMP USING THIS FOR DEBUG INPUT
        Vector2 input_direction = Vector2.zero;

        input_direction += Input.GetKey(KeyCode.UpArrow) ? Vector2.up : Vector2.zero;
        input_direction += Input.GetKey(KeyCode.RightArrow) ? Vector2.right : Vector2.zero;
        input_direction += Input.GetKey(KeyCode.LeftArrow) ? Vector2.left : Vector2.zero;
        input_direction += Input.GetKey(KeyCode.DownArrow) ? Vector2.down : Vector2.zero;

        input_direction.Normalize();

        //This is super temp hack because it messes up gravity and all external forces etc and rigidbody manipulation should be in FixedUpdate anyway
        GetComponent<Rigidbody>().velocity = moveSpeed * InputToWorldDirection(input_direction);
        //END TEMP
    }

    private Vector3 InputToWorldDirection(Vector2 input_direction)
    {
        Vector3 camera_forward = Camera.main.transform.forward;
        Vector3 camera_right = Camera.main.transform.right;
        Vector3 world_up = worldRotation.Value * Vector3.up;

        Vector3 flattened_camera_forward = camera_forward - Vector3.Dot(camera_forward, world_up) * world_up;
        Vector3 flattened_camera_right = camera_right - Vector3.Dot(camera_right, world_up) * world_up;

        return Vector3.Normalize(flattened_camera_forward * input_direction.y + flattened_camera_right * input_direction.x);
    }
}
