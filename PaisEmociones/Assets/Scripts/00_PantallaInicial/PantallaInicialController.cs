using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInicialController  : MonoBehaviour {

    private EstadoJuego estado_juego;

    public GameObject boton_inicio;

    private const float t_sig_escena = 2f;

    private void Awake()
    {
        estado_juego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        
    }

    // Use this for initialization
    void Start () {
        estado_juego.cargar();
        print("Ultima escena: " + estado_juego.datos.ultima_escena);
        //estado_juego.reset();
        //estado_juego.datos.dificultad = 1;
        estado_juego.guardar();
        if (!string.IsNullOrEmpty(estado_juego.datos.ultima_escena))
            SceneManager.LoadScene(estado_juego.datos.ultima_escena);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Inicio()
    {
        boton_inicio.GetComponent<Animator>().Play("BotonInicioPulsado");
        StartCoroutine(SiguienteEscena("01_EscenaParque", 1+t_sig_escena));
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
}
