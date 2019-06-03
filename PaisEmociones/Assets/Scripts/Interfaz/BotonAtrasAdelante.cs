using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonAtrasAdelante : MonoBehaviour {

    public static BotonAtrasAdelante batras;
    public static BotonAtrasAdelante badelante;

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        GetComponent<SpriteRenderer>().enabled = (GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().datos.modo_atras);
        GetComponent<BoxCollider>().enabled = (GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().datos.modo_atras);
        if (GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().datos.indice_escena == 0 && gameObject.name.Equals("BotonAtras"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }

    }

    private void Awake()
    {
        if (gameObject.name.Equals("BotonAtras"))
        {
            if (batras == null)
            {
                batras = this;
                SceneManager.sceneLoaded += this.OnLoadCallback;
                DontDestroyOnLoad(gameObject);
            }
            else if (batras != this)
            {
                Destroy(gameObject);
            }
        }
        if (gameObject.name.Equals("BotonAdelante"))
        {
            if (badelante == null)
            {
                badelante = this;
                SceneManager.sceneLoaded += this.OnLoadCallback;
                DontDestroyOnLoad(gameObject);
            }
            else if (badelante != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        if (gameObject.name.Equals("BotonAtras"))
        {
            GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().anteriorEscenaBoton();
        }
        else
        {
            GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().siguienteEscenaBoton();
        }
    }
}
