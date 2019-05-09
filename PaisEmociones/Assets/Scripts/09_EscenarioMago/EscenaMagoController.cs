using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaMagoController  : MonoBehaviour {

    public GameObject personaje;

    public GameObject emoticono;
    public GameObject sol;

    private const float t_sig_escena = 2f;

    private const float td = 0f;

    private EstadoJuego estado_juego;

    private void Iniciar()
    {
        estado_juego.cargar();
        estado_juego.datos.ultima_escena = "09_EscenaMago";
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
        Iniciar();
    }

    // Use this for initialization
    void Start()
    {
        sol.GetComponent<Animator>().Play("SolNubeAnimation");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Inicio()
    {
        StartCoroutine(SiguienteEscena("10_EscenaPatio", t_sig_escena));
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
}
