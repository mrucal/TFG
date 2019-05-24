using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuConfiguracion : MonoBehaviour {

    public CandadoController candado;
    public Toggle[] toggles;
    public GameObject menu_confirmacion;

    public int dificultad;

    private bool first_play = false;

    private void Start()
    {
        dificultad = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().datos.dificultad;
        toggles[dificultad].isOn = true;
        first_play = false;
    }

    public void activarMenu()
    {
        GetComponent<Canvas>().enabled = true;
        toggles[dificultad].isOn = true;
        candado.llaveL = false;
        candado.llaveR = false;
    }

    public void desactivarMenu(bool cambiar_dificultad)
    {
        GetComponent<AudioSource>().Play();
        candado.abierto = false;
        GetComponent<Canvas>().enabled = false;
        if (cambiar_dificultad)
        {
            GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().datos.dificultad = dificultad;
            GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().guardar();
        }
        GameObject.Find("SwitchController").GetComponent<SwitchController>().activar_objetos();
    }

    /*public void setTres(bool on)
    {
        print("3");
        if (first_play)
            GetComponent<AudioSource>().Play();
        first_play = true;
        dificultad = 0;
    }

    public void setCuatro(bool on)
    {
        print("4");
        if (first_play)
            GetComponent<AudioSource>().Play();
        first_play = true;
        dificultad = 1;
    }

    public void setCinco(bool on)
    {
        print("5");
        if (first_play)
            GetComponent<AudioSource>().Play();
        first_play = true;
        dificultad = 2;
    }*/

    public void setDificultad(int dificultad)
    {
        this.dificultad = dificultad;
        if (first_play)
        {
            GetComponent<AudioSource>().Play();
            desactivarMenu(true);
        }
        first_play = true;
    }

    public void confirmar_reiniciar_juego()
    {
        GetComponent<AudioSource>().Play();
        menu_confirmacion.SetActive(true);
    }
    
    public void reiniciar_juego(bool reiniciar)
    {
        GetComponent<AudioSource>().Play();
        if (reiniciar)
        {
            dificultad = 0;
            GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().reset();
            desactivarMenu(false);
            first_play = false;
        }
        menu_confirmacion.SetActive(false);
    }

}
