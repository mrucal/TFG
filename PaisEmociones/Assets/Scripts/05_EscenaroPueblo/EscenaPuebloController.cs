using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaPuebloController : MonoBehaviour {

    public GameController controller;

    public GameObject niño;
    public GameObject coche;
    public GameObject fuente;
    public GameObject farola;
    public GameObject pelota;
    public GameObject perro;
    public AnimacionTrofeo animacion_trofeo;

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
        estado_juego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        animacion_trofeo = GameObject.Find("AnimacionTrofeo").GetComponent<AnimacionTrofeo>();
    }

    // Use this for initialization
    void Start()
    {
        Iniciar();
        sol.GetComponent<Animator>().Play("SolNubeAnimation");
        farola_on = false;
        encontrado = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CocheRun()
    {
        //ParticleSystem ps = fuente.GetComponent<ParticleSystem>();
        //print("Controller coche! "/*+ps.name*/);
        coche.GetComponent<Animator>().Play("CocheRunAnimation");
    }

    void FuenteRun()
    {
        fuente.GetComponent<Animator>().Play("FuenteRunAnimation");
    }

    void FarolaRun()
    {
        if(farola_on)
            farola.GetComponent<Animator>().Play("FarolaOffAnimation");
        else
            farola.GetComponent<Animator>().Play("FarolaOnAnimation");

        farola_on = !farola_on;
    }

    void PelotaRun()
    {
        pelota.GetComponent<Animator>().Play("PelotaAnimation");
    }

    void BancoRunEncontrado()
    {
        if (!encontrado)
        {
            perro.GetComponent<Animator>().Play("PerroEncontradoAnimation");
            StartCoroutine(esperarAnimacion(t_niño_feliz, niño, "NiñoFelizAnimation"));
            StartCoroutine(esperarAnimacion(t_niño_feliz + t_emoticono, emoticono, "AlegriaAnimation"));
            //StartCoroutine(esperarAnimacion(3.5f, emoticono, "AlegriaAnimation"));
            StartCoroutine(esperarAnimacion(3.5f, sol, "SolAnimation"));
            /*print("fallos: " + estado_juego.datos.fallos[estado_juego.datos.dificultad][2]);
            estado_juego.datos.fallos[estado_juego.datos.dificultad][2]++;
            print("fallos: " + estado_juego.datos.fallos[estado_juego.datos.dificultad][2]);*/

            estado_juego.incrementarAciertos(1);
            Finalizar();
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
        if (confirmacion)
        {
            estado_juego.incrementarFallos(1);
            Finalizar();
            StartCoroutine(SiguienteEscena(/*"06_EscenaLaberinto"*/"06_EscenaCastillo", t_sig_escena,false));
        }
        /*else
            Conejo pregunta si esta seguro de salir del pueblo
        */
        confirmacion = !confirmacion;
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
        animacion_trofeo.IniciarAnimacion(acierto, 1, escena);
    }

}
