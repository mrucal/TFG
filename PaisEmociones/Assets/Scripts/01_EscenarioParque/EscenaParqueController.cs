using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaParqueController  : MonoBehaviour {

    private EstadoJuego estado_juego;

    public GameObject personaje;
    public GameObject portal;
    public GameObject pelota;
    public GameObject sol;
    //public GameObject pelota_movimiento;

    private const float t_sig_escena = 2f;

    private const float td = 0f;
    private const float t_transicion = 3f;
    private const float t_patada = 0.65f;
    private const float t_pelota = 2f;

    //private static string escena_anterior = "Inicio";

    private void Iniciar()
    {        
        estado_juego.cargar();
        //estado_juego.datos.ultima_escena = "01_EscenaParque";
        estado_juego.guardar();
    }

    private void Finalizar()
    {
        estado_juego.guardar();
    }

    private void Awake()
    {
        estado_juego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        //estado_juego.reset();
    }

    // Use this for initialization
    void Start () {

        sol.GetComponent<Animator>().Play("SolAnimation");
        //print(escena_anterior);
        //if (escena_anterior.Equals("Inicio"))
        if(string.IsNullOrEmpty(estado_juego.datos.ultima_escena))
        {
            //escena_anterior = "Patio";
            //print("BREAK 1 "+ escena_anterior);
            StartCoroutine(patear(td + t_transicion));
        }
        else
        {
            //print("BREAK 2 "+pelota.transform.position);
            //pelota.transform.position = new Vector3(0,0,0);
            

            pelota.GetComponent<Animator>().Play("PelotaNoVisible");
            personaje.GetComponent<Animator>().Play("PersonajeNoVisible");
            //print("BREAK 3 " + pelota.transform.position);
            //escena_anterior = "Inicio";
        }
        Iniciar();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void PortalDesaparece()
    {
        portal.GetComponent<Animator>().Play("PortalDesaparece");
    }

    void PortalVisible()
    {
        print("BREAK PORTAL");
        portal.GetComponent<AudioSource>().Play();
        portal.GetComponent<Animator>().Play("PortalApareceRaiz");
    }

    void PersonajeRegresa()
    {
        print("BREAK PERSONAJE");
        personaje.GetComponent<Animator>().Play("PersonajeRegresa");
    }

    IEnumerator patear(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        personaje.GetComponent<Animator>().Play("PersonajePelota");
        StartCoroutine(moverpelota(t_patada));
    }

    IEnumerator moverpelota(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        pelota.GetComponent<Animator>().Play("PelotaMoveAnimation");
        //Finalizar();
        StartCoroutine(SiguienteEscena("02_EscenaPortal", t_pelota + t_sig_escena/*7f*/));
        //pelota_movimiento.GetComponent<Animator>().Play("AlegriaAnimation");
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Finalizar();
        SceneManager.LoadScene(escena);
    }
}
