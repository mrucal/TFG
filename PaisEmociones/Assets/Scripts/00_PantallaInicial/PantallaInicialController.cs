using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInicialController  : MonoBehaviour {

    private EstadoJuego estado_juego;

    public GameObject boton_inicio;

    private const float t_sig_escena = 2f;

    public GameObject boton_atras, boton_adelante;

    private void Awake()
    {
        estado_juego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        estado_juego.cargar();
        //boton_atras.GetComponent<SpriteRenderer>().enabled = false;
       // boton_adelante.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Use this for initialization
    void Start () {
        //print("Ultima escena: " + estado_juego.datos.ultima_escena);
        //estado_juego.reset();
        //estado_juego.datos.dificultad = 1;
        estado_juego.guardar();
        /*if (!string.IsNullOrEmpty(estado_juego.datos.ultima_escena))
            SceneManager.LoadScene(estado_juego.datos.ultima_escena);*/
    }

    
    public LayerMask touchInputMask;
    // Update is called once per frame
    void Update ()
    {

        GameObject.Find("BotonAtras").GetComponent<BotonAtrasAdelante>().GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("BotonAdelante").GetComponent<BotonAtrasAdelante>().GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("BotonAtras").GetComponent<BotonAtrasAdelante>().GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("BotonAdelante").GetComponent<BotonAtrasAdelante>().GetComponent<BoxCollider>().enabled = false;

        /*if (Input.touchCount > 0)
        {
            print("ntouch: "+Input.touchCount+" touch: "+Input.GetTouch(0));
            boton_inicio.SetActive(false);
        }*/

        foreach (Touch touch in Input.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;


            if(Physics.Raycast(ray, out hit, touchInputMask)) {
                GameObject recipient = hit.transform.gameObject;
                print("TOCADO: "+recipient.name+" "+touch.phase+" "+TouchPhase.Began+ " "+TouchPhase.Ended);
                /*switch (touch.phase)
                {
                    case TouchPhase.Began:
                        GetComponent<AudioSource>().Play();
                    case TouchPhase.Stationary:
                        recipient.SendMessage("OnTouchDrag");
                        break;
                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:
                    case TouchPhase.Moved:
                        recipient.SendMessage("OnTouchUp");
                        break;
                }*/
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
                    if (touch.phase == TouchPhase.Began)
                        GetComponent<AudioSource>().Play();
                    recipient.SendMessage("OnTouchDrag");
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Canceled)
                    recipient.SendMessage("OnTouchUp");
            }
        }
	}

    void Inicio()
    {
        boton_inicio.GetComponent<Animator>().Play("BotonInicioPulsado");
        boton_inicio.GetComponent<AudioSource>().Play();
        estado_juego.guardar();
        if (string.IsNullOrEmpty(estado_juego.datos.ultima_escena))
            StartCoroutine(SiguienteEscena("01_EscenaParque", /*1+t_sig_escena*/boton_inicio.GetComponent<AudioSource>().clip.length + 1));
        else
            StartCoroutine(SiguienteEscena(estado_juego.datos.ultima_escena,/* 1 + t_sig_escena*/boton_inicio.GetComponent<AudioSource>().clip.length + 1));
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //SceneManager.LoadScene(escena);
        SceneManager.LoadScene(estado_juego.datos.escenas[estado_juego.datos.indice_escena]);
    }
}
