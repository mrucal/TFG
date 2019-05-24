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
        Invoke("playIntroduccion", 1f);
    }
	
    void playIntroduccion()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(playPrincipalesLlegando(GetComponent<AudioSource>().clip.length));
    }    

    public IEnumerator playPrincipalesLlegando( float seconds)
    {
        yield return new WaitForSeconds(seconds);
        personaje.GetComponent<Animator>().Play("PrincipalesLlegando");
    }

    public void continuarIntroduccion()
    {
        StartCoroutine(play(1, 0.5f));
        float tiempo = 0.5f;
        if (estado_juego.juegoGanado())
        {
            tiempo += GetComponents<AudioSource>()[1].clip.length + 0.5f;
            StartCoroutine(play(2, tiempo));
            tiempo += GetComponents<AudioSource>()[2].clip.length + 0.5f;
            StartCoroutine(play(3, tiempo));
            tiempo += GetComponents<AudioSource>()[3].clip.length;
            StartCoroutine(SiguienteEscena("10_EscenaPatio", tiempo + t_sig_escena));
        }
        else
        {
            tiempo += GetComponents<AudioSource>()[1].clip.length + 0.5f;
            StartCoroutine(play(4, tiempo));
            tiempo += GetComponents<AudioSource>()[4].clip.length + 0.5f;
            StartCoroutine(play(5, tiempo));
        }
    }

    public IEnumerator play(int i, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GetComponents<AudioSource>()[i].Play();
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
