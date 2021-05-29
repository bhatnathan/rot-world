using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchTrigger : MonoBehaviour
{
    [Tooltip("The build index of the scene to load to")]
    [SerializeField] private int loadScene;

    [Tooltip("Tag of the collider we want to activate this level switch")]
    [SerializeField] private string activationTag;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == activationTag)
            SceneManager.LoadScene(loadScene);
    }
}
