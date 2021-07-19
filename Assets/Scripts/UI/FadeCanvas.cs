using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeCanvas : MonoBehaviour
{
    [Tooltip("The image used as screen fade. Typically a black field covering the screen.")]
    [SerializeField] private Image fadeImage;
    [Tooltip("The speed at which the screen should fade in and out.")]
    [SerializeField] private float fadeSpeed;

    private float alpha = 0f;
    private bool shouldFadeOut = false;    

    void Awake()
    {
        fadeImage.enabled = true;
        SetAlpha(1f);
        shouldFadeOut = false;
        StartCoroutine(Fade());
    }

    public void FadeIn()
    {
        shouldFadeOut = false;
    }

    public void FadeOut()
    {
        shouldFadeOut = true;
    } 

    private IEnumerator Fade()
    {
        while (true)
        {
            if (shouldFadeOut && alpha < 1f)
            {
                alpha += fadeSpeed * Time.unscaledDeltaTime;
            }
            else
            if (!shouldFadeOut && alpha > 0f)
            {
                alpha -= fadeSpeed * Time.unscaledDeltaTime;
            }
            

            SetAlpha(alpha);

            yield return null;
        }
    }

    private void SetAlpha(float new_alpha)
    {
        alpha = Mathf.Clamp(new_alpha, 0, 1f);
        fadeImage.color = new Color(0f, 0f, 0f, alpha);
    }
}
