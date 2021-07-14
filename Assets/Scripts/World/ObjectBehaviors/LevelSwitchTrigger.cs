using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchTrigger : MonoBehaviour
{
    [Tooltip("Check to load next scene in the build order rather than the value in loadScene.")]
    [SerializeField] private bool loadNextScene;

    [Tooltip("The build index of the scene to load to")]
    [SerializeField] private int loadScene;

    [Tooltip("Tag of the collider we want to activate this level switch")]
    [SerializeField] private string activationTag;

    public void Trigger()
    {
        SwitchScene();
    }

    void OnTriggerEnter(Collider other)
    {
        CheckIfValid(other);
    }

    void OnTriggerStay(Collider other)
    {
        CheckIfValid(other);
    }

    private void CheckIfValid(Collider other)
    {
        if (other.gameObject.tag == activationTag)
        {
            if (other.gameObject.GetComponent<DynamicObject>().IsGrounded())
            {
                SwitchScene();                
            }
                
        }
    }

    private void SwitchScene()
    {
        int nextScene = (loadNextScene) ? (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings : loadScene;
        SceneManager.LoadScene(nextScene);
    }
}
