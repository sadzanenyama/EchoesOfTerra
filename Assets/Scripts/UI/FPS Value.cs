using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSValue : MonoBehaviour
{
    public float fps;
    [SerializeField] private TextMeshProUGUI _FPSValue;

    public int samplesToCount = 10;
    private int samplesCount;
    private float totalTime;

    [SerializeField] private bool limitFrameRate;
    [SerializeField] private int FPSLimit = 60;

    // Start is called before the first frame update
    void Start()
    {
        samplesCount = 0;
        totalTime = 0;

        if (limitFrameRate)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = FPSLimit;
        }

        InvokeRepeating("GetFPS", 1, 1);
    }

    private void Update()
    {
        samplesCount++;
        totalTime += Time.deltaTime;
    }

    void GetFPS()
    {
        if (samplesCount >= samplesToCount)
        {
            fps = (int)(samplesCount / totalTime);
            _FPSValue.text = fps + " FPS";
            totalTime = 0;
            samplesCount = 0;
        }
    }
}
