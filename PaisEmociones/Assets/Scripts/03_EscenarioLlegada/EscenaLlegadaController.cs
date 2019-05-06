using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaLlegadaController : MonoBehaviour {

    public GameObject personaje;

    public GameObject sol;

    private const float t_sig_escena = 2f;

    private const float t_animacion_inicial = 7.3f;
    private const float td = 0f;

    private EstadoJuego estado_juego;

    private void Iniciar()
    {
        estado_juego.cargar();
        estado_juego.datos.ultima_escena = "03_EscenaLlegada";
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
    void Start ()
    {
        Iniciar();
        sol.GetComponent<Animator>().Play("SolNubeAnimation");
        for (int i = 0; i < 3; i++)
            estado_juego.ganarTrofeo(i);

        //StartCoroutine(PersonajeFeliz(t_animacion_inicial+td));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //public IEnumerator PersonajeFeliz(float seconds)
    public void PersonajeFeliz()
    {
        //print("BREAK " + personaje.name);
        personaje.GetComponent<Animator>().Play("PersonajeFelizIdleAnimation");
        sol.GetComponent<Animator>().Play("SolAnimation");
        StartCoroutine(SiguienteEscena("04_EscenaDragon", t_sig_escena));
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Finalizar();
        SceneManager.LoadScene(escena);
    }
    

}
