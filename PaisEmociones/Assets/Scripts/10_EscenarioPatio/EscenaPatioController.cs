using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaPatioController  : MonoBehaviour {

    public GameObject personaje;
    public GameObject mago;
    public GameObject portal;

    public GameObject emoticono;
    public GameObject sol;

    private const float t_sig_escena = 2f;

    private const float td = 0f;

    private bool enabled_portal = false;

    private EstadoJuego estado_juego;

    private void Iniciar()
    {
        estado_juego.cargar();
        estado_juego.datos.ultima_escena = "10_EscenaPatio";
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

    void EnablePortal()
    {
        portal.GetComponents<AudioSource>()[0].Play();
        portal.transform.position = new Vector3(portal.transform.position.x, portal.transform.position.y, -1.5f);
        enabled_portal = true;
    }

    void EntrarPortal()
    {
        if(enabled_portal)
            personaje.GetComponent<Animator>().Play("EntrarPortal");
    }
    void Inicio()
    {
        mago.GetComponent<Animator>().Play("MagoPortal");
        sol.GetComponent<Animator>().Play("SolAnimation");
        emoticono.GetComponent<Animator>().Play("AlegriaAnimation");
    }

    void SiguienteEscena()
    {
        StartCoroutine(SiguienteEscena("01_EscenaParque", t_sig_escena));
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(GameObject.Find("Boton").GetComponent<BotonTrofeos>().gameObject);
        //SceneManager.LoadScene(escena);
        estado_juego.datos.final = true;
        estado_juego.siguienteEscena();
    }
}
