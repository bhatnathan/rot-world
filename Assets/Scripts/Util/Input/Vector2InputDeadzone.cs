using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/**
 * Replaces context.started and context.cancelled with UnityEvents for inputs using a custom deadzone.
 */
public class Vector2InputDeadzone : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("Events will be triggered after the magnitude of the input vector exceeds this value. Behaviour is undefined if the default deadzone exceeds this value.")]
    [Range(0, 1)]
    [SerializeField]
    private float deadzone;    
    [Space]
    [Header("Events")]
    [SerializeField]
    private UnityEvent<Vector2> OnInputStarted;
    [SerializeField]
    private UnityEvent OnInputCanceled;

    private bool isInputStarted;
    private float sqrDeadzone;

    private void Awake()
    {
        sqrDeadzone = deadzone * deadzone; //Precalculate to compare vector sqrMagnitudes and avoid a sqr root calc each input update.
    }

    public void OnInput(InputAction.CallbackContext context)
    {
        Vector2 input_value = context.ReadValue<Vector2>();
        
        if(!isInputStarted && input_value.sqrMagnitude > sqrDeadzone)
        {
            isInputStarted = true;
            OnInputStarted.Invoke(input_value);
        } else if(isInputStarted && input_value.sqrMagnitude < sqrDeadzone)
        {
            isInputStarted = false;
            OnInputCanceled.Invoke();
        }
    }
}
