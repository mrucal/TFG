using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimacionTrofeoLaberinto : MonoBehaviour {

    // public static AnimacionTrofeo at;
    public EscenaLaberintoController elc;
    public SwitchController switch_controller;
    private bool activado;
    private Canvas canvas;
    public GameObject fondo;
    public GameObject trofeo;
    public GameObject trofeo1;
    public GameObject trofeo2;
    public GameObject mago;

    private string siguiente_escena;
    private bool acierto;
    private int emocion;
    private int emocion1;
    private int emocion2;

    void Start()
    {
        activado = true;
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        /*trofeo.GetComponent<Animator>().Play("Trofeo0");
        mago.GetComponent<Animator>().Play("Mago0");*/
    }

    public void IniciarAnimacion(bool acierto,int emocion, int emocion1, int emocion2, string siguiente_escena)
    {
        switch_controller.desactivar_objetos();
        this.acierto = acierto;
        if (acierto)
        {
            this.emocion = emocion;
            trofeo.GetComponent<BoxCollider>().enabled = true;
        }else
        {
            this.emocion1 = emocion1;
            this.emocion2 = emocion2;
            trofeo1.GetComponent<BoxCollider>().enabled = true;
            trofeo2.GetComponent<BoxCollider>().enabled = true;
        }

        this.siguiente_escena = siguiente_escena;
        canvas.enabled = true;
        Invoke("MostrarFondo", 0.5f);
    }

    public void IniciarAnimacion(int emocion1, int emocion2, string siguiente_escena)
    {
        switch_controller.desactivar_objetos();
        Time.timeScale = 0;
        this.acierto = false;
        this.emocion1 = emocion1;
        this.emocion2 = emocion2;
        trofeo1.GetComponent<BoxCollider>().enabled = true;
        trofeo2.GetComponent<BoxCollider>().enabled = true;
        this.siguiente_escena = siguiente_escena;
        canvas.enabled = true;
        Invoke("MostrarFondo",0.5f); 
    }

    public void IniciarAnimacion(int emocion, string siguiente_escena)
    {
        this.acierto = true;
        this.emocion = emocion;
        trofeo.GetComponent<BoxCollider>().enabled = true;
        this.siguiente_escena = siguiente_escena;
        canvas.enabled = true;
        Invoke("MostrarFondo", 0.5f);
    }

    private void MostrarFondo()
    {
        fondo.SetActive(true);
        fondo.GetComponent<Animator>().Play("Fondo" + (!acierto ? 1 : 0));
        if (acierto)
        {
            GetComponents<AudioSource>()[0].Play();
        }
        else
        {
            GetComponents<AudioSource>()[2].Play();
        }
        mago.SetActive(true);
        mago.GetComponent<Animator>().Play("ApareceMago" + (!acierto ? 1 : 0));
        Invoke("MostrarMenu", 1f);
    }

    private void MostrarMenu()
    {
        if (acierto)
        {
            print("BREAK MENU: " + emocion);
            trofeo.SetActive(true);
            trofeo.GetComponent<Animator>().Play("ApareceTrofeo" + emocion);
        }
        else
        {
            trofeo1.SetActive(true);
            trofeo1.GetComponent<Animator>().Play("ApareceTrofeo" + emocion1);
            trofeo2.SetActive(true);
            trofeo2.GetComponent<Animator>().Play("ApareceTrofeo" + emocion2);
        }
    }

    private void SiguienteEscena()
    {
        if (activado)
        {
            activado = false;
            //print("BREAK SIGUIENTE");
            GetComponents<AudioSource>()[4].Play();
            switch_controller.activar_objetos();
            Invoke("CargarEscena", 2f);
        }
    }

    private void CargarEscena()
    {
        
        activado = true;
        if (!siguiente_escena.Equals("-"))
            SceneManager.LoadScene(siguiente_escena);
        else
        {
            canvas.enabled = false;
            fondo.SetActive(false);
            mago.SetActive(false);
            trofeo.SetActive(false);
            trofeo1.SetActive(false);
            trofeo2.SetActive(false);
            elc.siguienteCruce();
        }
    }
}
