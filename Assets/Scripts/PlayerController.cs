using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{

    [Header("Camara")] 
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    public float lookSensi;
    private float camCurXRot;
    private Vector2 mouseDelta;

    [Header("Movimiento")] 
    public float moveSpeed;
    private Vector2 curMovInput;
    private Rigidbody rig;
    public float jumpForce;
    public LayerMask groundLayerMask;
    
    // Inicializamos el  rigibody
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //Escondemos el raton
        Cursor.lockState = CursorLockMode.Locked;

    }

   /*
    * Metodo que sera llamado por el InputSystem
    * en el capturaremos el vector2 del raton
    */
   public void OnLookInput(InputAction.CallbackContext context)
   {
       mouseDelta = context.ReadValue<Vector2>();
   }

   /**
    * Metodo que sera llamado por el InputSystem
    * en el capturaremos el vector2 del movimiento
    */
   public void OnMoveInput(InputAction.CallbackContext context)
   {
       //Comprobamos si se estan pulsando las teclas
       if (context.phase == InputActionPhase.Performed)
       {
           curMovInput = context.ReadValue<Vector2>();
       }else if (context.phase == InputActionPhase.Canceled)
       {
        curMovInput = Vector2.zero;   
       }
   }

   /**
    * Metodo que sera llamado por el InputSystem
    * en el que comprobaremos si se ha presionado
    * el boton de saltar
    */
   public void OnJumpInput(InputAction.CallbackContext context)
   {
       //1. Se acaba de presionar el boton de saltar?
       if (context.phase == InputActionPhase.Started)
       {
           //2.Comprobar si estamos tocando tierra
           if (Grounded())
           {
               //3.Saltar
               rig.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
           }
       }
   }

   private bool Grounded()
   {
       //Creamos un array de Ray llamada rays
       Ray[] rays = new Ray[4]
       {
           new Ray(transform.position +
                   (transform.forward * 0.2f),
               Vector3.down),
           new Ray(transform.position +
                   (-transform.forward * 0.2f),
               Vector3.down),
           new Ray(transform.position +
                   (transform.right * 0.2f),
               Vector3.down),
           new Ray(transform.position +
                   (-transform.right * 0.2f),
               Vector3.down),

       };
       
       //Disparamos los 4 rayos y comprobamos si
       // alguno de ellos toca tierra
       for (int i = 0; i < rays.Length; i++)
       {
           if (Physics.Raycast(rays[i],
                   0.1f,groundLayerMask))
           {
               return true;
           }
       }

       return false;
   }

   private void LateUpdate()
   {
       CameraLook();
   }
   
   //FIXED UPDATE PARA MANEJO DE FISICAS
   private void FixedUpdate()
   {
       Move();
   }

   private void Move()
   {
     
       /*
        * Calcular la direccion de mov
        * pero relativa a donde estemo mirando
        */
       Vector3 dir = transform.forward * curMovInput.y
                     + transform.right * curMovInput.x;
       // Darle velocidad
       dir *= moveSpeed;
       //Liberamos la y del dir
       dir.y = rig.velocity.y;
       // Movemo el rigibody
       rig.velocity = dir;

   }


   private void CameraLook()
   {
      //Rotacion vertical de la camara
      camCurXRot += mouseDelta.y * lookSensi;
      //Limitar la rotacion para que no pase del min ni del max
      camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
      // Rotar
      cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
      //Rotar en horizontal
      transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensi, 0);
   }
}
