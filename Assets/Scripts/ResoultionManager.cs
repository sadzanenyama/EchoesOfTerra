using System.Collections.Generic;
using UnityEngine;

public class ResoultionManager : MonoBehaviour
{
    public static ResoultionManager ResInstance;
    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;
    private List<string> options = new List<string>();

    public bool IsInitialized { get; private set; }

    private void Awake()
    {
        if (ResInstance == null)
        {
            ResInstance = this;
            DontDestroyOnLoad(gameObject);
            InitializeResolutions();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeResolutions()
    {
        resolutions = Screen.resolutions;
        Debug.Log($"Total resolutions: {resolutions.Length}");

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = $"{resolutions[i].width}x{resolutions[i].height} {resolutions[i].refreshRateRatio.value:F2} Hz";
            options.Add(resolutionOption);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height &&
                Mathf.Approximately((float)resolutions[i].refreshRateRatio.value, (float)Screen.currentResolution.refreshRateRatio.value))
            {
                currentResolutionIndex = i;
            }
        }

        Debug.Log($"Options count: {options.Count}");
        Debug.Log($"Current resolution index: {currentResolutionIndex}");

        SetResolution(currentResolutionIndex);
        IsInitialized = true;
    }

    public List<string> GetScreenOptions()
    {
        Debug.Log($"GetScreenOptions called. Options count: {options.Count}");
        return options;
    }

    public void SetCurrentIndex(int value)
    {
        currentResolutionIndex = value;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }
}