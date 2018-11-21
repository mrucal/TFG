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

    public GameObject emoticono;
    public GameObject sol;

    private bool farola_on;
    private bool encontrado;

    private const float t_niño_feliz = 2.5f;
    private const float t_emoticono = 1f;
    private const float t_sig_escena = 5f;

    private bool confirmacion = false;

	// Use this for initialization
	void Start ()
    {
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
            StartCoroutine(SiguienteEscena("06_EscenaLaberinto", t_niño_feliz + t_emoticono+t_sig_escena));
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
        if(confirmacion)
            StartCoroutine(SiguienteEscena("06_EscenaLaberinto", t_sig_escena));
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

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }

}
