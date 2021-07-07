using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevelButton : MonoBehaviour
{    
    [SerializeField] private GameEvent resetLevelEvent;

    public void OnClick()
    {
        resetLevelEvent.Raise();
    }
}
