using UnityEngine;
using UnityEngine.Rendering;

public class RenderRandorizer : MonoBehaviour
{
    [Header("Fog")]
    [SerializeField] bool fogEnabled = true;
    [SerializeField] Color fogColor = new Color(0.5f, 0.5f, 0.5f);
    [SerializeField] float fogDensity = 0.01f;

    [Header("Ambient Light")]
    [SerializeField] Color ambientLight = new Color(0.4f, 0.4f, 0.5f);
    [SerializeField] float ambientIntensity = 1f;

    //just grabs the current render settings and store them in the fields when you enter play mode
    void Awake()
    {
        fogEnabled = RenderSettings.fog;
        fogColor = RenderSettings.fogColor;
        fogDensity = RenderSettings.fogDensity;
        ambientLight = RenderSettings.ambientLight;
        ambientIntensity = RenderSettings.ambientIntensity;
    }

    public void ApplyFog()
    {
        RenderSettings.fog = fogEnabled;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = fogDensity;
    }

    public void ApplyAmbient()
    {
        RenderSettings.ambientMode = AmbientMode.Flat;
        RenderSettings.ambientLight = ambientLight;
        RenderSettings.ambientIntensity = ambientIntensity;
    }
}
