using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimacionTrofeo : MonoBehaviour {

    // public static AnimacionTrofeo at;

    public GameObject controller;
    public string funcion;

    private bool activado;
    private Canvas canvas;
    public GameObject fondo;
    public GameObject trofeo;
    public GameObject mago;

    private string siguiente_escena;
    private bool acierto;
    private int emocion;

    /*public void Awake()
    {
        if (at == null)
        {
            at = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (at != this)
            Destroy(gameObject);
    }*/

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        /*trofeo.GetComponent<Animator>().Play("Trofeo0");
        mago.GetComponent<Animator>().Play("Mago0");*/
    }

    public void IniciarAnimacion(bool acierto, int emocion, string siguiente_escena)
    {
        this.acierto = acierto;
        this.emocion = emocion;
        trofeo.GetComponent<BoxCollider>().enabled = true;
        this.siguiente_escena = siguiente_escena;
        canvas.enabled = true;
        Invoke("MostrarFondo",0.5f); 
    }
    
    private void MostrarFondo()
    {
        fondo.SetActive(true);
        fondo.GetComponent<Animator>().Play("Fondo" + (!acierto ? 1 : 0));
        mago.SetActive(true);
        mago.GetComponent<Animator>().Play("ApareceMago" + (!acierto ? 1 : 0));
        Invoke("MostrarMenu", 1f);
    }

    private void MostrarMenu()
    {
        trofeo.SetActive(true);
        trofeo.GetComponent<Animator>().Play("ApareceTrofeo" + emocion);
    }

    private void SiguienteEscena()
    {
        print("BREAK SIGUIENTE");
        Invoke("CargarEscena",2f);
    }

    private void CargarEscena()
    {
        if (!siguiente_escena.Equals("-"))
            SceneManager.LoadScene(siguiente_escena);
        else
        {
            canvas.enabled = false;
            fondo.SetActive(false);
            mago.SetActive(false);
            trofeo.SetActive(false);
            controller.SendMessage(funcion);
        }
    }
}
