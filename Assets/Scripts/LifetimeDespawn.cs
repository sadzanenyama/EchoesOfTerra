using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeDespawn : MonoBehaviour
{
    [SerializeField] private float time;

    private void OnEnable()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
