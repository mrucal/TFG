using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimacionTrofeo : MonoBehaviour {

    public static AnimacionTrofeo at;
    public SwitchController switch_controller;

    public GameObject audioganar;
    public GameObject audioperder;

    public AnimacionTrofeo prefab;

    public GameObject controller;
    public string funcion;

    private bool activado;
    private Canvas canvas;
    public GameObject fondo;
    public GameObject trofeo;
    public GameObject mago;

    private string siguiente_escena;
    private bool acierto;
    private int emocion;

    public GameObject boton_salida;


    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        IniciarEscena();
        try
        {
            switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();
            boton_salida.SetActive(false);
        }
        catch { }
    }

    public void Awake()
    {
        if (at == null)
        {
            at = this;
            canvas = GetComponent<Canvas>();
            SceneManager.sceneLoaded += this.OnLoadCallback;
            DontDestroyOnLoad(gameObject);
        }
        else if (at != this)
            Destroy(gameObject);
    }

    private void IniciarEscena()
    {
        canvas.enabled = false;
        fondo.SetActive(false);
        mago.SetActive(false);
        trofeo.SetActive(false);
    }

    void Start()
    {
        
        IniciarEscena();
        /*trofeo.GetComponent<Animator>().Play("Trofeo0");
        mago.GetComponent<Animator>().Play("Mago0");*/
        switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();
    }

    public void IniciarAnimacion(bool acierto, int emocion, string siguiente_escena)
    {
        switch_controller.desactivar_objetos();
        this.acierto = acierto;
        this.emocion = emocion;
        trofeo.GetComponent<BoxCollider>().enabled = true;
        this.siguiente_escena = siguiente_escena;
        canvas.enabled = true;
        Invoke("MostrarFondo",0.5f); 
    }

    public IEnumerator play(GameObject objeto, int i, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        objeto.GetComponents<AudioSource>()[i].Play();
    }

    float getTiempoAudio(GameObject objeto, int i)
    {
        return objeto.GetComponents<AudioSource>()[i].clip.length;
    }

    void activarBotonSalida()
    {
        boton_salida.SetActive(true);
    }
    private void MostrarFondo()
    {
        fondo.SetActive(true);
        fondo.GetComponent<Animator>().Play("Fondo" + (!acierto ? 1 : 0));
        float tiempo = 0f;
        if (acierto)
        {
            int m = Random.Range(3, 4);
            print("Break: " + m);
            StartCoroutine(play(gameObject, 0, tiempo));
            tiempo += getTiempoAudio(gameObject, 0)/2;
            StartCoroutine(play(gameObject, m, tiempo));
            tiempo += getTiempoAudio(gameObject, m)+0.5f;
            StartCoroutine(play(audioganar, emocion, tiempo));
            tiempo += getTiempoAudio(audioganar, emocion);
            Invoke("activarBotonSalida",tiempo);
            //GetComponents<AudioSource>()[0].Play();
        }
        else
        {
            StartCoroutine(play(gameObject, 1, tiempo));
            tiempo += getTiempoAudio(gameObject, 1)/2;
            StartCoroutine(play(audioperder, emocion, tiempo));
            tiempo += getTiempoAudio(audioperder, emocion);
            Invoke("activarBotonSalida", tiempo);
            //GetComponents<AudioSource>()[1].Play();
        }
        mago.SetActive(true);
        mago.GetComponent<Animator>().Play("ApareceMago" + (!acierto ? 1 : 0));
        Invoke("MostrarMenu", 1f);
    }

    private void MostrarMenu()
    {
        trofeo.SetActive(true);
        trofeo.GetComponent<Animator>().Play("ApareceTrofeo" + emocion);
    }

    private void SiguienteEscena()
    {
        //print("BREAK SIGUIENTE");
        boton_salida.SetActive(false);
        GetComponents<AudioSource>()[2].Play();
        switch_controller.activar_objeto_i(0);
        Invoke("CargarEscena",1f);
    }

    void Finalizar()
    {
        EstadoJuego estado_juego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        estado_juego.guardar();
    }

    private void CargarEscena()
    {
        if (!siguiente_escena.Equals("-"))
        {
            //controller.SendMessage("Finalizar");
            Finalizar();
            //SceneManager.LoadScene(escena);
            GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().siguienteEscena(acierto);
        }
        else
        {
            canvas.enabled = false;
            fondo.SetActive(false);
            mago.SetActive(false);
            trofeo.SetActive(false);
            controller.SendMessage(funcion);
        }
    }
}
