using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class PostProcessingBehaviour : MonoBehaviour
{
    private PostProcessVolume volume;
    private ColorGrading colorGrading = null;

    void Start()
    {
        volume = GetComponent<PostProcessVolume>();

        volume.profile.TryGetSettings(out colorGrading);
    }

    public void SetColorGrading(bool enable)
    {
        colorGrading.active = enable;
    }
}
