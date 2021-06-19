using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
    [Tooltip("Audio Source with music clip to play.")]
    [SerializeField] private AudioSource audioSource;

    private bool hasPlayed = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Remove Duplicates
        BGMPlayer[] collection = FindObjectsOfType<BGMPlayer>();

        foreach (BGMPlayer bgm_player in collection)
        {
            if (bgm_player == this)
                continue;

            if (bgm_player.GetBGMName() == GetBGMName())
                if(hasPlayed)
                    Destroy(bgm_player.gameObject);
                else
                    Destroy(gameObject);
            else if (hasPlayed)
                Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        hasPlayed = true;
    }

    public string GetBGMName()
    {
        return audioSource.clip.name;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
