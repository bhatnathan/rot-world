using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(DynamicObject))]
public class PlayerMovement : MonoBehaviour
{    
    [Header("Movement")]
    [Tooltip("How fast does the player start moving horizontally")]
    [SerializeField] private float horizontalAcceleration;
    [Tooltip("How fast can the player move max in a horizontal direction.")]
    [SerializeField] private float horizontalMaxSpeed;
    [Space]
    [Header("Jumping")]
    [Tooltip("How fast is the initial player jump velocity.")]
    [SerializeField] private float jumpVelocity;    

    //Saved input variables to apply in FixedUpdate
    private Vector2 inputDirection;
    private bool shouldJump;

    //Components
    private Rigidbody body;
    private DynamicObject dynamicObject;

    // Start is called before the first frame update
    void Start()
    {
        //Movement variables
        inputDirection = Vector2.zero;
        shouldJump = false;

        //Components
        body = GetComponent<Rigidbody>();
        dynamicObject = GetComponent<DynamicObject>();
    }

    void FixedUpdate()
    {
        ApplyHorizontalMovement(FetchDesiredHorizontalMovement());
        ApplyJump();
    }

    public void OnMove(InputAction.CallbackContext context) //Event from Player Input component.
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    private Vector3 InputToWorldDirection(Vector2 input_direction)
    {
        Vector3 camera_forward = Camera.main.transform.forward;
        Vector3 camera_right = Camera.main.transform.right;
        Vector3 world_up = dynamicObject.GetWorldRotation() * Vector3.up;

        Vector3 flattened_camera_forward = camera_forward - Vector3.Dot(camera_forward, world_up) * world_up;
        Vector3 flattened_camera_right = camera_right - Vector3.Dot(camera_right, world_up) * world_up;

        return Vector3.Normalize(flattened_camera_forward * input_direction.y + flattened_camera_right * input_direction.x);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(dynamicObject.IsGrounded())
                shouldJump = true;
        }
    }

    private Vector3 FetchDesiredHorizontalMovement()
    {
        return inputDirection.magnitude * horizontalMaxSpeed * InputToWorldDirection(inputDirection);
    }

    private void ApplyHorizontalMovement(Vector3 movement)
    {
        Vector3 world_up = dynamicObject.GetWorldRotation() * Vector3.up;
        Vector3 body_horizontal_velocity = body.velocity - Vector3.Dot(body.velocity, world_up) * world_up;

        Vector3 velocity_offset = movement - body_horizontal_velocity;

        float velocity_change_amount = horizontalAcceleration * Time.fixedDeltaTime;
        velocity_change_amount = Mathf.Clamp(velocity_change_amount, 0, velocity_offset.magnitude);

        body.velocity += velocity_offset.normalized * velocity_change_amount;
    }

    private void ApplyJump()
    {
        if (!shouldJump)
            return;

        Vector3 jump_dir = dynamicObject.GetWorldRotation() * Vector3.up;
        body.velocity += jumpVelocity * jump_dir;

        shouldJump = false;
    }
}
