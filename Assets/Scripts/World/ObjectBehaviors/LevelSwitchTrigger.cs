using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchTrigger : MonoBehaviour
{
    [Tooltip("Check to load next scene in the build order rather than the value in loadScene.")]
    [SerializeField] private bool loadNextScene;

    [Tooltip("The build index of the scene to load to")]
    [SerializeField] private int loadScene;

    [Tooltip("The delay from triggering a scene switch to the actual switch. Use for transition effects, etc.")]
    [SerializeField] private float sceneSwitchDelay;

    [Tooltip("Tag of the collider we want to activate this level switch")]
    [SerializeField] private string activationTag;

    [SerializeField] private GameEvent onLoadSceneEvent;

    public void Trigger()
    {
        SwitchScene();
    }

    public void TriggerDelayed()
    {
        StartCoroutine(SwitchSceneInSeconds(sceneSwitchDelay));
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
                StartCoroutine(SwitchSceneInSeconds(sceneSwitchDelay));                
            }
                
        }
    }

    private IEnumerator SwitchSceneInSeconds(float scene_switch_delay)
    {
        onLoadSceneEvent.Raise();
        yield return new WaitForSecondsRealtime(scene_switch_delay);
        SwitchScene();
    }

    private void SwitchScene()
    {
        int nextScene = (loadNextScene) ? (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings : loadScene;
        SceneManager.LoadScene(nextScene);
    }
}
