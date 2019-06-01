using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaLaberintoController  : MonoBehaviour {

    public GameObject personaje;
    public GameObject conejo;
    public GameObject emoticono_pequeño;
    public AnimacionTrofeoLaberinto animacion_trofeo;
    public MenuTrofeo2 mt;
    public SwitchController switch_controller;
    public GameObject audios_descripciones;
    public GameObject audios_instrucciones;
    //public GameObject pelota_movimiento;

    public int dificultad;
    private int n_cruces;
    private int cruce_i = 0, cruce_actual;
    private int[] cruce_actual_emociones = new int[3];
    private List<int> cruces;

    private int n_pasillos = 3;
    public Pasillo[] pasillos;
    public GameObject[] seleccion;
    public int[] emociones_pasillos = { 0, 0, 0 };
    public int[] indices_cartas = { 0, 0, 0 };

    /*public PasilloTexturas texturas0;
    public PasilloTexturas texturas1;
    public PasilloTexturas texturas2;*/

    public EmocionTexturas[] texturas;// = new PasilloTexturas[3];

    private int[] soluciones_dif0 = { 0, 1, 2 };
    private int[] soluciones_dif1 = { 0, 0, 1, 1, 2, 2 };
    private int[] soluciones_dif2 = { 0, 0, 0, 1, 1, 1, 2, 2, 2 };//GVGRRV

    private int[][] soluciones = new int[3][];

    private int solucion_actual;

    private bool clicks_activos = true;

    private const float t_sig_escena = 2f;

    private const float td = 0f;

    private EstadoJuego estado_juego;

    private void Iniciar()
    {
        estado_juego.cargar();
        estado_juego.datos.ultima_escena = "08_EscenaLaberinto";
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
        animacion_trofeo = GameObject.Find("AnimacionTrofeoLaberinto").GetComponent<AnimacionTrofeoLaberinto>();
        switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();
        mt = GameObject.Find("MenuTrofeos").GetComponent<MenuTrofeo2>();
        conejo.GetComponent<Animator>().Play("ConejoIdleAnimation");
        Iniciar();
    }

    // Use this for initialization
    void Start()
    {
        dificultad = estado_juego.datos.dificultad;
        n_cruces = 3 * (dificultad +1);/*2 + (2 * dificultad);*/
        soluciones[0] = soluciones_dif0;
        soluciones[1] = soluciones_dif1;
        soluciones[2] = soluciones_dif2;
        /*texturas[0] = texturas0;
        texturas[1] = texturas1;
        texturas[2] = texturas2;*/
        cruces = new List<int>();
        for (int i = 0; i < soluciones[dificultad].Length; i++)
            cruces.Add(i);
        siguienteCruce(true);
        switch_controller.desactivar_objetos();
        StartCoroutine(play(1, 1f));
        switch_controller.Invoke("activar_objetos", GetComponents<AudioSource>()[1].clip.length + 1f);
        StartCoroutine(playDescripciones(GetComponents<AudioSource>()[1].clip.length + 1f));
    }

    public IEnumerator play(int i, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GetComponents<AudioSource>()[i].Play();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public bool isClicksActivos()
    {
        return clicks_activos;
    }

    IEnumerator playDescripciones(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        int n_cartas_alegria = 9, n_cartas_tristeza = 8;
        int[] indices_audios = { 0,0,0};
        for(int i = 0; i < indices_audios.Length; i++)
        {
            switch (emociones_pasillos[i])
            {
                case 0:
                    indices_audios[i] = indices_cartas[i];
                    break;
                case 1:
                    indices_audios[i] = n_cartas_alegria + indices_cartas[i];
                    break;
                case 2:
                    indices_audios[i] = n_cartas_alegria + n_cartas_tristeza + indices_cartas[i];
                    break;
            }
        }
                
        float tiempo = 0.5f;
        switch_controller.desactivar_objetos();

        StartCoroutine(playInstruccion(emociones_pasillos[solucion_actual], tiempo));
        tiempo += audios_instrucciones.GetComponents<AudioSource>()[solucion_actual].clip.length + 1f;


        StartCoroutine(playDescripcion(indices_audios[0], tiempo));
        StartCoroutine(activarSeleccion(0, true, tiempo));
        tiempo += audios_descripciones.GetComponents<AudioSource>()[0].clip.length + 1f;
        StartCoroutine(activarSeleccion(0, false, tiempo));
        tiempo += 0.5f;

        StartCoroutine(playDescripcion(indices_audios[1], tiempo));
        StartCoroutine(activarSeleccion(1, true, tiempo));
        tiempo += audios_descripciones.GetComponents<AudioSource>()[1].clip.length + 1f;
        StartCoroutine(activarSeleccion(1, false, tiempo));
        tiempo += 0.5f;

        StartCoroutine(playDescripcion(indices_audios[2], tiempo));
        StartCoroutine(activarSeleccion(2, true, tiempo));
        tiempo += audios_descripciones.GetComponents<AudioSource>()[2].clip.length + 1f;
        StartCoroutine(activarSeleccion(2, false, tiempo));

        switch_controller.Invoke("activar_objetos", tiempo + 1f);
    }

    public IEnumerator playInstruccion(int i, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        audios_instrucciones.GetComponents<AudioSource>()[i].Play();
    }


    public IEnumerator playDescripcion(int i, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        audios_descripciones.GetComponents<AudioSource>()[i].Play();
    }

    /*IEnumerator playDescripciones(int i, int j, int k, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        float tiempo = 0.5f;
        switch_controller.desactivar_objetos();
        StartCoroutine(playPasillo(0, i, tiempo));
        StartCoroutine(activarSeleccion(0, true, tiempo));
        tiempo += pasillos[0].GetComponents<AudioSource>()[i].clip.length;
        StartCoroutine(activarSeleccion(0, false, tiempo));
        tiempo += 1f;

        StartCoroutine(playPasillo(1, j, tiempo));
        StartCoroutine(activarSeleccion(1, true, tiempo));
        tiempo += pasillos[1].GetComponents<AudioSource>()[j].clip.length;
        StartCoroutine(activarSeleccion(1, false, tiempo));
        tiempo += 1f;

        StartCoroutine(playPasillo(2, k, tiempo));
        StartCoroutine(activarSeleccion(2, true, tiempo));
        tiempo += pasillos[2].GetComponents<AudioSource>()[k].clip.length;
        StartCoroutine(activarSeleccion(2, false, tiempo));

        switch_controller.Invoke("activar_objetos", tiempo + 1f);
    }*/

    /*
public IEnumerator playPasillo(int i, int j, float seconds)
{
    yield return new WaitForSeconds(seconds);
    pasillos[i].GetComponents<AudioSource>()[j].Play();
}*/

    IEnumerator activarSeleccion(int i, bool activar, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        seleccion[i].SetActive(activar);
    }

    void mostrarCruceLaberinto(bool primero)
    {
        //print(texturas.Length);
        List<int> orden = new List<int>();
        for (int j = 0; j < 3; j++)
            orden.Add(j);
        int r;
        /*for (int j = 0; j < n_pasillos; j++){
            r = Random.Range(0, orden.Count);
            print("BREAK1: "+pasillos[j].idPasillo + " " + r + " ");
            print("BREAK2: " + texturas[orden[r]].obtenerTextura(dificultad, i));
            pasillos[j].mostrarCrucePasillo(texturas[orden[r]].obtenerTextura(dificultad, i));
            if (soluciones[dificultad][cruce_actual] == orden[r])
                solucion_actual = j;
            //print(soluciones[dificultad][cruce_actual] + " " + orden[r] + " " + r + " " + orden.Count);
            orden.RemoveAt(r);*/

        
        for (int j = 0; j < n_pasillos; j++)
        {
            r = Random.Range(0, orden.Count);
            emociones_pasillos[j] = orden[r];
            Texture2D tex = texturas[orden[r]].obtenerTextura(dificultad);
            int.TryParse(tex.name.Substring(2,2),out indices_cartas[j]);
            indices_cartas[j]--;
            pasillos[j].mostrarCrucePasillo(tex);
            cruce_actual_emociones[j] = orden[r];
            if (soluciones[dificultad][cruce_actual] == orden[r])
                solucion_actual = j;
            //print(soluciones[dificultad][cruce_actual] + " " + orden[r] + " " + r + " " + orden.Count);
            orden.RemoveAt(r);

        }
        emoticono_pequeño.GetComponent<Animator>().Play("Emocion"+ soluciones[dificultad][cruce_actual]);
        
        if (!primero)
        {
            StartCoroutine(playDescripciones(1f));
        }
            //playDescripciones(emociones_pasillos[0], emociones_pasillos[1], emociones_pasillos[2], 0f);
        /*for (int j = 0; j < n_pasillos; j++)
            //pasillos[j].mostrarCrucePasillo(dificultad, i);
            pasillos[j].mostrarCrucePasillo(texturas[j].obtenerTextura(dificultad,i));*/
    }

    public void siguienteCruce(bool primero)
    {
        //print(cruces.Count);
        pasillos[1].GetComponent<BoxCollider>().enabled = true;
        int r = Random.Range(0, cruces.Count);//print(r+" "+cruces.Count);
        cruce_actual = cruces[r];
        //print("Cruce_i:" + cruce_i + " cruce_actual:" + cruce_actual + " n_cruces:" + n_cruces);
        mostrarCruceLaberinto(primero);
        cruces.Remove(cruce_actual);
        cruce_i++;
    }

    public void corregirCruce(int idPasillo)
    {
        bool acierto; int emocion =-1, emocion1 =-1, emocion2 = -1;
        animacion_trofeo.boton_salida.SetActive(true);
        if (solucion_actual == idPasillo) {//(soluciones[dificultad][cruce_actual] == idPasillo){
            acierto = true;
            emocion = cruce_actual_emociones[solucion_actual];
            //print("Cruce " + cruce_actual + ": CORRECTO!! "+emocion);
            estado_juego.incrementarAciertosLaberinto(cruce_actual_emociones[solucion_actual]);
        }else{
            acierto = false;
            emocion1 = cruce_actual_emociones[solucion_actual];
            emocion2 = cruce_actual_emociones[idPasillo];
            //print("Cruce " + cruce_actual + ": HAS FALLADO :(");
            estado_juego.incrementarFallosLaberinto(cruce_actual_emociones[solucion_actual]);
            estado_juego.incrementarFallosLaberinto(cruce_actual_emociones[idPasillo]);
            estado_juego.datos.fallos_general++;
            estado_juego.datos.total_laberinto[cruce_actual_emociones[solucion_actual]]++;
        }
        mt.Actualizar();
        pasillos[1].GetComponent<BoxCollider>().enabled = false;
        if (cruce_i < n_cruces){
            StartCoroutine(SiguienteEscena("-", 1f, acierto, emocion, emocion1, emocion2));
            //siguienteCruce();
        }else
        {
            //Finalizar();
            StartCoroutine(SiguienteEscena(/*"07_EscenaCastillo"*/"09_EscenaMago", t_sig_escena,acierto, emocion, emocion1, emocion2));
        }
    }

    public IEnumerator SiguienteEscena(string escena, float seconds,bool acierto, int emocion, int emocion1, int emocion2)
    {
        yield return new WaitForSeconds(seconds);
        //SceneManager.LoadScene(escena);
        animacion_trofeo.IniciarAnimacion(acierto,emocion, emocion1, emocion2, escena);
    }
}
