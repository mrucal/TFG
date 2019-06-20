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

    public SwitchController switch_controller;

    private const float t_sig_escena = 1f;

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
        BotonTrofeos bt = GameObject.Find("Boton").GetComponent<BotonTrofeos>();
        bt.transform.position = new Vector3(bt.transform.position.x,bt.transform.position.y,-3.5f);
        Iniciar();
    }

    // Use this for initialization
    void Start()
    {
        switch_controller.desactivar_objetos();
        sol.GetComponent<Animator>().Play("SolNubeAnimation");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void iniciarIntroduccion()
    {
        print("Introduccion");
        switch_controller.desactivar_objeto_i(1);
        float tiempo = 0f;
        StartCoroutine(play(0, tiempo));
        tiempo += GetComponents<AudioSource>()[0].clip.length + 0.5f;
        StartCoroutine(play(1, tiempo));
        tiempo += GetComponents<AudioSource>()[1].clip.length + 0.5f;
        Invoke("activarNiña", tiempo);
    }

    public IEnumerator play(int i, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GetComponents<AudioSource>()[i].Play();
    }

    private void activarNiña()
    {
        switch_controller.activar_objeto_i(1);
    }
    void EnablePortal()
    {
        portal.GetComponents<AudioSource>()[0].Play();
        portal.transform.position = new Vector3(portal.transform.position.x, portal.transform.position.y, -1.5f);
        enabled_portal = true;
    }

    void enableBotonTrofeo()
    {
        switch_controller.activar_lista_objetos(new int[] {0,1});
    }

    void EntrarPortal()
    {
        if (enabled_portal)
        {
            portal.GetComponents<AudioSource>()[1].Play();
            float tiempo = 0.5f;
            for (int i = 4; i < 6; i++)
            {
                StartCoroutine(play(i, tiempo));
                tiempo += GetComponents<AudioSource>()[i].clip.length + 0.5f;
            }
            Invoke("animacionEntrarPortal", tiempo - 0.5f);
            //personaje.GetComponent<Animator>().Play("EntrarPortal");
        }
    }

    void animacionEntrarPortal()
    {
        personaje.GetComponent<Animator>().Play("EntrarPortal");
    }

    void playIntroduccionAnimacionPortal()
    {
        mago.GetComponent<Animator>().Play("MagoFeliz");
        sol.GetComponent<Animator>().Play("SolAnimation");
        emoticono.GetComponent<Animator>().Play("AlegriaAnimation");
        float tiempo = 0f;
        for(int i= 2; i < 4; i++)
        {
            StartCoroutine(play(i, tiempo));
            tiempo += GetComponents<AudioSource>()[i].clip.length + 0.5f;
        }
        Invoke("AnimacionPortal", tiempo-0.5f);
    }

    void AnimacionPortal()
    {
        mago.GetComponent<Animator>().Play("MagoPortal");
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
