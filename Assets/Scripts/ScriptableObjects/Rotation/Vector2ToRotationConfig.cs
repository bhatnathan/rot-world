using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Vector2ToRotationConfig : ScriptableObject
{
    [System.Serializable]
    public struct Vector2ToRotationMapping
    {
        public float minAngle;
        public float maxAngle;
        public Rotation rotation;
    }

    [Header("Mappings")]
    [Tooltip("Mappings from angles to Axis rotation. 0 degrees is up. All angles should be positive.")]
    [SerializeField]
    private List<Vector2ToRotationMapping> mappings;

    public Rotation Vector2ToRotation(Vector2 vector)
    {        
        float angle = VectorUtils.Vec2ToRad(vector) * Mathf.Rad2Deg;
        angle = (angle <= 0f) ? angle + 360f : angle; //Make negative angles positive.        

        if(mappings.Exists(mapping => MappingPredicate(mapping, angle))) //Can't check result of Find for null as structs can't be null.
        {
            return mappings.Find(mapping => MappingPredicate(mapping, angle)).rotation;
        } else
        {
            throw new System.Exception($"Requested a Vector2 mapping for angle {angle} that did not correspond to any RotationMapping. Please check the Vector2ToRotationConfig object.");
        }        
    }

    private bool MappingPredicate (Vector2ToRotationMapping mapping, float angle) => angle > mapping.minAngle && angle <= mapping.maxAngle;
}
