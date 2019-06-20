using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimacionTrofeoLaberinto : MonoBehaviour {

    // public static AnimacionTrofeo at;
    public EscenaLaberintoController elc;
    public SwitchController switch_controller;

    public GameObject audioganar;
    public GameObject audioperder;

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

    public GameObject boton_salida;

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

    public IEnumerator play(GameObject objeto, int i, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        objeto.GetComponents<AudioSource>()[i].Play();
    }

    float getTiempoAudio(GameObject objeto, int i)
    {
        return objeto.GetComponents<AudioSource>()[i].clip.length;
    }

    void activarBotonSalida()
    {
        boton_salida.SetActive(true);
    }

    private void MostrarFondo()
    {
        fondo.SetActive(true);
        fondo.GetComponent<Animator>().Play("Fondo" + (!acierto ? 1 : 0));
        float tiempo = 0f;
        if (acierto)
        {
            int m = Random.Range(3, 4);
            print("Break: " + m);
            StartCoroutine(play(gameObject, 0, tiempo));
            tiempo += getTiempoAudio(gameObject, 0) / 2;
            StartCoroutine(play(gameObject, m, tiempo));
            tiempo += getTiempoAudio(gameObject, m) + 0.5f;
            StartCoroutine(play(audioganar, emocion, tiempo));
            tiempo += getTiempoAudio(audioganar, emocion);
            Invoke("activarBotonSalida", tiempo);
            //GetComponents<AudioSource>()[0].Play();
        }
        else
        {
            StartCoroutine(play(gameObject, 1, tiempo));
            tiempo += getTiempoAudio(gameObject, 1) / 2;
            StartCoroutine(play(audioperder, emocion, tiempo));
            tiempo += getTiempoAudio(audioperder, emocion);
            Invoke("activarBotonSalida", tiempo);
            //GetComponents<AudioSource>()[1].Play();
        }
        mago.SetActive(true);
        mago.GetComponent<Animator>().Play("ApareceMago" + (!acierto ? 1 : 0));
        Invoke("MostrarMenu", 1f);
    }

    private void MostrarMenu()
    {
        if (acierto)
        {
            //print("BREAK MENU: " + emocion);
            /*trofeo.SetActive(true);
            trofeo.GetComponent<Animator>().Play("ApareceTrofeo" + emocion);*/
            trofeo1.SetActive(true);
            trofeo1.GetComponent<Animator>().Play("ApareceTrofeo" + emocion);
            trofeo2.SetActive(true);
            trofeo2.GetComponent<Animator>().Play("ApareceTrofeo" + emocion);
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
            boton_salida.SetActive(false);
            activado = false;
            //print("BREAK SIGUIENTE"); 
            GetComponents<AudioSource>()[2].Play();
            Invoke("CargarEscena", 1f);
        }
    }

    private void CargarEscena()
    {
        
        activado = true;
        switch_controller.activar_objetos();
        if (!siguiente_escena.Equals("-"))
        {
            elc.SendMessage("Finalizar");
            //SceneManager.LoadScene(siguiente_escena);
            GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().siguienteEscena(false);
        }
        else
        {
            canvas.enabled = false;
            fondo.SetActive(false);
            mago.SetActive(false);
            trofeo.SetActive(false);
            trofeo1.SetActive(false);
            trofeo2.SetActive(false);
            elc.siguienteCruce(false);
        }
    }
}
