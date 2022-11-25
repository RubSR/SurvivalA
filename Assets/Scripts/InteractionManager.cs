using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
   //Varibales --
   public float checkRate = 0.05f;
   private float lastCheckTime;
   public float distancia;
   public LayerMask layerMask;


}

public interface IInteractuable
{
    // Nombramos dos metodos
    string GetMensajeEnPantalla();
    void AlInteractuar();
}


