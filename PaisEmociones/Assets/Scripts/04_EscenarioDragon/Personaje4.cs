using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje4 : MonoBehaviour {


    public GameObject controller_escena;

    public GameObject dragon;

    private AudioSource[] sonidos;
    private AudioSource espadaSonido;
    private AudioSource pasoSonido;

    private AudioSource[] sonidosDragon;
    private AudioSource gruñidoSonido;
    //private AudioSource fuegoSonido;

    void Awake()
    {
        sonidos = gameObject.GetComponents<AudioSource>();
        espadaSonido = sonidos[0];
        pasoSonido = sonidos[1];
        sonidosDragon = dragon.GetComponents<AudioSource>();
        gruñidoSonido = sonidosDragon[0];
        //fuegoSonido = sonidosDragon[1];
    }

    public void PlayEspadaSonido()
    {
        espadaSonido.Play();
    }

    public void PlayPasoSonido()
    {
        pasoSonido.Play();
        gruñidoSonido.volume = 0.08f;
    }

    public void SubirDragonSonido()
    {
        gruñidoSonido.volume = 0.15f;
    }

    public void esperarPersonajeAstilla()
    {
        controller_escena.SendMessage("esperarPersonajeAstilla");
    }

    public void terminarAstilla()
    {
        controller_escena.SendMessage("terminarAstilla");
    }

    public void esperarPersonajeEspada()
    {
        dragon.GetComponent<Animator>().Play("DragonEnfadadoIdleAnimation");
        dragon.GetComponents<AudioSource>()[2].Play();
        float tiempo = dragon.GetComponents<AudioSource>()[2].clip.length + 1f;
        Invoke("hablaConejo", tiempo);
        tiempo += dragon.GetComponents<AudioSource>()[4].clip.length + 0.5f;
        Invoke("esperarPersonajeEspada2",tiempo);
    }


    private void hablaConejo()
    {
        dragon.GetComponents<AudioSource>()[4].Play();
    }

    public void esperarPersonajeEspada2()
    {
        controller_escena.SendMessage("esperarPersonajeEspada");
    }

    public void terminarEspada()
    {
        controller_escena.SendMessage("terminarEspada");
    }
}

