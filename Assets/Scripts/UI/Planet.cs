using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Trooper")
        {
            int planetDamage = collision.gameObject.GetComponent<EnemyStatsSO>().planetDamage;
            // destroy trooper 
            IngameUI.Instance.ReducePopulation(planetDamage);
            //Destroy(collision.gameObject);
        }
    }

}
