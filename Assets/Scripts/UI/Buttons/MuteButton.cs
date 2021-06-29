using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MuteButton : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private BoolVariable isAudioMuted;
    [Space]
    [Header("Button Icon")]
    [SerializeField] private Sprite unmutedIcon;
    [SerializeField] private Sprite mutedIcon;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        UpdateButtonIcon();
    }

    public void OnClick()
    {
        isAudioMuted.SetValue(!isAudioMuted.Value);
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        image.sprite = isAudioMuted.Value ? mutedIcon : unmutedIcon;
    }
}
