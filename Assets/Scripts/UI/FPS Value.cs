using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSValue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _FPSValue;


    public void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }

    void GetFPS()
    {
        float fps = (int)(1f / Time.unscaledDeltaTime);
        SetFBS(fps.ToString());
    }
    public void SetFBS(string fps)
    {
         _FPSValue.text ="FPS: "+ fps;
    }
}
