using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(LevelBuilder))]
public class LevelBuilderEditor : Editor
{    
    private enum EditMode
    {
        None,
        Create,
        Edit,
        Destroy
    }

    int prefabIndex = 0;
    EditMode editMode = EditMode.Create;    

    #region Inspector
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();        
        LevelBuilder level_builder = (LevelBuilder) target;
        if(level_builder == null) 
        {
            editMode = EditMode.None;
            return; 
        }

        ShowPrefabDropdown(level_builder);
        ShowEditMode();
    }    

    private void ShowPrefabDropdown(LevelBuilder level_builder)
    {
        if(level_builder.GetPrefabList() == null || level_builder.GetPrefabList().Count <= 0) { return; }
        this.prefabIndex = EditorGUILayout.Popup(prefabIndex, level_builder.GetPrefabList().ConvertAll(prefab => prefab.name).ToArray());

        if (EditorGUI.EndChangeCheck())
        {
            level_builder.SetActivePrefab(level_builder.GetPrefabList()[prefabIndex]);
        }

        GUILayout.Space(20);
    }

    private void ShowEditMode()
    {
        GUILayout.Label("Edit Mode");
        editMode = (EditMode)EditorGUILayout.EnumPopup(editMode, new GUILayoutOption[0]);
    }
    #endregion

    #region Scene
    void OnSceneGUI()
    {
        LevelBuilder level_builder = (LevelBuilder)target;

        if (editMode.Equals(EditMode.None) || level_builder == null) { return; }

        if (Event.current.type == EventType.MouseUp && Event.current.button == 1)
        {
            Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(worldRay, out hitInfo))
            {
                switch (editMode)
                {
                    case EditMode.Create:
                        Create(level_builder, hitInfo);
                        break;

                    case EditMode.Edit:
                        Edit(level_builder, hitInfo);
                        break;

                    case EditMode.Destroy:
                        Destroy(level_builder, hitInfo);
                        break;
                }
            }
            
        }
    }

    private void Create(LevelBuilder level_builder, RaycastHit hitInfo)
    {
        Vector3 spawnPoint = CalculatePositionFromCollider(hitInfo);
        GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(level_builder.GetActivePrefab(), level_builder.transform);
        go.transform.position = spawnPoint;
    }

    private void Edit(LevelBuilder level_builder, RaycastHit hitInfo)
    {                
        GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(level_builder.GetActivePrefab(), level_builder.transform);
        go.transform.position = hitInfo.collider.gameObject.transform.position;
        DestroyImmediate(hitInfo.collider.gameObject);
    }

    private void Destroy(LevelBuilder level_builder, RaycastHit hitInfo)
    {
        DestroyImmediate(hitInfo.collider.gameObject);
    }

    private Vector3 CalculatePositionFromCollider(RaycastHit hitInfo)
    {
        Vector3 objectPosition = hitInfo.collider.gameObject.transform.position;
        Vector3 hitPosition = hitInfo.point;
        Vector3 directionVector = hitPosition - objectPosition;
        Vector3 primaryDirectionVector = new Vector3(
            (Mathf.Abs(directionVector.x) > Mathf.Abs(directionVector.y) && Mathf.Abs(directionVector.x) > Mathf.Abs(directionVector.z)) ? 1 * Mathf.Sign(directionVector.x) : 0,
            (Mathf.Abs(directionVector.y) > Mathf.Abs(directionVector.x) && Mathf.Abs(directionVector.y) > Mathf.Abs(directionVector.z)) ? 1 * Mathf.Sign(directionVector.y) : 0,
            (Mathf.Abs(directionVector.z) > Mathf.Abs(directionVector.y) && Mathf.Abs(directionVector.z) > Mathf.Abs(directionVector.x)) ? 1 * Mathf.Sign(directionVector.z) : 0);

        return objectPosition + primaryDirectionVector;
    }

    

    #endregion    

}
