using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuEvaluacion : MonoBehaviour {

    public CandadoController candado;

    private int i_panel = 0;
    public GameObject[] paneles;

    public GameObject barra_general_aciertos;
    public GameObject barra_general_fallos;
    public Text[] aciertos_general_emocion_text;
    public Text[] fallos_general_emocion_text;
    public Text[] general_text;
    public Text total_general_text;

    public TextMeshProUGUI parejas_text;
    public TextMeshProUGUI[] intentos_parejas_tablero;

    public GameObject barra_laberinto_aciertos;
    public GameObject barra_laberinto_fallos;
    public Text[] aciertos_laberinto_emocion_text;
    public Text[] fallos_laberinto_emocion_text;
    public Text[] laberinto_text;
    public Text total_laberinto_text;

    private EstadoJuego estado;

    private void Awake()
    {
        estado = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        cargarPanel();
    }

    private void cargarPanel()
    {
        for (int i = 0; i < paneles.Length; i++)
            paneles[i].SetActive(false);
        paneles[i_panel].SetActive(true);

        switch (i_panel)
        {
            case 0:
                cargarPanelGeneral();
                break;
            case 1:
                cargarPanelParejas();
                break;
            case 2:
                cargarPanelLaberinto();
                break;
        }
    }

    private void cargarPanelGeneral()
    {
        for(int i = 0; i < 3; i++)
        {
            aciertos_general_emocion_text[i].text = estado.getAciertosEmocion(i).ToString();
            fallos_general_emocion_text[i].text = estado.getFallosEmocion(i).ToString();
        }

        float total_ejercicios = estado.datos.tot_general;
        if (total_ejercicios != 0)
        {
            float porcentaje_aciertos = estado.datos.aciertos_general / total_ejercicios;
            float porcentaje_fallos = estado.datos.fallos_general / total_ejercicios;
            float porcentaje_restante = (1 - (porcentaje_aciertos + porcentaje_fallos));

            general_text[0].text = (porcentaje_aciertos * 100f).ToString() + "%";
            setBarra(barra_general_aciertos, porcentaje_aciertos);

            general_text[1].text = (porcentaje_fallos * 100f).ToString() + "%";
            setBarra(barra_general_fallos, (porcentaje_aciertos + porcentaje_fallos));

            general_text[2].text = (porcentaje_restante * 100f).ToString() + "%";

            total_general_text.text = "Total: " + total_ejercicios.ToString();
        }
    }

    private void cargarPanelParejas()
    {
        parejas_text.text = "Número de parejas: " + estado.datos.n_parejas[estado.datos.dificultad];
        for (int i = 0; i < intentos_parejas_tablero.Length; i++)
            intentos_parejas_tablero[i].text = estado.getIntentosTablero(i).ToString();
    }

    private void cargarPanelLaberinto()
    {

        for (int i = 0; i < 3; i++)
        {
            aciertos_laberinto_emocion_text[i].text = estado.getAciertosLaberintoEmocion(i).ToString();
            fallos_laberinto_emocion_text[i].text = estado.getFallosLaberintoEmocion(i).ToString();
        }

        float total_ejercicios = estado.getTotalLaberinto();
        float porcentaje_aciertos = 0, porcentaje_fallos = 0;
        float aciertos_laberinto = estado.getAciertosLaberinto();
        float fallos_laberinto = estado.getFallosLaberinto();
        if (total_ejercicios != 0)
        {
            porcentaje_aciertos = aciertos_laberinto / total_ejercicios;
            porcentaje_fallos = fallos_laberinto / total_ejercicios;
        }

        //print("pa: " + porcentaje_aciertos + " pf: " + porcentaje_fallos+ " a: "+aciertos_laberinto+" f: "+fallos_laberinto+" t: "+total_ejercicios);
        laberinto_text[0].text = (porcentaje_aciertos * 100f).ToString() + "%";
        setBarra(barra_laberinto_aciertos, porcentaje_aciertos);

        laberinto_text[1].text = (porcentaje_fallos * 100f).ToString() + "%";
        setBarra(barra_laberinto_fallos, (porcentaje_aciertos + porcentaje_fallos));

        total_laberinto_text.text = "Total cruces: " + total_ejercicios.ToString();
    }

    private void setBarra(GameObject barra, float porcentaje)
    {
        barra.transform.localScale = new Vector3(porcentaje, barra.transform.localScale.y, barra.transform.localScale.z);
    }

    public void activarMenu()
    {
        estado = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        GetComponent<Canvas>().enabled = true;
        candado.llaveL = false;
        candado.llaveR = false;
        //barra_aciertos.transform.localScale = new Vector3(0.4f, barra_aciertos.transform.localScale.y, barra_aciertos.transform.localScale.z);
        //barra_fallos.transform.localScale = new Vector3(0.8f, barra_fallos.transform.localScale.y, barra_fallos.transform.localScale.z);
        //setBarra(barra_aciertos, 0.1f);
        //setBarra(barra_fallos, 0.8f);
    }

    public void desactivarMenu()
    {
        GetComponent<AudioSource>().Play();
        candado.abierto = false;
        GetComponent<Canvas>().enabled = false;
        GameObject.Find("SwitchController").GetComponent<SwitchController>().activar_objetos();
    }

    public void siguientePanel()
    {
        GetComponent<AudioSource>().Play();
        i_panel = (i_panel + 1) % 3;
        cargarPanel();
    }

    public void anteriorPanel()
    {
        GetComponent<AudioSource>().Play();
        i_panel = (i_panel + 3 - 1) % 3;
        cargarPanel();
    }
}
