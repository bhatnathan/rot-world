using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
    [Tooltip("Audio Source with music clip to play.")]
    [SerializeField] private AudioSource audioSource;

    private bool isMain = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Remove Duplicates
        BGMPlayer[] collection = FindObjectsOfType<BGMPlayer>();

        foreach (BGMPlayer bgm_player in collection)
        {
            if (bgm_player == this)
                continue;

            if (bgm_player.GetBGMName() != GetBGMName())
                audioSource.clip = bgm_player.GetAudioClip();

            if (isMain)
                Destroy(bgm_player.gameObject);
        }

        isMain = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public string GetBGMName()
    {
        return audioSource.clip.name;
    }

    public AudioClip GetAudioClip()
    {
        return audioSource.clip;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        audioSource.Stop();
    }
}
