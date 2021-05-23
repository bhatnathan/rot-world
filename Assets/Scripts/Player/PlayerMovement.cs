using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private QuaternionReference worldRotation; //Reference to the world's rotation
    [SerializeField] private float horizontalAcceleration; //How fast does the player start moving horizontally
    [SerializeField] private float horizontalMaxSpeed; //How fast can the player move max in a horizontal direction.

    private Vector3 movement; //The calculated movement we want to apply based on inputs

    //Components
    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //BEGIN TEMP USING THIS FOR DEBUG INPUT
        Vector2 input_direction = Vector2.zero;
        float input_mag = 1f; //When input is done calculate properly

        input_direction += Input.GetKey(KeyCode.UpArrow) ? Vector2.up : Vector2.zero;
        input_direction += Input.GetKey(KeyCode.RightArrow) ? Vector2.right : Vector2.zero;
        input_direction += Input.GetKey(KeyCode.LeftArrow) ? Vector2.left : Vector2.zero;
        input_direction += Input.GetKey(KeyCode.DownArrow) ? Vector2.down : Vector2.zero;

        input_direction.Normalize();

        movement = input_mag * horizontalMaxSpeed * InputToWorldDirection(input_direction);
        //END TEMP
    }

    void FixedUpdate()
    {
        ApplyHorizontalMovement(movement);
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

    private void ApplyHorizontalMovement(Vector3 movement)
    {
        Vector3 world_up = worldRotation.Value * Vector3.up;
        Vector3 body_horizontal_velocity = body.velocity - Vector3.Dot(body.velocity, world_up) * world_up;

        Vector3 velocity_offset = movement - body_horizontal_velocity;

        float velocity_change_amount = horizontalAcceleration * Time.deltaTime;
        velocity_change_amount = Mathf.Clamp(velocity_change_amount, 0, velocity_offset.magnitude);

        body.velocity += velocity_offset.normalized * velocity_change_amount;
    }
}
