using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cactusManager : MonoBehaviour
{
    
    //Daño
    public float damage;
    //Cada cuantos segundos va a pegar
    public float damageRate;
    //Lista de gente a la que tiene que pegar
    private List<GameObject> thingsToDamage = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DealDamage());
    }

    //Si alguien entra en colision lo meto en la lista
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            thingsToDamage.Add(collision.gameObject);
        }else if (collision.gameObject.CompareTag("Enemy"))
        {
            thingsToDamage.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (thingsToDamage.Contains(other.gameObject))
        {
            thingsToDamage.Remove(other.gameObject);
        }
    }
    
    //Rutina con bulce infinito que se va a ejecutar
    // cada x segundos
    IEnumerator DealDamage()
    {
        //Dentro de un bucle infinito
        //Vamos a hacer daño de todos lo de la lista
        // cada 0.5 segundos
        while (true)
        {
            
            for (int i = 0; i < thingsToDamage.Count; i++)
            {
                //OJO que podemos tener al player o enemigos
                switch (thingsToDamage[i].tag)
                {
                    case "Player":
                        thingsToDamage[i]
                            .GetComponent<PlayerNeedsManager>()
                            .TakeDamage(damage);
                        break;
                    //TODO: Case de Enemy
                    
                }
            }
            
            //Decirle a la corutina que pare 0.5s
            yield return new WaitForSeconds(damageRate);
        }
    }
}
