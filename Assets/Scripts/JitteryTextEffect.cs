using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JitteryTextEffect : MonoBehaviour
{
    public float jitterRange;
    RectTransform rectTransform;


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition = Vector2.zero + new Vector2(Random.Range(-jitterRange, jitterRange), Random.Range(-jitterRange, jitterRange));
    }
}
