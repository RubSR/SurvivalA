using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    //Singleton
    //1. Nos instanciamos a nosotros mismos
    private static LoadSceneManager instance;
    // Otras variables
    public Image image;
    public float fadeSpeed;

    private void Awake()
    {
        //2. Comprobamos nuestra existencia
        if (instance == null)
        {
            //3. Nos inicializamos, creamos nuestra copia
            //this es un abreviatura para decir LoadSceneManager
            instance = this;
            DontDestroyOnLoad(gameObject);
            image.enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //Metodo al que llamaremos desde fuera 
    // Para activar esl carga de escena
    public static void LoadScene(int buildIndex)
    {
        //TODO: LLamar al corutina de carga
        instance.StartCoroutine(
            instance.LoadNextScene(buildIndex));
    }
    
    //Corutina de carga y fade
    IEnumerator LoadNextScene(int buildIndex)
    {
        //1.Activamos el image
        image.enabled = true;
        
        //2. Fade in de pantalla de carga
        float a = 0.0f;
        //Bajamos el alpha hasta 0 en un tiempo determinado
        while (a < 1.0f)
        {
            a += (1.0f / fadeSpeed) * Time.deltaTime;
            //Establcer en la imagen el nuevo alpha
            image.color = new Color(0, 0, 0, a);
            yield return null;
        }
        
        
        //3. Carga asincrona de la escena
        AsyncOperation ao = SceneManager.LoadSceneAsync(buildIndex);
        while (ao.isDone == false)
        {
            yield return null;
        }
        
        //4. Fade out de la pantalla de carga
         a = 1.0f;
        //Bajamos el alpha hasta 0 en un tiempo determinado
        while (a > 0.0f)
        {
            a -= (1.0f / fadeSpeed) * Time.deltaTime;
            //Establcer en la imagen el nuevo alpha
            image.color = new Color(0, 0, 0, a);
            yield return null;
        }
        //5. Desactivamos image
        image.enabled = false;
    }
}
