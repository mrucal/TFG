using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaCastilloController  : MonoBehaviour {

    public GameObject personaje;
    public GameObject interruptor;
    public GameObject sol;

    public SwitchController switch_controller;
    //public GameObject pelota_movimiento;

    private const float t_sig_escena = 2f;

    private const float td = 0f;
    private const float t_entrada = 5f;

    private bool enabled_interruptor = false;

    static string escena_anterior = "laberinto";

    private EstadoJuego estado_juego;

    private void Iniciar()
    {
        estado_juego.cargar();
        estado_juego.datos.ultima_escena = "06_EscenaCastillo";
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
    }

    // Use this for initialization
    void Start()
    {
        Iniciar();
        sol.GetComponent<Animator>().Play("SolAnimation");
        enabled_interruptor = false;
        print(escena_anterior);
        if (escena_anterior.Equals("laberinto"))
        {
            //boton.GetComponent<BoxCollider>().enabled = false;
            print("BREAK CASTILLO");
            switch_controller.desactivar_objetos();
            personaje.GetComponent<Animator>().Play("PersonajeLlegaCastillo");
            interruptor.GetComponent<Animator>().Play("PantallaParpadea");
        }else
        {
            interruptor.GetComponent<Animator>().Play("PantallaCorrecta");
            estado_juego.datos.ultima_escena = /*"09_EscenaMago"*/"08_EscenaLaberinto";
            print("BREAK ultima escena: " + estado_juego.datos.ultima_escena);
            GetComponents<AudioSource>()[1].Play();
            Invoke("escenaAbrirPuertas", GetComponents<AudioSource>()[1].clip.length+0.5f);
        }
    }

    void escenaAbrirPuertas()
    {
        personaje.GetComponent<Animator>().Play("PersonajeEntraCastillo");
        this.GetComponent<Animator>().Play("AbrirCastillo");
        Finalizar();
        StartCoroutine(SiguienteEscena(/*"09_EscenaMago"*/"08_EscenaLaberinto", t_entrada + t_sig_escena));
        escena_anterior = "laberinto";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void enableInterruptor()
    {
        Invoke("esperarIntroduccion", personaje.GetComponents<AudioSource>()[2].clip.length + 0.5f);
    }
    
    void esperarIntroduccion()
    {
        enabled_interruptor = true;
        //boton.GetComponent<BoxCollider>().enabled = false;
        switch_controller.activar_objetos();
    }

    void InterruptorOn()
    {
        if (enabled_interruptor)
        {
            enabled_interruptor = false;
            interruptor.GetComponents<AudioSource>()[0].Play();
            StartCoroutine(SiguienteEscena("07_EscenaParejas", t_sig_escena));
            escena_anterior = "parejas";
        }
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
}
