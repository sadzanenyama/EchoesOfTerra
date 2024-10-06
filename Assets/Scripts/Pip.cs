using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pip : MonoBehaviour
{
    [SerializeField] private GameObject _active;    

    public void PipActivate(bool activate)
    {
        _active.SetActive(activate);
    }

}
