using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOption : MonoBehaviour
{

    [SerializeField] private List<Pip> pips; 


    public void SetPipsActive(int pipActivate)
    {
        for(int i = 0; i < pipActivate; i++)
        {

            pips[i].PipActivate(true);
           
        }
    }
    public void ResetPips()
    {
        for (int i = 0; i < pips.Count; i++)
        {
            pips[i].PipActivate(false);
        }
    }
}
