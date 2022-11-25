using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1. Necesito decirle Unity donde aparecera dentro del menu 
// creacion de assets
[CreateAssetMenu(fileName = "Objeto", 
    menuName = "Nuevo objeto")]
public class ItemSo : ScriptableObject
{//es una plantilla para crear assets de tipo item
    [Header("Info")] 
    public string nombre;

    public string descripcion;

    public Tipoitem tipo;
    //Sprite que va a tener cuando este en el inventario
    public Sprite icono;
    //Prefab para cuando lo soltemos del inventario
    public GameObject dropPrefab;

    [Header("Stackeo")] 
    public bool puedeStackear;

    public int maxCantidadStack;
}

public enum Tipoitem
{
    Recurso,
    Equipable,
    Consumible
}
