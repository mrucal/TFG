using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaPuebloController : MonoBehaviour {

    public GameController controller;

    public GameObject niño;
    public GameObject niño_body;
    public GameObject coche;
    public GameObject fuente;
    public GameObject farola;
    public GameObject pelota;
    public GameObject perro;
    public GameObject señal;
    public AnimacionTrofeo animacion_trofeo;
    public SwitchController switch_controller;

    public GameObject emoticono;
    public GameObject sol;

    private bool farola_on;
    private bool encontrado;

    private const float t_sig_escena = 2f;

    private const float t_niño_feliz = 2.5f;
    private const float t_emoticono = 1f;

    private bool confirmacion = false;

    private EstadoJuego estado_juego;

    private void Iniciar()
    {
        estado_juego.cargar();
        estado_juego.datos.ultima_escena = "05_EscenaPueblo";
        estado_juego.guardar();
        //estado_juego.reset();
    }

    private void Finalizar()
    {
        estado_juego.guardar();
    }

    private void Awake()
    {
        //print("AWAKE PUEBLO");
        estado_juego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        animacion_trofeo = GameObject.Find("AnimacionTrofeo").GetComponent<AnimacionTrofeo>();
        Iniciar();
    }

    // Use this for initialization
    void Start()
    {
        sol.GetComponent<Animator>().Play("SolNubeAnimation");
        farola_on = false;
        encontrado = false;
        switch_controller.desactivar_objetos();
        Invoke("playIntroduccion", 1f);
    }

    void playIntroduccion()
    {
        float tiempo = 0f;
        StartCoroutine(play(0, 0f));
        tiempo += GetComponents<AudioSource>()[0].clip.length + 0.5f;
        StartCoroutine(play(1, tiempo));
        tiempo += GetComponents<AudioSource>()[1].clip.length + 0.5f;
        Invoke("activarTristin", tiempo);
    }

    public IEnumerator play(int i, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GetComponents<AudioSource>()[i].Play();
    }

    void activarTristin()
    {
        niño_body.GetComponent<BoxCollider>().enabled = true;
        
    }

    void terminarLlorar()
    {
        niño.GetComponents<AudioSource>()[1].Stop();
        niño.GetComponent<Animator>().Play("NiñoTristeIdle");
    }

    void activarObjetos()
    {
        switch_controller.activar_objetos();
    }

    public void playTristin()
    {
        niño_body.GetComponent<AudioSource>().Play();
        niño_body.GetComponent<BoxCollider>().enabled = false;

        float tiempo = 0f;
        for(int i =2; i < 6; i++)
        {
            switch (i)
            {
                case 3:
                    Invoke("terminarLlorar", tiempo);
                    break;
                case 5:
                    Invoke("activarObjetos", tiempo + GetComponents<AudioSource>()[5].clip.length);
                    break;
            }
            StartCoroutine(play(i, tiempo));
            tiempo += GetComponents<AudioSource>()[i].clip.length + 0.5f;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void CocheRun()
    {
        confirmacion = false;
        //ParticleSystem ps = fuente.GetComponent<ParticleSystem>();
        //print("Controller coche! "/*+ps.name*/);
        coche.GetComponent<Animator>().Play("CocheRunAnimation");
    }

    void FuenteRun()
    {
        confirmacion = false;
        fuente.GetComponent<Animator>().Play("FuenteRunAnimation");
    }

    void FarolaRun()
    {
        confirmacion = false;
        if (farola_on)
            farola.GetComponent<Animator>().Play("FarolaOffAnimation");
        else
        {
            switch_controller.desactivar_objetos_menos(0);
            farola.GetComponent<Animator>().Play("FarolaOnAnimation");
            switch_controller.Invoke("activar_objetos", farola.GetComponents<AudioSource>()[1].clip.length);
        }

        farola_on = !farola_on;
    }

    void PelotaRun()
    {
        confirmacion = false;
        pelota.GetComponent<Animator>().Play("PelotaAnimation");
    }

    void BancoRunEncontrado()
    {
        if (!encontrado)
        {
            switch_controller.desactivar_objetos_menos(0);
            perro.GetComponents<AudioSource>()[0].Play();
            perro.GetComponent<Animator>().Play("PerroEncontradoAnimation");
            StartCoroutine(esperarAnimacion(t_niño_feliz, niño, "NiñoFelizAnimation"));
            StartCoroutine(esperarAnimacion(t_niño_feliz + t_emoticono, emoticono, "AlegriaAnimation"));
            //StartCoroutine(esperarAnimacion(3.5f, emoticono, "AlegriaAnimation"));
            StartCoroutine(esperarAnimacion(3.5f, sol, "SolAnimation"));
            /*print("fallos: " + estado_juego.datos.fallos[estado_juego.datos.dificultad][2]);
            estado_juego.datos.fallos[estado_juego.datos.dificultad][2]++;
            print("fallos: " + estado_juego.datos.fallos[estado_juego.datos.dificultad][2]);*/

            estado_juego.incrementarAciertos(1);
            //Finalizar();
            StartCoroutine(SiguienteEscena(/*"06_EscenaLaberinto"*/"06_EscenaCastillo", t_niño_feliz + t_emoticono+t_sig_escena,true));
            encontrado = true;
        }
    }

    IEnumerator esperarAnimacionPerro(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        print("Esperar");
        emoticono.GetComponent<Animator>().Play("AlegriaAnimation");
        niño.GetComponent<Animator>().Play("NiñoFelizAnimation");
        //StartCoroutine(SiguienteEscena("EscenaPueblo", 7f));
    }

    void SalirPueblo()
    {
        perro.GetComponents<AudioSource>()[0].Play();
        if (confirmacion)
        {
            switch_controller.desactivar_objetos_menos(0);
            estado_juego.incrementarFallos(1);
            //Finalizar();
            StartCoroutine(SiguienteEscena(/*"06_EscenaLaberinto"*/"06_EscenaCastillo", t_sig_escena, false));
        }
        else
        {
            //Conejo pregunta si esta seguro de salir del pueblo
            señal.GetComponent<BoxCollider>().enabled = false;
            Invoke("playConfirmacion", 0.5f);
            Invoke("activarSeñal", señal.GetComponent<AudioSource>().clip.length);
        }
        
        confirmacion = !confirmacion;
    }

    void activarSeñal()
    {
        señal.GetComponent<BoxCollider>().enabled = true;
    }

    void playConfirmacion()
    {
        señal.GetComponent<AudioSource>().Play();
    }

    IEnumerator esperarAnimacion(float seconds, GameObject go, string animacion)
    {
        yield return new WaitForSeconds(seconds);
        go.GetComponent<Animator>().Play(animacion);
    }

    public IEnumerator SiguienteEscena(string escena, float seconds,bool acierto)
    {
        yield return new WaitForSeconds(seconds);
        //SceneManager.LoadScene(escena);
        coche.GetComponent<BoxCollider>().enabled = false;
        fuente.GetComponent<CapsuleCollider>().enabled = false;
        farola.GetComponent<BoxCollider>().enabled = false;
        pelota.GetComponent<BoxCollider>().enabled = false;

        //Finalizar();
        animacion_trofeo.IniciarAnimacion(acierto, 1, escena);
    }

}
