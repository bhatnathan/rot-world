using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Timelord : MonoBehaviour
{
    [Tooltip("Reference to the world's rotation")]
    [SerializeField] private BoolVariable isTimeStopped;
    [Tooltip("List of all the active dynamic object's datas")]
    [SerializeField] private DynamicObjectDataList dynamicObjectDatas;

    public void OnTimeInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TryStopTime();
        }
        else if (context.canceled)
        {
            TryStartTime();
        }
    }

    private void TryStopTime()
    {
        if(!isTimeStopped.Value && AreDynamicObjectsGrounded())
        {
            isTimeStopped.SetValue(true);
            Debug.Log("Stop time");
        }
    }

    private void TryStartTime()
    {
        if (isTimeStopped.Value)
        {
            isTimeStopped.SetValue(false);
            Debug.Log("Start time");
        }
    }

    private bool AreDynamicObjectsGrounded()
    {
        return dynamicObjectDatas.Values.All(obj => obj.IsGrounded());        
    }
}
