using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MuteButton : MonoBehaviour
{       
    [Header("Button Icon")]
    [SerializeField] private Sprite unmutedIcon;
    [SerializeField] private Sprite mutedIcon;

    private bool isMuted;
    private Image image;

    private void Awake()
    {        
        image = GetComponent<Image>();        
        SetIsMuted(Mathf.Approximately(0f, AudioListener.volume));
    }

    public void OnClick()
    {
        SetIsMuted(!isMuted);        
    }

    private void SetIsMuted(bool is_muted)
    {
        isMuted = is_muted;

        AudioListener.volume = (isMuted) ? 0f : 1f;
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        image.sprite = isMuted ? mutedIcon : unmutedIcon;
    }
}