using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RelativeLayerMaskQuery))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private QuaternionReference worldRotation; //Reference to the world's rotation
    [Space]
    [SerializeField] private float horizontalAcceleration; //How fast does the player start moving horizontally
    [SerializeField] private float horizontalMaxSpeed; //How fast can the player move max in a horizontal direction.
    [Space]
    [SerializeField] private float jumpVelocity; //How fast does the player jump
    [Space]
    [SerializeField] private LayerMask groundLayer; //What layer do we consider ground for this player.

    //Saved movement variables to apply in FixedUpdate
    private Vector3 movement; //The calculated movement we want to apply based on inputs
    private bool shouldJump;

    //Components
    private Rigidbody body;
    private RelativeLayerMaskQuery analyser;

    // Start is called before the first frame update
    void Start()
    {
        //Movement variables
        movement = Vector3.zero;
        shouldJump = false;

        //Components
        body = GetComponent<Rigidbody>();
        analyser = GetComponent<RelativeLayerMaskQuery>();
    }

    void FixedUpdate()
    {
        ApplyHorizontalMovement(movement);
        ApplyJump();
    }

    public void OnMove(InputAction.CallbackContext context) //Event from Player Input component.
    {
        Vector2 input_direction = context.ReadValue<Vector2>();
        float input_mag = 1f;
        movement = input_mag * horizontalMaxSpeed * InputToWorldDirection(input_direction);
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

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(analyser.IsLayerClose(groundLayer, worldRotation.Value))
                shouldJump = true;
        }
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

    private void ApplyJump()
    {
        if (!shouldJump)
            return;

        Vector3 jump_dir = worldRotation.Value * Vector3.up;
        body.velocity += jumpVelocity * jump_dir;

        shouldJump = false;
    }
}
