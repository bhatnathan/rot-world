using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class LevelNameSetter : MonoBehaviour
{
    [SerializeField] private int levelNameLength = 40;

    private TMPro.TextMeshProUGUI textmesh;

    private void Awake()
    {
        textmesh = GetComponent<TMPro.TextMeshProUGUI>();        
    }

    void Start()
    {
        string level_name = SceneManager.GetActiveScene().name;

        if(level_name.Length >= levelNameLength)
        {
            level_name = level_name.Substring(0, levelNameLength - 3) + "...";
        }
        else
        for(int i = 0; i < levelNameLength - level_name.Length - 1; i++)
        {
            level_name += " ";
        }
        level_name += "|";

        textmesh.text = level_name;
    }

}
