using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeLord : MonoBehaviour
{
    [Tooltip("Reference to Is Time Stopped")]
    [SerializeField] private BoolVariable isTimeStopped;
    [Tooltip("List of all the active dynamic object's datas")]
    [SerializeField] private DynamicObjectDataList dynamicObjectDatas;
    [Tooltip("Event to send when time is stopped.")]
    [SerializeField] private GameEvent onTimeStop;
    [Tooltip("Event to send when time is started.")]
    [SerializeField] private GameEvent onTimeStart;

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

    private void Update()
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
            Time.timeScale = 0f;
            isTimeStopped.SetValue(true);
            if(onTimeStop != null)
                onTimeStop.Raise();
        }
    }

    private void TryStartTime()
    {        
        Time.timeScale = 1f;
        isTimeStopped.SetValue(false);
        if(onTimeStart != null)
            onTimeStart.Raise();
    }    

    private bool AreDynamicObjectsGrounded()
    {
        return dynamicObjectDatas.Values.All(obj => obj.IsGrounded());        
    }
}
