using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.root.CompareTag("Trooper"))
        {
            int planetDamage = collision.collider.transform.root.GetComponent<EnemyPathfinding>().enemyStats.planetDamage;
            // destroy trooper 
            IngameUI.Instance.ReducePopulation(planetDamage);
            //Destroy(collision.gameObject);
        }
    }

}
