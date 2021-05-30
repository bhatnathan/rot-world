using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Timelord : MonoBehaviour
{
    [Tooltip("Reference to the world's rotation")]
    [SerializeField] private BoolVariable isTimeStopped;
    [Tooltip("List of all the active dynamic object's datas")]
    [SerializeField] private DynamicObjectDataList dynamicObjectDatas;

    private bool shouldStopTime;

    public void OnTimeInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            shouldStopTime = true;            
        }
        else if (context.canceled)
        {
            shouldStopTime = false;            
        }
    }    

    private void FixedUpdate()
    {
        if (!isTimeStopped.Value && shouldStopTime)
        {
            TryStopTime();
        }
        else if(isTimeStopped.Value && !shouldStopTime)
        {
            TryStartTime();
        }
    }

    private void TryStopTime()
    {
        if (AreDynamicObjectsGrounded())
        {
            isTimeStopped.SetValue(true);
            Debug.Log("Stop time");
        }
    }

    private void TryStartTime()
    {
        isTimeStopped.SetValue(false);
        Debug.Log("Start time");
    }

    private bool AreDynamicObjectsGrounded()
    {
        return dynamicObjectDatas.Values.All(obj => obj.IsGrounded());        
    }
}
