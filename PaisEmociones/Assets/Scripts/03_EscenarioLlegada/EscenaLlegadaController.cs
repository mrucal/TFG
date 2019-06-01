using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaLlegadaController : MonoBehaviour {

    public GameObject personaje;
    public GameObject conejo;
    public GameObject sol;

    public GameObject viñeta_pelota;
    public GameObject viñeta_alegria;
    public GameObject viñeta_tristeza;
    public GameObject viñeta_enfado;
    public GameObject viñeta_trofeo_alegria;
    public GameObject viñeta_trofeo_tristeza;
    public GameObject viñeta_trofeo_enfado;
    public GameObject trofeo;
    public GameObject emoticono;
    public GameObject circuloL;
    public GameObject circuloR;

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
        conejo.GetComponent<Animator>().Play("ConejoIdleAnimation");
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
    
    public void playN01()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(playDialogo_i(1, GetComponent<AudioSource>().clip.length+ 0.5f));
    }

    public void continuarDialogo()
    {
        StartCoroutine(playDialogo_i(16, 0.5f));
    }

    public IEnumerator playDialogo_i(int i,float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //print("playDialogo_i " + i);
        try
        {
            float t_transicion = 0.5f;
            switch (i)
            {
                case 2:
                    personaje.GetComponent<Animator>().Play("PersonajeLlorandoAnimation");
                    break;
                case 5:
                    personaje.GetComponent<Animator>().Play("PersonajeIdleTriste");
                    personaje.GetComponents<AudioSource>()[1].Stop();
                    break;
                case 7:
                    Invoke("playFelizIdleAnimation", 0.3f);
                    StartCoroutine(playAnimation(personaje, "playFelizIdleAnimation", 0.3f));
                    break;
                case 10:
                    StartCoroutine(mostrarViñeta(viñeta_alegria, 6f));
                    StartCoroutine(ocultarViñeta(viñeta_alegria, 13.5f));
                    break;
                case 11:
                    viñeta_alegria.SetActive(false);
                    StartCoroutine(mostrarViñeta(viñeta_pelota, 0.5f));
                    break;
                case 12:
                    viñeta_pelota.SetActive(false);
                    StartCoroutine(mostrarViñeta(viñeta_tristeza, 8f));
                    StartCoroutine(ocultarViñeta(viñeta_tristeza, 16f));
                    StartCoroutine(mostrarViñeta(viñeta_enfado, 17.5f));
                    StartCoroutine(ocultarViñeta(viñeta_enfado, 27f));
                    break;
                case 13:
                    StartCoroutine(mostrarViñeta(viñeta_trofeo_alegria, 15f));
                    StartCoroutine(ocultarViñeta(viñeta_trofeo_alegria, 15.8f));
                    StartCoroutine(mostrarViñeta(viñeta_trofeo_tristeza, 16f));
                    StartCoroutine(ocultarViñeta(viñeta_trofeo_tristeza, 16.8f));
                    StartCoroutine(mostrarViñeta(viñeta_trofeo_enfado, 17f));
                    StartCoroutine(ocultarViñeta(viñeta_trofeo_enfado, 17.8f));
                    break;
                case 15:
                    GameObject.Find("AnimacionTrofeoLlegada").GetComponent<AnimacionTrofeoLlegada>().IniciarAnimacion();
                    break;
                case 17:
                    trofeo.SetActive(true);
                    emoticono.SetActive(true);
                    StartCoroutine(mostrarViñeta(circuloL, 1f));
                    StartCoroutine(ocultarViñeta(circuloL, 4f));
                    StartCoroutine(mostrarViñeta(circuloR, 5));
                    StartCoroutine(ocultarViñeta(circuloR, 8f));
                    break;
            }
            //print("TRY " + i);
            GetComponents<AudioSource>()[i].Play();
            if (i != 15)
            {
                //print("STARTCOROUTINE " + i);
                StartCoroutine(playDialogo_i(i + 1, GetComponents<AudioSource>()[i].clip.length + t_transicion));
            }
        }
        catch
        {
            //print("CATCH " + i);
            PersonajeFeliz();
        }
    }

    void playFelizIdleAnimation()
    {
        personaje.GetComponent<Animator>().Play("PersonajeFelizIdleAnimation");
            }


    public IEnumerator playAnimation(GameObject objteo, string animacion, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        objteo.GetComponent<Animator>().Play(animacion);
    }

    IEnumerator mostrarViñeta(GameObject viñeta,float seconds)
    {
        yield return new WaitForSeconds(seconds);
        viñeta.SetActive(true);
    }

    IEnumerator ocultarViñeta(GameObject viñeta, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        viñeta.SetActive(false);
    }
}
